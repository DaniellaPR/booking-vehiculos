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

public class TarifaDataService : ITarifaDataService
{
    private readonly IUnitOfWork _unitOfWork;

    public TarifaDataService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TarifaDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.TarifaRepository.ObtenerPorIdAsync(id, cancellationToken);
        return entity is null ? null : TarifaDataMapper.ToDataModel(entity);
    }

    public async Task<IReadOnlyList<TarifaDataModel>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.TarifaRepository.ListarAsync(cancellationToken);
        return entities.Select(TarifaDataMapper.ToDataModel).ToList();
    }

    public async Task<IReadOnlyList<TarifaDataModel>> ListarPorCategoriaAsync(Guid categoriaId, CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.TarifaRepository.ListarPorCategoriaAsync(categoriaId, cancellationToken);
        return entities.Select(TarifaDataMapper.ToDataModel).ToList();
    }

    public async Task<DataPagedResult<TarifaDataModel>> BuscarAsync(TarifaFiltroDataModel filtro, CancellationToken cancellationToken = default)
    {
        var result = await _unitOfWork.TarifaQueryRepository.BuscarAsync(filtro.PageNumber, filtro.PageSize, cancellationToken);
        return new DataPagedResult<TarifaDataModel>
        {
            Items = result.Items.Select(TarifaDataMapper.ToDataModel).ToList(),
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalRecords = result.TotalRecords
        };
    }

    public async Task<TarifaDataModel> CrearAsync(TarifaDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = TarifaDataMapper.ToEntity(model);
        await _unitOfWork.TarifaRepository.AgregarAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return TarifaDataMapper.ToDataModel(entity);
    }

    public async Task<TarifaDataModel?> ActualizarAsync(TarifaDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.TarifaRepository.ObtenerParaActualizarAsync(model.TAR_id, cancellationToken);
        if (entity is null) return null;

        entity.CAT_id = model.CAT_id;
        entity.TAR_precioDiario = model.TAR_precioDiario;
        entity.TAR_fechaModificacion = model.TAR_fechaModificacion ?? DateTime.UtcNow;
        entity.TAR_usuarioModificacion = model.TAR_usuarioModificacion;

        _unitOfWork.TarifaRepository.Actualizar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return TarifaDataMapper.ToDataModel(entity);
    }

    public async Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.TarifaRepository.ObtenerPorIdAsync(id, cancellationToken);
        if (entity is null) return false;
        _unitOfWork.TarifaRepository.Eliminar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
