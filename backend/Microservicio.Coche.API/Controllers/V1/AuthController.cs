using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microservicios.Coche.Api.Models.Common;
using Microservicios.Coche.Api.Models.Settings;
using Microservicios.Coche.Business.DTOs.Auth;
using Microservicios.Coche.Business.Interfaces;

namespace Microservicios.Coche.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly JwtSettings _jwtSettings;

    public AuthController(IAuthService authService, IOptions<JwtSettings> jwtOptions)
    {
        _authService = authService;
        _jwtSettings = jwtOptions.Value;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.Fail(
                "Datos inválidos.",
                ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));

        // 1. Validar credenciales contra la BD
        var result = await _authService.LoginAsync(request, ct);

        // 2. Generar JWT con el rol que viene de la BD
        var token = GenerarToken(result.USU_id, result.Email, result.Rol);

        return Ok(new
        {
            Success = true,
            Message = "Login exitoso.",
            Data = new
            {
                Token = token,
                result.Rol
            }
        });
    }

    private string GenerarToken(Guid usuarioId, string email, string rol)
    {
        var expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuarioId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim("name", email),
            new Claim(ClaimTypes.Role, rol),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}