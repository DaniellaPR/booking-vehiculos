using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microservicios.Coche.DataManagement.Common;
using Microservicios.Coche.DataManagement.Interfaces;
using Microservicios.Coche.DataManagement.Mappers;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Services;

public class MantenimientoDataService : IMantenimientoDataService
{
    private readonly IUnitOfWork _unitOfWork;

    public MantenimientoDataService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MantenimientoDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.MantenimientoRepository.ObtenerPorIdAsync(id, cancellationToken);
        return entity is null ? null : MantenimientoDataMapper.ToDataModel(entity);
    }

    public async Task<IReadOnlyList<MantenimientoDataModel>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.MantenimientoRepository.ListarAsync(cancellationToken);
        return entities.Select(MantenimientoDataMapper.ToDataModel).ToList();
    }

    public async Task<IReadOnlyList<MantenimientoDataModel>> ListarPorVehiculoAsync(Guid vehiculoId, CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.MantenimientoRepository.ObtenerPorVehiculoAsync(vehiculoId, cancellationToken);
        return entities.Select(MantenimientoDataMapper.ToDataModel).ToList();
    }

    public async Task<DataPagedResult<MantenimientoDataModel>> BuscarAsync(MantenimientoFiltroDataModel filtro, CancellationToken cancellationToken = default)
    {
        var result = await _unitOfWork.MantenimientoQueryRepository.BuscarAsync(filtro.PageNumber, filtro.PageSize, cancellationToken);

        return new DataPagedResult<MantenimientoDataModel>
        {
            Items = result.Items.Select(MantenimientoDataMapper.ToDataModel).ToList(),
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalRecords = result.TotalRecords
        };
    }

    public async Task<MantenimientoDataModel> CrearAsync(MantenimientoDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = MantenimientoDataMapper.ToEntity(model);

        await _unitOfWork.MantenimientoRepository.AgregarAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MantenimientoDataMapper.ToDataModel(entity);
    }

    public async Task<MantenimientoDataModel?> ActualizarAsync(MantenimientoDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.MantenimientoRepository.ObtenerParaActualizarAsync(model.MAN_id, cancellationToken);
        if (entity is null) return null;

        
        entity.VEH_id = model.VEH_id;
        entity.MAN_fecha = model.MAN_fecha;
        entity.MAN_descripcion = model.MAN_descripcion;
        entity.MAN_costo = model.MAN_costo;
        entity.MAN_fechaModificacion = model.MAN_fechaModificacion ?? DateTime.UtcNow;
        entity.MAN_usuarioModificacion = model.MAN_usuarioModificacion;

        _unitOfWork.MantenimientoRepository.Actualizar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MantenimientoDataMapper.ToDataModel(entity);
    }

    public async Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.MantenimientoRepository.ObtenerParaActualizarAsync(id, cancellationToken);
        if (entity is null) return false;

        _unitOfWork.MantenimientoRepository.Eliminar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
