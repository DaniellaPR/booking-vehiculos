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

public class ReservaDetalleDataService : IReservaDetalleDataService
{
    private readonly IUnitOfWork _unitOfWork;

    public ReservaDetalleDataService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ReservaDetalleDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.ReservaDetalleRepository.ObtenerPorIdAsync(id, cancellationToken);
        return entity is null ? null : ReservaDetalleDataMapper.ToDataModel(entity);
    }

    public async Task<IReadOnlyList<ReservaDetalleDataModel>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.ReservaDetalleRepository.ListarAsync(cancellationToken);
        return entities.Select(ReservaDetalleDataMapper.ToDataModel).ToList();
    }

    public async Task<DataPagedResult<ReservaDetalleDataModel>> BuscarAsync(ReservaDetalleFiltroDataModel filtro, CancellationToken cancellationToken = default)
    {
        var result = await _unitOfWork.ReservaDetalleQueryRepository.BuscarAsync(filtro.PageNumber, filtro.PageSize, cancellationToken);
        return new DataPagedResult<ReservaDetalleDataModel>
        {
            Items = result.Items.Select(ReservaDetalleDataMapper.ToDataModel).ToList(),
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalRecords = result.TotalRecords
        };
    }

    public async Task<ReservaDetalleDataModel> CrearAsync(ReservaDetalleDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = ReservaDetalleDataMapper.ToEntity(model);
        await _unitOfWork.ReservaDetalleRepository.AgregarAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ReservaDetalleDataMapper.ToDataModel(entity);
    }

    public async Task<ReservaDetalleDataModel?> ActualizarAsync(ReservaDetalleDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.ReservaDetalleRepository.ObtenerParaActualizarAsync(model.REX_id, cancellationToken);
        if (entity is null) return null;

        entity.RES_id = model.RES_id;
        entity.SEG_id = model.SEG_id;
        entity.EXT_id = model.EXT_id;
        entity.REX_cantidad = model.REX_cantidad;
        entity.REX_fechaModificacion = model.REX_fechaModificacion ?? DateTime.UtcNow;
        entity.REX_usuarioModificacion = model.REX_usuarioModificacion;

        _unitOfWork.ReservaDetalleRepository.Actualizar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ReservaDetalleDataMapper.ToDataModel(entity);
    }

    public async Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.ReservaDetalleRepository.ObtenerParaActualizarAsync(id, cancellationToken);
        if (entity is null) return false;

        _unitOfWork.ReservaDetalleRepository.Eliminar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
