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

public class HorarioAtencionDataService : IHorarioAtencionDataService
{
    private readonly IUnitOfWork _unitOfWork;

    public HorarioAtencionDataService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<HorarioAtencionDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.HorarioAtencionRepository.ObtenerPorIdAsync(id, cancellationToken);
        return entity is null ? null : HorarioAtencionDataMapper.ToDataModel(entity);
    }

    public async Task<IReadOnlyList<HorarioAtencionDataModel>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.HorarioAtencionRepository.ListarAsync(cancellationToken);
        return entities.Select(HorarioAtencionDataMapper.ToDataModel).ToList();
    }

    public async Task<IReadOnlyList<HorarioAtencionDataModel>> ListarPorSucursalAsync(Guid sucursalId, CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.HorarioAtencionRepository.ObtenerPorSucursalAsync(sucursalId, cancellationToken);
        return entities.Select(HorarioAtencionDataMapper.ToDataModel).ToList();
    }

    public async Task<DataPagedResult<HorarioAtencionDataModel>> BuscarAsync(HorarioAtencionFiltroDataModel filtro, CancellationToken cancellationToken = default)
    {
        var result = await _unitOfWork.HorarioAtencionQueryRepository.BuscarAsync(filtro.PageNumber, filtro.PageSize, cancellationToken);
        return new DataPagedResult<HorarioAtencionDataModel>
        {
            Items = result.Items.Select(HorarioAtencionDataMapper.ToDataModel).ToList(),
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalRecords = result.TotalRecords
        };
    }

    public async Task<HorarioAtencionDataModel> CrearAsync(HorarioAtencionDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = HorarioAtencionDataMapper.ToEntity(model);
        await _unitOfWork.HorarioAtencionRepository.AgregarAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return HorarioAtencionDataMapper.ToDataModel(entity);
    }

    public async Task<HorarioAtencionDataModel?> ActualizarAsync(HorarioAtencionDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.HorarioAtencionRepository.ObtenerParaActualizarAsync(model.HOR_id, cancellationToken);
        if (entity is null) return null;

        entity.SUC_id = model.SUC_id;
        entity.HOR_diaSemana = model.HOR_diaSemana;
        entity.HOR_apertura = model.HOR_apertura;
        entity.HOR_cierre = model.HOR_cierre;
        entity.HOR_fechaModificacion = model.HOR_fechaModificacion ?? DateTime.UtcNow;
        entity.HOR_usuarioModificacion = model.HOR_usuarioModificacion;

        _unitOfWork.HorarioAtencionRepository.Actualizar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return HorarioAtencionDataMapper.ToDataModel(entity);
    }

    public async Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.HorarioAtencionRepository.ObtenerParaActualizarAsync(id, cancellationToken);
        if (entity is null) return false;

        _unitOfWork.HorarioAtencionRepository.Eliminar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
