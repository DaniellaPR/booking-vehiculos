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

public class RolDataService : IRolDataService
{
    private readonly IUnitOfWork _unitOfWork;

    public RolDataService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<RolDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.RolRepository.ObtenerPorIdAsync(id, cancellationToken);
        return entity is null ? null : RolDataMapper.ToDataModel(entity);
    }

    public async Task<IReadOnlyList<RolDataModel>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.RolRepository.ListarAsync(cancellationToken);
        return entities.Select(RolDataMapper.ToDataModel).ToList();
    }

    public async Task<DataPagedResult<RolDataModel>> BuscarAsync(RolFiltroDataModel filtro, CancellationToken cancellationToken = default)
    {
        var result = await _unitOfWork.RolQueryRepository.BuscarAsync(filtro.PageNumber, filtro.PageSize, cancellationToken);
        return new DataPagedResult<RolDataModel>
        {
            Items = result.Items.Select(RolDataMapper.ToDataModel).ToList(),
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalRecords = result.TotalRecords
        };
    }

    public async Task<RolDataModel> CrearAsync(RolDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = RolDataMapper.ToEntity(model);
        await _unitOfWork.RolRepository.AgregarAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return RolDataMapper.ToDataModel(entity);
    }

    public async Task<RolDataModel?> ActualizarAsync(RolDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.RolRepository.ObtenerParaActualizarAsync(model.ROL_id, cancellationToken);
        if (entity is null) return null;

        entity.ROL_nombre = model.ROL_nombre;
        entity.ROL_fechaModificacion = model.ROL_fechaModificacion ?? DateTime.UtcNow;
        entity.ROL_usuarioModificacion = model.ROL_usuarioModificacion;

        _unitOfWork.RolRepository.Actualizar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return RolDataMapper.ToDataModel(entity);
    }

    public async Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.RolRepository.ObtenerParaActualizarAsync(id, cancellationToken);
        if (entity is null) return false;

        _unitOfWork.RolRepository.Eliminar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
