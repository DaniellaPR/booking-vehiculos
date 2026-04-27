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

public class VehiculoDataService : IVehiculoDataService
{
    private readonly IUnitOfWork _unitOfWork;

    public VehiculoDataService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<VehiculoDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.VehiculoRepository.ObtenerPorIdAsync(id, cancellationToken);
        return entity is null ? null : VehiculoDataMapper.ToDataModel(entity);
    }

    public async Task<IReadOnlyList<VehiculoDataModel>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.VehiculoRepository.ListarAsync(cancellationToken);
        return entities.Select(VehiculoDataMapper.ToDataModel).ToList();
    }

    public async Task<DataPagedResult<VehiculoDataModel>> BuscarAsync(VehiculoFiltroDataModel filtro, CancellationToken cancellationToken = default)
    {
        var result = await _unitOfWork.VehiculoQueryRepository.BuscarAsync(filtro.PageNumber, filtro.PageSize, cancellationToken);
        return new DataPagedResult<VehiculoDataModel>
        {
            Items = result.Items.Select(VehiculoDataMapper.ToDataModel).ToList(),
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalRecords = result.TotalRecords
        };
    }

    public async Task<VehiculoDataModel> CrearAsync(VehiculoDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = VehiculoDataMapper.ToEntity(model);
        await _unitOfWork.VehiculoRepository.AgregarAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return VehiculoDataMapper.ToDataModel(entity);
    }

    public async Task<VehiculoDataModel?> ActualizarAsync(VehiculoDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.VehiculoRepository.ObtenerParaActualizarAsync(model.VEH_id, cancellationToken);
        if (entity is null) return null;

        entity.CAT_id = model.CAT_id;
        entity.SUC_id = model.SUC_id;
        entity.VEH_placa = model.VEH_placa;
        entity.VEH_modelo = model.VEH_modelo;
        entity.VEH_anio = model.VEH_anio;
        entity.VEH_color = model.VEH_color;
        entity.VEH_kilometraje = model.VEH_kilometraje;
        entity.VEH_estado = model.VEH_estado;
        entity.VEH_fechaModificacion = model.VEH_fechaModificacion ?? DateTime.UtcNow;
        entity.VEH_usuarioModificacion = model.VEH_usuarioModificacion;

        _unitOfWork.VehiculoRepository.Actualizar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return VehiculoDataMapper.ToDataModel(entity);
    }

    public async Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.VehiculoRepository.ObtenerParaActualizarAsync(id, cancellationToken);
        if (entity is null) return false;

        _unitOfWork.VehiculoRepository.Eliminar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
