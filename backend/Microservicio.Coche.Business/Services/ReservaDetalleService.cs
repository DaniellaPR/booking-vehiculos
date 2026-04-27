

using Microservicios.Coche.Business.DTOs.ReservaDetalle;
using Microservicios.Coche.Business.Exceptions;
using Microservicios.Coche.Business.Interfaces;
using Microservicios.Coche.Business.Mappers;
using Microservicios.Coche.Business.Validators;
using Microservicios.Coche.DataManagement.Interfaces;

namespace Microservicios.Coche.Business.Services;

public class ReservaDetalleService : IReservaDetalleService
{
    private readonly IReservaDetalleDataService _detalleDataService;
    private readonly IReservaDataService _reservaDataService;
    private readonly ISeguroDataService _seguroDataService;
    private readonly IExtraAdicionalDataService _extraDataService;

    public ReservaDetalleService(
        IReservaDetalleDataService detalleDataService,
        IReservaDataService reservaDataService,
        ISeguroDataService seguroDataService,
        IExtraAdicionalDataService extraDataService)
    {
        _detalleDataService = detalleDataService;
        _reservaDataService = reservaDataService;
        _seguroDataService = seguroDataService;
        _extraDataService = extraDataService;
    }

    public async Task<ReservaDetalleResponse> CrearAsync(CrearReservaDetalleRequest request, CancellationToken cancellationToken = default)
    {
        var errors = ReservaDetalleValidator.ValidarCreacion(request);
        if (errors.Any())
            throw new ValidationException("La solicitud de creación de detalle es inválida.", errors);

        // Validar Reserva Padre
        var reservaExistente = await _reservaDataService.ObtenerPorIdAsync(request.RES_id, cancellationToken);
        if (reservaExistente is null)
            throw new NotFoundException("La reserva asociada no existe.");

        // Validar Seguro (si aplica)
        if (request.SEG_id.HasValue)
        {
            var seguro = await _seguroDataService.ObtenerPorIdAsync(request.SEG_id.Value, cancellationToken);
            if (seguro is null) throw new NotFoundException("El seguro asociado no existe.");
        }

        // Validar Extra (si aplica)
        if (request.EXT_id.HasValue)
        {
            var extra = await _extraDataService.ObtenerPorIdAsync(request.EXT_id.Value, cancellationToken);
            if (extra is null) throw new NotFoundException("El extra adicional asociado no existe.");
        }

        var dataModel = ReservaDetalleBusinessMapper.ToDataModel(request);
        var creado = await _detalleDataService.CrearAsync(dataModel, cancellationToken);

        return ReservaDetalleBusinessMapper.ToResponse(creado);
    }

    public async Task<ReservaDetalleResponse> ActualizarAsync(ActualizarReservaDetalleRequest request, CancellationToken cancellationToken = default)
    {
        var errors = ReservaDetalleValidator.ValidarActualizacion(request);
        if (errors.Any())
            throw new ValidationException("La solicitud de actualización de detalle es inválida.", errors);

        var existente = await _detalleDataService.ObtenerPorIdAsync(request.REX_id, cancellationToken);
        if (existente is null)
            throw new NotFoundException("No se encontró el detalle solicitado.");

        if (existente.RES_id != request.RES_id)
        {
            var reserva = await _reservaDataService.ObtenerPorIdAsync(request.RES_id, cancellationToken);
            if (reserva is null) throw new NotFoundException("La nueva reserva asociada no existe.");
        }

        if (request.SEG_id.HasValue && existente.SEG_id != request.SEG_id)
        {
            var seguro = await _seguroDataService.ObtenerPorIdAsync(request.SEG_id.Value, cancellationToken);
            if (seguro is null) throw new NotFoundException("El nuevo seguro asociado no existe.");
        }

        if (request.EXT_id.HasValue && existente.EXT_id != request.EXT_id)
        {
            var extra = await _extraDataService.ObtenerPorIdAsync(request.EXT_id.Value, cancellationToken);
            if (extra is null) throw new NotFoundException("El nuevo extra adicional asociado no existe.");
        }

        var dataModel = ReservaDetalleBusinessMapper.ToDataModel(request);

        dataModel.REX_fechaCreacion = existente.REX_fechaCreacion;
        dataModel.REX_usuarioCreacion = existente.REX_usuarioCreacion;

        var actualizado = await _detalleDataService.ActualizarAsync(dataModel, cancellationToken);
        if (actualizado is null)
            throw new NotFoundException("No se pudo actualizar el detalle.");

        return ReservaDetalleBusinessMapper.ToResponse(actualizado);
    }

    public async Task<ReservaDetalleResponse> ObtenerPorIdAsync(Guid detalleId, CancellationToken cancellationToken = default)
    {
        var detalle = await _detalleDataService.ObtenerPorIdAsync(detalleId, cancellationToken);
        if (detalle is null)
            throw new NotFoundException("No se encontró el detalle solicitado.");

        return ReservaDetalleBusinessMapper.ToResponse(detalle);
    }

    public async Task<IReadOnlyList<ReservaDetalleResponse>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var detalles = await _detalleDataService.ListarAsync(cancellationToken);
        return detalles.Select(ReservaDetalleBusinessMapper.ToResponse).ToList();
    }
}