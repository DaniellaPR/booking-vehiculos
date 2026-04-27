
using Microservicios.Coche.Business.DTOs.Tarifa;

namespace Microservicios.Coche.Business.Interfaces;

public interface ITarifaService
{
    Task<TarifaResponse> CrearAsync(CrearTarifaRequest request, CancellationToken cancellationToken = default);
    Task<TarifaResponse> ActualizarAsync(ActualizarTarifaRequest request, CancellationToken cancellationToken = default);
    Task<TarifaResponse> ObtenerPorIdAsync(Guid tarifaId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TarifaResponse>> ListarAsync(CancellationToken cancellationToken = default);
    Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default);
}
