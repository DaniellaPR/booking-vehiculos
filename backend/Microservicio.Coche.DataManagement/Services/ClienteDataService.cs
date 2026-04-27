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

public class ClienteDataService : IClienteDataService
{
    private readonly IUnitOfWork _unitOfWork;

    public ClienteDataService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ClienteDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.ClienteRepository.ObtenerPorIdAsync(id, cancellationToken);
        return entity is null ? null : ClienteDataMapper.ToDataModel(entity);
    }

    public async Task<ClienteDataModel?> ObtenerPorCedulaAsync(string cedula, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.ClienteRepository.ObtenerPorCedulaAsync(cedula, cancellationToken);
        return entity is null ? null : ClienteDataMapper.ToDataModel(entity);
    }


    public async Task<IReadOnlyList<ClienteDataModel>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _unitOfWork.ClienteRepository.ListarAsync(cancellationToken);
        return entities.Select(ClienteDataMapper.ToDataModel).ToList();
    }

    public async Task<DataPagedResult<ClienteDataModel>> BuscarAsync(ClienteFiltroDataModel filtro, CancellationToken cancellationToken = default)
    {
        var result = await _unitOfWork.ClienteQueryRepository.BuscarAsync(filtro.PageNumber, filtro.PageSize, cancellationToken);
        return new DataPagedResult<ClienteDataModel>
        {
            Items = result.Items.Select(ClienteDataMapper.ToDataModel).ToList(),
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalRecords = result.TotalRecords
        };
    }

    public async Task<ClienteDataModel> CrearAsync(ClienteDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = ClienteDataMapper.ToEntity(model);
        await _unitOfWork.ClienteRepository.AgregarAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return ClienteDataMapper.ToDataModel(entity);
    }

    public async Task<ClienteDataModel?> ActualizarAsync(ClienteDataModel model, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.ClienteRepository.ObtenerParaActualizarAsync(model.CLI_id, cancellationToken);
        if (entity is null) return null;

        entity.CLI_nombres = model.CLI_nombres;
        entity.CLI_apellidos = model.CLI_apellidos;
        entity.CLI_cedula = model.CLI_cedula;
        entity.CLI_telefono = model.CLI_telefono;
        entity.CLI_fechaModificacion = model.CLI_fechaModificacion ?? DateTime.UtcNow;
        entity.CLI_usuarioModificacion = model.CLI_usuarioModificacion;

        _unitOfWork.ClienteRepository.Actualizar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ClienteDataMapper.ToDataModel(entity);
    }


    public async Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _unitOfWork.ClienteRepository.ObtenerPorIdAsync(id, cancellationToken);
        if (entity is null) return false;

        _unitOfWork.ClienteRepository.Eliminar(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}