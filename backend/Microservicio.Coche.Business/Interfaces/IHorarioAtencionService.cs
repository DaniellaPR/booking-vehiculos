
using Microservicios.Coche.Business.DTOs.HorarioAtencion;

namespace Microservicios.Coche.Business.Interfaces;

public interface IHorarioAtencionService
{
    Task<HorarioAtencionResponse> CrearAsync(CrearHorarioAtencionRequest request, CancellationToken cancellationToken = default);
    Task<HorarioAtencionResponse> ActualizarAsync(ActualizarHorarioAtencionRequest request, CancellationToken cancellationToken = default);
    Task<HorarioAtencionResponse> ObtenerPorIdAsync(Guid horarioId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<HorarioAtencionResponse>> ListarAsync(CancellationToken cancellationToken = default);
}