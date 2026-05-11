using Microservicios.Coche.Business.DTOs.Pago;
using Microservicios.Coche.DataManagement.Common;

namespace Microservicios.Coche.Business.Interfaces;

public interface IPagoService
{
    Task<PagoResponse> CrearAsync(CrearPagoRequest request, CancellationToken cancellationToken = default);
    Task<PagoResponse> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PagoResponse>> ListarAsync(CancellationToken cancellationToken = default);
    Task<DataPagedResult<PagoResponse>> BuscarAsync(PagoFiltroRequest request, CancellationToken cancellationToken = default);
}