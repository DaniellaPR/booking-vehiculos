using Microservicios.Coche.Business.DTOs.Auth;
using Microservicios.Coche.Business.Exceptions;
using Microservicios.Coche.Business.Interfaces;

namespace Microservicios.Coche.Business.Services;

public class AuthService : IAuthService
{
    public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        await Task.Delay(100, cancellationToken);

        if (request.Correo == "admin" && request.Password == "admin")
        {
            return new LoginResponse
            {
                Token = "",
                USU_id = Guid.NewGuid(),
                Email = request.Correo,
                Rol = "ADMIN"
            };
        }

        // ✅ Cambiado de Exception a BusinessException → retorna 409
        throw new BusinessException("Credenciales incorrectas.");
    }
}