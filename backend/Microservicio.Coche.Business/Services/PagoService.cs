using Microservicios.Coche.Business.DTOs.Pago;
using Microservicios.Coche.Business.Exceptions;
using Microservicios.Coche.Business.Interfaces;
using Microservicios.Coche.Business.Mappers;
using Microservicios.Coche.Business.Validators;
using Microservicios.Coche.DataManagement.Common;
using Microservicios.Coche.DataManagement.Interfaces;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.Business.Services;

public class PagoService : IPagoService
{
    private readonly IPagoDataService _pagoDataService;
    private readonly IReservaDataService _reservaDataService;

    // Inyectamos también ReservaDataService para cruzar datos y verificar que la reserva exista
    public PagoService(IPagoDataService pagoDataService, IReservaDataService reservaDataService)
    {
        _pagoDataService = pagoDataService;
        _reservaDataService = reservaDataService;
    }

    public async Task<PagoResponse> CrearAsync(CrearPagoRequest request, CancellationToken cancellationToken = default)
    {
        var errors = PagoValidator.ValidarCreacion(request);
        if (errors.Any()) throw new ValidationException("Solicitud de pago inválida", errors);

        var reserva = await _reservaDataService.ObtenerPorIdAsync(request.RES_id, cancellationToken);
        if (reserva is null) throw new NotFoundException("La reserva asociada no existe en el sistema.");

        var dataModel = PagoBusinessMapper.ToDataModel(request);
        var creado = await _pagoDataService.CrearAsync(dataModel, cancellationToken);

        return PagoBusinessMapper.ToResponse(creado);
    }

    public async Task<PagoResponse> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var pago = await _pagoDataService.ObtenerPorIdAsync(id, cancellationToken);
        if (pago is null) throw new NotFoundException("Pago no encontrado.");
        return PagoBusinessMapper.ToResponse(pago);
    }

    public async Task<IReadOnlyList<PagoResponse>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var pagos = await _pagoDataService.ListarAsync(cancellationToken);
        return pagos.Select(PagoBusinessMapper.ToResponse).ToList();
    }

    public async Task<DataPagedResult<PagoResponse>> BuscarAsync(PagoFiltroRequest request, CancellationToken cancellationToken = default)
    {
        var filtro = new PagoFiltroDataModel
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            RES_id = request.RES_id,
            PAG_estado = request.PAG_estado
        };

        var result = await _pagoDataService.BuscarAsync(filtro, cancellationToken);

        return new DataPagedResult<PagoResponse>
        {
            Items = result.Items.Select(PagoBusinessMapper.ToResponse).ToList(),
            TotalRecords = result.TotalRecords,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize
        };
    }
}