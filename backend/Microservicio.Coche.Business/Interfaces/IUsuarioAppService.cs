using Microservicios.Coche.Business.DTOs.UsuarioApp;

namespace Microservicios.Coche.Business.Interfaces;

public interface IUsuarioAppService
{
    Task<UsuarioAppResponse> CrearAsync(CrearUsuarioAppRequest request, CancellationToken cancellationToken = default);
    Task<UsuarioAppResponse> ActualizarAsync(ActualizarUsuarioAppRequest request, CancellationToken cancellationToken = default);
    Task<UsuarioAppResponse> ObtenerPorIdAsync(Guid usuarioId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<UsuarioAppResponse>> ListarAsync(CancellationToken cancellationToken = default);
    Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default);
}