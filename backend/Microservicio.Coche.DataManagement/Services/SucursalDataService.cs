using Microservicios.Coche.DataManagement.Interfaces;
using Microservicios.Coche.DataManagement.Mappers;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Services;

public class SucursalDataService : ISucursalDataService
{
    private readonly IUnitOfWork _unitOfWork;

    public SucursalDataService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // ✅ FIX: Antes devolvía new List<>() vacía hardcodeada
    public async Task<IReadOnlyList<SucursalDataModel>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.SucursalRepository.ListarAsync(cancellationToken);
        return entities.Select(e => e.ToDataModel()).ToList();
    }

    // ✅ FIX: Antes devolvía new SucursalDataModel() vacío hardcodeado
    public async Task<SucursalDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.SucursalRepository.ObtenerPorIdAsync(id, cancellationToken);
        return entity is null ? null : entity.ToDataModel();
    }

    // ✅ FIX: Antes devolvía el model sin guardarlo ni llamar SaveChanges
    public async Task<SucursalDataModel> CrearAsync(SucursalDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = model.ToEntity();
        await _unitOfWork.SucursalRepository.AgregarAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return entity.ToDataModel();
    }

    // ✅ FIX: Antes devolvía el model sin guardarlo ni llamar SaveChanges
    public async Task<SucursalDataModel?> ActualizarAsync(SucursalDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.SucursalRepository.ObtenerParaActualizarAsync(model.SUC_id, cancellationToken);
        if (entity is null) return null;

        entity.SUC_nombre = model.SUC_nombre;
        entity.SUC_ciudad = model.SUC_ciudad;
        entity.SUC_direccion = model.SUC_direccion;
        entity.SUC_coordenadas = model.SUC_coordenadas;
        entity.SUC_fechaModificacion = model.SUC_fechaModificacion ?? DateTime.UtcNow;
        entity.SUC_usuarioModificacion = model.SUC_usuarioModificacion;

        _unitOfWork.SucursalRepository.Actualizar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.ToDataModel();
    }

    public async Task EliminarLogicoAsync(Guid id, string usuario, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.SucursalRepository.ObtenerParaActualizarAsync(id, cancellationToken);
        if (entity is null) return;

        _unitOfWork.SucursalRepository.Eliminar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}