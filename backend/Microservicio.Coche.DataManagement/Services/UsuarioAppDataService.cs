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

public class UsuarioAppDataService : IUsuarioAppDataService
{
    private readonly IUnitOfWork _unitOfWork;

    public UsuarioAppDataService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UsuarioAppDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.UsuarioAppRepository.ObtenerPorIdAsync(id, cancellationToken);
        return entity is null ? null : UsuarioAppDataMapper.ToDataModel(entity);
    }

    public async Task<UsuarioAppDataModel?> ObtenerPorEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var entity = (await _unitOfWork.UsuarioAppRepository.ListarAsync(cancellationToken)).FirstOrDefault(x => x.USU_email == email);
        return entity is null ? null : UsuarioAppDataMapper.ToDataModel(entity);
    }

    public async Task<IReadOnlyList<UsuarioAppDataModel>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.UsuarioAppRepository.ListarAsync(cancellationToken);
        return entities.Select(UsuarioAppDataMapper.ToDataModel).ToList();
    }

    public async Task<DataPagedResult<UsuarioAppDataModel>> BuscarAsync(UsuarioAppFiltroDataModel filtro, CancellationToken cancellationToken = default)
    {
        var result = await _unitOfWork.UsuarioAppQueryRepository.BuscarAsync(filtro.PageNumber, filtro.PageSize, cancellationToken);
        return new DataPagedResult<UsuarioAppDataModel>
        {
            Items = result.Items.Select(UsuarioAppDataMapper.ToDataModel).ToList(),
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalRecords = result.TotalRecords
        };
    }

    public async Task<UsuarioAppDataModel> CrearAsync(UsuarioAppDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = UsuarioAppDataMapper.ToEntity(model);
        await _unitOfWork.UsuarioAppRepository.AgregarAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return UsuarioAppDataMapper.ToDataModel(entity);
    }

    public async Task<UsuarioAppDataModel?> ActualizarAsync(UsuarioAppDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.UsuarioAppRepository.ObtenerParaActualizarAsync(model.USU_id, cancellationToken);
        if (entity is null) return null;

        entity.ROL_id = model.ROL_id;
        entity.USU_email = model.USU_email;
        entity.USU_passwordHash = model.USU_passwordHash;
        entity.USU_fechaModificacion = model.USU_fechaModificacion ?? DateTime.UtcNow;
        entity.USU_usuarioModificacion = model.USU_usuarioModificacion;

        _unitOfWork.UsuarioAppRepository.Actualizar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return UsuarioAppDataMapper.ToDataModel(entity);
    }

    public async Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.UsuarioAppRepository.ObtenerPorIdAsync(id, cancellationToken);
        if (entity is null) return false;
        _unitOfWork.UsuarioAppRepository.Eliminar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}