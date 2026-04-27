using Microservicios.Coche.Business.DTOs.Auditoria;

namespace Microservicios.Coche.Business.Interfaces;

public interface IAuditoriaService
{
    Task<AuditoriaResponse> CrearAsync(CrearAuditoriaRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<AuditoriaResponse>> ListarAsync(CancellationToken cancellationToken = default);
}