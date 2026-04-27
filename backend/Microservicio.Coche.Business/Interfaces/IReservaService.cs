
using Microservicios.Coche.Business.DTOs.Reserva;

namespace Microservicios.Coche.Business.Interfaces;

public interface IReservaService
{
    Task<ReservaResponse> CrearAsync(CrearReservaRequest request, CancellationToken cancellationToken = default);
    Task<ReservaResponse> ActualizarAsync(ActualizarReservaRequest request, CancellationToken cancellationToken = default);
    Task<ReservaResponse> ObtenerPorIdAsync(Guid reservaId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ReservaResponse>> ListarAsync(CancellationToken cancellationToken = default);
}