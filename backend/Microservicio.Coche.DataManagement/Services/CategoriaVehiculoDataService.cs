using Microservicios.Coche.DataManagement.Interfaces;
using Microservicios.Coche.DataManagement.Mappers;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Services;

public class CategoriaVehiculoDataService : ICategoriaVehiculoDataService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoriaVehiculoDataService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // ✅ FIX: Antes devolvía new List<>() vacía hardcodeada
    public async Task<IReadOnlyList<CategoriaVehiculoDataModel>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.CategoriaVehiculoRepository.ListarAsync(cancellationToken);
        return entities.Select(CategoriaVehiculoDataMapper.ToDataModel).ToList();
    }

    // ✅ FIX: Antes devolvía new CategoriaVehiculoDataModel() vacío hardcodeado
    public async Task<CategoriaVehiculoDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.CategoriaVehiculoRepository.ObtenerPorIdAsync(id, cancellationToken);
        return entity is null ? null : CategoriaVehiculoDataMapper.ToDataModel(entity);
    }

    // ✅ FIX: Antes devolvía el model sin guardarlo en la BD ni llamar SaveChanges
    public async Task<CategoriaVehiculoDataModel> CrearAsync(CategoriaVehiculoDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = CategoriaVehiculoDataMapper.ToEntity(model);
        await _unitOfWork.CategoriaVehiculoRepository.AgregarAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return CategoriaVehiculoDataMapper.ToDataModel(entity);
    }

    // ✅ FIX: Antes devolvía el model sin guardarlo en la BD ni llamar SaveChanges
    public async Task<CategoriaVehiculoDataModel?> ActualizarAsync(CategoriaVehiculoDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.CategoriaVehiculoRepository.ObtenerParaActualizarAsync(model.CAT_id, cancellationToken);
        if (entity is null) return null;

        entity.CAT_nombre = model.CAT_nombre;
        entity.CAT_descripcion = model.CAT_descripcion;
        entity.CAT_costoBase = model.CAT_costoBase;
        entity.CAT_capacidadPasajeros = model.CAT_capacidadPasajeros;
        entity.CAT_capacidadMaletas = model.CAT_capacidadMaletas;
        entity.CAT_tipoTransmision = model.CAT_tipoTransmision;
        entity.CAT_fechaModificacion = model.CAT_fechaModificacion ?? DateTime.UtcNow;
        entity.CAT_usuarioModificacion = model.CAT_usuarioModificacion;

        _unitOfWork.CategoriaVehiculoRepository.Actualizar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CategoriaVehiculoDataMapper.ToDataModel(entity);
    }

    public Task EliminarLogicoAsync(Guid id, string usuario, CancellationToken cancellationToken = default)
        => Task.CompletedTask; // No aplica eliminación lógica para categorías
}