using Microservicios.Coche.Business.DTOs.Auth;

namespace Microservicios.Coche.Business.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
}