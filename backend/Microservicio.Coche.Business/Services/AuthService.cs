using Microservicios.Coche.Business.DTOs.Auth;
using Microservicios.Coche.Business.Exceptions;
using Microservicios.Coche.Business.Interfaces;
using Microservicios.Coche.DataManagement.Interfaces;

namespace Microservicios.Coche.Business.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioAppDataService _usuarioDataService;
    private readonly IRolDataService _rolDataService;

    public AuthService(IUsuarioAppDataService usuarioDataService, IRolDataService rolDataService)
    {
        _usuarioDataService = usuarioDataService;
        _rolDataService = rolDataService;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        // 1. Buscar usuario por email
        var usuario = await _usuarioDataService.ObtenerPorEmailAsync(request.Correo, cancellationToken);

        // REGLA QA: Si no existe, lanzamos 401 (Unauthorized), NO 409 (Conflict) ni 400 (Bad Request).
        if (usuario == null)
            throw new UnauthorizedBusinessException("Credenciales incorrectas.");

        // 2. Comparar contraseña
        if (usuario.USU_passwordHash != request.Password)
            throw new UnauthorizedBusinessException("Credenciales incorrectas.");

        // 3. Obtener nombre del rol usando el ROL_id del usuario
        var rol = await _rolDataService.ObtenerPorIdAsync(usuario.ROL_id, cancellationToken);
        var rolNombre = rol?.ROL_nombre ?? "CLIENTE";

        return new LoginResponse
        {
            Token = string.Empty,
            USU_id = usuario.USU_id,
            Email = usuario.USU_email,
            Rol = rolNombre
        };
    }
}