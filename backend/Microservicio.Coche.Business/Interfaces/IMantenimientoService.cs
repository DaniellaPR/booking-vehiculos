
using Microservicios.Coche.Business.DTOs.Mantenimiento;

namespace Microservicios.Coche.Business.Interfaces;

public interface IMantenimientoService
{
    Task<MantenimientoResponse> CrearAsync(CrearMantenimientoRequest request, CancellationToken cancellationToken = default);
    Task<MantenimientoResponse> ActualizarAsync(ActualizarMantenimientoRequest request, CancellationToken cancellationToken = default);
    Task<MantenimientoResponse> ObtenerPorIdAsync(Guid mantenimientoId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MantenimientoResponse>> ListarAsync(CancellationToken cancellationToken = default);
}