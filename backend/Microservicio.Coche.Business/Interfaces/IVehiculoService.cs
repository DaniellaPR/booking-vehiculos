
using Microservicios.Coche.Business.DTOs.Vehiculo;

namespace Microservicios.coche.Business.Interfaces;

public interface IVehiculoService
{
    Task<VehiculoResponse> CrearAsync(CrearVehiculoRequest request, CancellationToken cancellationToken = default);
    Task<VehiculoResponse> ActualizarAsync(ActualizarVehiculoRequest request, CancellationToken cancellationToken = default);
    Task<VehiculoResponse> ObtenerPorIdAsync(Guid vehiculoId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<VehiculoResponse>> ListarAsync(CancellationToken cancellationToken = default);
    Task EliminarAsync(Guid id, CancellationToken cancellationToken = default);
}