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

public class ExtraAdicionalDataService : IExtraAdicionalDataService
{
    private readonly IUnitOfWork _unitOfWork;

    public ExtraAdicionalDataService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ExtraAdicionalDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.ExtraAdicionalRepository.ObtenerPorIdAsync(id, cancellationToken);
        return entity is null ? null : ExtraAdicionalDataMapper.ToDataModel(entity);
    }

    public async Task<IReadOnlyList<ExtraAdicionalDataModel>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.ExtraAdicionalRepository.ListarAsync(cancellationToken);
        return entities.Select(ExtraAdicionalDataMapper.ToDataModel).ToList();
    }

    public async Task<DataPagedResult<ExtraAdicionalDataModel>> BuscarAsync(ExtraAdicionalFiltroDataModel filtro, CancellationToken cancellationToken = default)
    {
        var result = await _unitOfWork.ExtraAdicionalQueryRepository.BuscarAsync(filtro.PageNumber, filtro.PageSize, cancellationToken);
        return new DataPagedResult<ExtraAdicionalDataModel>
        {
            Items = result.Items.Select(ExtraAdicionalDataMapper.ToDataModel).ToList(),
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalRecords = result.TotalRecords
        };
    }

    public async Task<ExtraAdicionalDataModel> CrearAsync(ExtraAdicionalDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = ExtraAdicionalDataMapper.ToEntity(model);
        await _unitOfWork.ExtraAdicionalRepository.AgregarAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ExtraAdicionalDataMapper.ToDataModel(entity);
    }

    public async Task<ExtraAdicionalDataModel?> ActualizarAsync(ExtraAdicionalDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.ExtraAdicionalRepository.ObtenerParaActualizarAsync(model.EXT_id, cancellationToken);
        if (entity is null) return null;

        entity.EXT_nombre = model.EXT_nombre;
        entity.EXT_costo = model.EXT_costo;
        entity.EXT_fechaModificacion = model.EXT_fechaModificacion ?? DateTime.UtcNow;
        entity.EXT_usuarioModificacion = model.EXT_usuarioModificacion;

        _unitOfWork.ExtraAdicionalRepository.Actualizar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ExtraAdicionalDataMapper.ToDataModel(entity);
    }

    public async Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.ExtraAdicionalRepository.ObtenerParaActualizarAsync(id, cancellationToken);
        if (entity is null) return false;

        _unitOfWork.ExtraAdicionalRepository.Eliminar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}