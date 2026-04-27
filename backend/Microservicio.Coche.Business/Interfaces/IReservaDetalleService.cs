
using Microservicios.Coche.Business.DTOs.ReservaDetalle;

namespace Microservicios.Coche.Business.Interfaces;

public interface IReservaDetalleService
{
    Task<ReservaDetalleResponse> CrearAsync(CrearReservaDetalleRequest request, CancellationToken cancellationToken = default);
    Task<ReservaDetalleResponse> ActualizarAsync(ActualizarReservaDetalleRequest request, CancellationToken cancellationToken = default);
    Task<ReservaDetalleResponse> ObtenerPorIdAsync(Guid detalleId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ReservaDetalleResponse>> ListarAsync(CancellationToken cancellationToken = default);
}