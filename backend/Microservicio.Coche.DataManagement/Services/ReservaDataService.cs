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

public class ReservaDataService : IReservaDataService
{
    private readonly IUnitOfWork _unitOfWork;

    public ReservaDataService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ReservaDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.ReservaRepository.ObtenerPorIdAsync(id, cancellationToken);
        return entity is null ? null : ReservaDataMapper.ToDataModel(entity);
    }

    public async Task<IReadOnlyList<ReservaDataModel>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.ReservaRepository.ListarAsync(cancellationToken);
        return entities.Select(ReservaDataMapper.ToDataModel).ToList();
    }

    public async Task<DataPagedResult<ReservaDataModel>> BuscarAsync(ReservaFiltroDataModel filtro, CancellationToken cancellationToken = default)
    {
        // Llamar una sola vez al repositorio para obtener el resultado paginado
        var pagedResult = await _unitOfWork.ReservaQueryRepository.BuscarAsync(
            filtro.PageNumber,
            filtro.PageSize,
            filtro.CLI_id,
            filtro.RES_estado,
            cancellationToken
        );

        return new DataPagedResult<ReservaDataModel>
        {
            Items = pagedResult.Items.Select(ReservaDataMapper.ToDataModel).ToList(),
            PageNumber = pagedResult.PageNumber,
            PageSize = pagedResult.PageSize,
            TotalRecords = pagedResult.TotalRecords
        };
    }

    public async Task<ReservaDataModel> CrearAsync(ReservaDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = ReservaDataMapper.ToEntity(model);
        await _unitOfWork.ReservaRepository.AgregarAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ReservaDataMapper.ToDataModel(entity);
    }

    public async Task<ReservaDataModel?> ActualizarAsync(ReservaDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.ReservaRepository.ObtenerParaActualizarAsync(model.RES_id, cancellationToken);
        if (entity is null) return null;

        entity.CLI_id = model.CLI_id;
        entity.RES_sucursalRetiroId = model.RES_sucursalRetiroId;
        entity.RES_sucursalEntregaId = model.RES_sucursalEntregaId;
        entity.RES_fechaRetiro = model.RES_fechaRetiro;
        entity.RES_fechaEntrega = model.RES_fechaEntrega;
        entity.RES_estado = model.RES_estado;
        entity.RES_fechaModificacion = model.RES_fechaModificacion ?? DateTime.UtcNow;
        entity.RES_usuarioModificacion = model.RES_usuarioModificacion;

        _unitOfWork.ReservaRepository.Actualizar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ReservaDataMapper.ToDataModel(entity);
    }

    public async Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.ReservaRepository.ObtenerParaActualizarAsync(id, cancellationToken);
        if (entity is null) return false;

        _unitOfWork.ReservaRepository.Eliminar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}