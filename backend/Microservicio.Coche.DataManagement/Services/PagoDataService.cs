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

public class PagoDataService : IPagoDataService
{
    private readonly IUnitOfWork _unitOfWork;

    public PagoDataService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagoDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.PagoRepository.ObtenerPorIdAsync(id, cancellationToken);
        return entity is null ? null : PagoDataMapper.ToDataModel(entity);
    }

    public async Task<IReadOnlyList<PagoDataModel>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.PagoRepository.ListarAsync(cancellationToken);
        return entities.Select(PagoDataMapper.ToDataModel).ToList();
    }

    public async Task<PagoDataModel> CrearAsync(PagoDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = PagoDataMapper.ToEntity(model);
        await _unitOfWork.PagoRepository.AgregarAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return PagoDataMapper.ToDataModel(entity);
    }

    public async Task<PagoDataModel?> ActualizarAsync(PagoDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.PagoRepository.ObtenerParaActualizarAsync(model.PAG_id, cancellationToken);
        if (entity is null) return null;

        entity.RES_id = model.RES_id;
        entity.PAG_monto = model.PAG_monto;
        entity.PAG_metodo = model.PAG_metodo;
        entity.PAG_estado = model.PAG_estado;
        entity.PAG_fechaPago = model.PAG_fechaPago ?? DateTime.UtcNow;

        _unitOfWork.PagoRepository.Actualizar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return PagoDataMapper.ToDataModel(entity);
    }

    public async Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.PagoRepository.ObtenerParaActualizarAsync(id, cancellationToken);
        if (entity is null) return false;

        _unitOfWork.PagoRepository.Eliminar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<DataPagedResult<PagoDataModel>> BuscarAsync(PagoFiltroDataModel filtro, CancellationToken cancellationToken = default)
    {
        // Llamamos a nuestro nuevo QueryRepository usando la Tupla que creamos
        var result = await _unitOfWork.PagoQueryRepository.BuscarAsync(
            filtro.PageNumber,
            filtro.PageSize,
            filtro.RES_id,
            filtro.PAG_estado,
            cancellationToken
        );

        // Mapeamos el resultado al objeto paginado que espera la capa Business
        return new DataPagedResult<PagoDataModel>
        {
            Items = result.Items.Select(PagoDataMapper.ToDataModel).ToList(),
            TotalRecords = result.TotalRecords,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize
        };
    }
}