using Microservicios.Coche.DataManagement.Interfaces;
using Microservicios.Coche.DataManagement.Mappers;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Services;

public class SeguroDataService : ISeguroDataService
{
    private readonly IUnitOfWork _unitOfWork;

    public SeguroDataService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // ✅ FIX: Antes devolvía new List<>() vacía hardcodeada
    public async Task<IReadOnlyList<SeguroDataModel>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.SeguroRepository.ListarAsync(cancellationToken);
        return entities.Select(e => e.ToDataModel()).ToList();
    }

    // ✅ FIX: Antes devolvía new SeguroDataModel() vacío hardcodeado
    public async Task<SeguroDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.SeguroRepository.ObtenerPorIdAsync(id, cancellationToken);
        return entity is null ? null : entity.ToDataModel();
    }

    // ✅ FIX: Antes devolvía el model sin guardarlo ni llamar SaveChanges
    public async Task<SeguroDataModel> CrearAsync(SeguroDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = model.ToEntity();
        await _unitOfWork.SeguroRepository.AgregarAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return entity.ToDataModel();
    }

    // ✅ FIX: Antes devolvía el model sin guardarlo ni llamar SaveChanges
    public async Task<SeguroDataModel?> ActualizarAsync(SeguroDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.SeguroRepository.ObtenerParaActualizarAsync(model.SEG_id, cancellationToken);
        if (entity is null) return null;

        entity.SEG_nombre = model.SEG_nombre;
        entity.SEG_costoDiario = model.SEG_costoDiario;
        entity.SEG_fechaModificacion = model.SEG_fechaModificacion ?? DateTime.UtcNow;
        entity.SEG_usuarioModificacion = model.SEG_usuarioModificacion;

        _unitOfWork.SeguroRepository.Actualizar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.ToDataModel();
    }

    public async Task EliminarLogicoAsync(Guid id, string usuario, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.SeguroRepository.ObtenerParaActualizarAsync(id, cancellationToken);
        if (entity is null) return;

        _unitOfWork.SeguroRepository.Eliminar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}