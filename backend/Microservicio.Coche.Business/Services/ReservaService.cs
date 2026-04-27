
using Microservicios.Coche.Business.DTOs.Reserva;
using Microservicios.Coche.Business.Exceptions;
using Microservicios.Coche.Business.Interfaces;
using Microservicios.Coche.Business.Mappers;
using Microservicios.Coche.Business.Validators;
using Microservicios.Coche.DataManagement.Interfaces;

namespace Microservicios.Coche.Business.Services;

public class ReservaService : IReservaService
{
    private readonly IReservaDataService _reservaDataService;
    private readonly IClienteDataService _clienteDataService;
    private readonly ISucursalDataService _sucursalDataService;

    public ReservaService(
        IReservaDataService reservaDataService,
        IClienteDataService clienteDataService,
        ISucursalDataService sucursalDataService)
    {
        _reservaDataService = reservaDataService;
        _clienteDataService = clienteDataService;
        _sucursalDataService = sucursalDataService;
    }

    public async Task<ReservaResponse> CrearAsync(CrearReservaRequest request, CancellationToken cancellationToken = default)
    {
        var errors = ReservaValidator.ValidarCreacion(request);
        if (errors.Any())
            throw new ValidationException("La solicitud de creación de reserva es inválida.", errors);

        // Validaciones cruzadas
        var clienteExistente = await _clienteDataService.ObtenerPorIdAsync(request.CLI_id, cancellationToken);
        if (clienteExistente is null)
            throw new NotFoundException("El cliente asociado no existe.");

        var sucursalRetiroExistente = await _sucursalDataService.ObtenerPorIdAsync(request.RES_sucursalRetiroId, cancellationToken);
        if (sucursalRetiroExistente is null)
            throw new NotFoundException("La sucursal de retiro no existe.");

        var sucursalEntregaExistente = await _sucursalDataService.ObtenerPorIdAsync(request.RES_sucursalEntregaId, cancellationToken);
        if (sucursalEntregaExistente is null)
            throw new NotFoundException("La sucursal de entrega no existe.");

        var dataModel = ReservaBusinessMapper.ToDataModel(request);
        var creado = await _reservaDataService.CrearAsync(dataModel, cancellationToken);

        return ReservaBusinessMapper.ToResponse(creado);
    }

    public async Task<ReservaResponse> ActualizarAsync(ActualizarReservaRequest request, CancellationToken cancellationToken = default)
    {
        var errors = ReservaValidator.ValidarActualizacion(request);
        if (errors.Any())
            throw new ValidationException("La solicitud de actualización de reserva es inválida.", errors);

        var existente = await _reservaDataService.ObtenerPorIdAsync(request.RES_id, cancellationToken);
        if (existente is null)
            throw new NotFoundException("No se encontró la reserva solicitada.");

        // Validaciones condicionales si las foráneas cambiaron
        if (existente.CLI_id != request.CLI_id)
        {
            var cliente = await _clienteDataService.ObtenerPorIdAsync(request.CLI_id, cancellationToken);
            if (cliente is null) throw new NotFoundException("El nuevo cliente asociado no existe.");
        }
        if (existente.RES_sucursalRetiroId != request.RES_sucursalRetiroId)
        {
            var sucRetiro = await _sucursalDataService.ObtenerPorIdAsync(request.RES_sucursalRetiroId, cancellationToken);
            if (sucRetiro is null) throw new NotFoundException("La nueva sucursal de retiro no existe.");
        }
        if (existente.RES_sucursalEntregaId != request.RES_sucursalEntregaId)
        {
            var sucEntrega = await _sucursalDataService.ObtenerPorIdAsync(request.RES_sucursalEntregaId, cancellationToken);
            if (sucEntrega is null) throw new NotFoundException("La nueva sucursal de entrega no existe.");
        }

        var dataModel = ReservaBusinessMapper.ToDataModel(request);

        dataModel.RES_fechaCreacion = existente.RES_fechaCreacion;
        dataModel.RES_usuarioCreacion = existente.RES_usuarioCreacion;

        var actualizado = await _reservaDataService.ActualizarAsync(dataModel, cancellationToken);
        if (actualizado is null)
            throw new NotFoundException("No se pudo actualizar la reserva.");

        return ReservaBusinessMapper.ToResponse(actualizado);
    }

    public async Task<ReservaResponse> ObtenerPorIdAsync(Guid reservaId, CancellationToken cancellationToken = default)
    {
        var reserva = await _reservaDataService.ObtenerPorIdAsync(reservaId, cancellationToken);
        if (reserva is null)
            throw new NotFoundException("No se encontró la reserva solicitada.");

        return ReservaBusinessMapper.ToResponse(reserva);
    }

    public async Task<IReadOnlyList<ReservaResponse>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var reservas = await _reservaDataService.ListarAsync(cancellationToken);
        return reservas.Select(ReservaBusinessMapper.ToResponse).ToList();
    }
}