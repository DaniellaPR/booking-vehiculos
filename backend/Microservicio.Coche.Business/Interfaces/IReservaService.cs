
using Microservicios.Coche.Business.DTOs.Reserva;

namespace Microservicios.Coche.Business.Interfaces;

public interface IReservaService
{
    Task<ReservaResponse> CrearAsync(CrearReservaRequest request, CancellationToken cancellationToken = default);
    Task<ReservaResponse> ActualizarAsync(ActualizarReservaRequest request, CancellationToken cancellationToken = default);
    Task<ReservaResponse> ObtenerPorIdAsync(Guid reservaId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ReservaResponse>> ListarAsync(CancellationToken cancellationToken = default);
    Task<ReservaResponseDto> CrearReservaAsync(ReservaRequestDto request, CancellationToken cancellationToken = default);
    Task IniciarAlquilerAsync(Guid reservaId, int kmSalida, CancellationToken cancellationToken = default);
    Task RegistrarDevolucionAsync(Guid reservaId, int kmEntrada, decimal cargoExtra, CancellationToken cancellationToken = default);
}