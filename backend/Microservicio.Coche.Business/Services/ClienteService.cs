
using Microservicios.Coche.Business.DTOs.Cliente;
using Microservicios.Coche.Business.Exceptions;
using Microservicios.Coche.Business.Interfaces;
using Microservicios.Coche.Business.Mappers;
using Microservicios.Coche.Business.Validators;
using Microservicios.Coche.DataAccess.Entities;
using Microservicios.Coche.DataManagement.Interfaces;

namespace Microservicios.Coche.Business.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteDataService _clienteDataService;

    public ClienteService(IClienteDataService clienteDataService)
    {
        _clienteDataService = clienteDataService;
    }


    public async Task<ClienteResponse> ActualizarAsync(ActualizarClienteRequest request, CancellationToken cancellationToken = default)
    {
        var errors = ClienteValidator.ValidarActualizacion(request);

        if (errors.Any())
            throw new ValidationException("La solicitud de actualización de cliente es inválida.", errors);

        var existente = await _clienteDataService.ObtenerPorIdAsync(request.CLI_id, cancellationToken);

        if (existente is null)
            throw new NotFoundException("No se encontró el cliente solicitado.");

        var dataModel = ClienteBusinessMapper.ToDataModel(request);

        // Conservar campos de auditoría de creación
        dataModel.CLI_fechaCreacion = existente.CLI_fechaCreacion;
        dataModel.CLI_usuarioCreacion = existente.CLI_usuarioCreacion;

        var actualizado = await _clienteDataService.ActualizarAsync(dataModel, cancellationToken);

        if (actualizado is null)
            throw new NotFoundException("No se pudo actualizar el cliente porque no existe.");

        return ClienteBusinessMapper.ToResponse(actualizado);
    }

    public async Task<IReadOnlyList<ClienteResponse>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var clientes = await _clienteDataService.ListarAsync(cancellationToken);

        return clientes.Select(ClienteBusinessMapper.ToResponse).ToList();
    }
    // Agrega este método en tu ClienteService existente

    public async Task<ClienteResponse> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var cliente = await _clienteDataService.ObtenerPorIdAsync(id, cancellationToken);

        if (cliente == null)
            throw new NotFoundException($"Cliente con ID {id} no encontrado");

        return ClienteBusinessMapper.ToResponse(cliente);
    }

    public async Task<ClienteResponse> CrearAsync(CrearClienteRequest request, CancellationToken cancellationToken = default)
    {
        var errors = ClienteValidator.ValidarCreacion(request);
        if (errors.Any())
            throw new ValidationException("La solicitud de creación de cliente es inválida.", errors);

        // ✅ Agregar esto:
        var existente = await _clienteDataService.ObtenerPorCedulaAsync(request.CLI_cedula, cancellationToken);
        if (existente != null)
            throw new BusinessException($"Ya existe un cliente con cédula {request.CLI_cedula}.");

        var dataModel = ClienteBusinessMapper.ToDataModel(request);
        var creado = await _clienteDataService.CrearAsync(dataModel, cancellationToken);
        return ClienteBusinessMapper.ToResponse(creado);
    }
    // Agrega este método al ClienteService.cs existente (dentro de la clase):

    public async Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var existente = await _clienteDataService.ObtenerPorIdAsync(id, cancellationToken);
        if (existente is null)
            throw new NotFoundException("No se encontró el cliente a eliminar.");

        return await _clienteDataService.EliminarAsync(id, cancellationToken);
    }
}