using Microservicios.Coche.Business.DTOs.Rol;

namespace Microservicios.Coche.Business.Interfaces;

public interface IRolService
{
    Task<RolResponse> CrearAsync(CrearRolRequest request, CancellationToken cancellationToken = default);
    Task<RolResponse> ActualizarAsync(ActualizarRolRequest request, CancellationToken cancellationToken = default);
    Task<RolResponse> ObtenerPorIdAsync(Guid rolId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RolResponse>> ListarAsync(CancellationToken cancellationToken = default);
}