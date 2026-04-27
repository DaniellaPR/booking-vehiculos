using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microservicios.Coche.Api.Models.Settings;

namespace Microservicios.Coche.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController : ControllerBase
{
    private readonly JwtSettings _jwtSettings;

    // Ya no inyectamos IAuthService, solo las configuraciones del JWT
    public AuthController(IOptions<JwtSettings> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
    }

    // Creamos un DTO falso (Mock) solo para recibir los datos básicos en Swagger
    public class MockLoginRequest
    {
        public string Correo { get; set; } = "admin@localiza.com";
        public string Password { get; set; } = "123456";
    }

    [HttpPost("login")]
    [AllowAnonymous] // Importante para poder generar el token sin estar logueado
    public IActionResult Login([FromBody] MockLoginRequest request)
    {
        var expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes);

        // Creamos las características (Claims) del Token a mano
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, "admin_mock"),
            new Claim(JwtRegisteredClaimNames.UniqueName, "admin_mock"),
            new Claim(JwtRegisteredClaimNames.Email, request.Correo),
            new Claim("name", "Administrador de Prueba"),
            new Claim(ClaimTypes.Role, "ADMIN"), // ¡Este Rol es el que te dejará pasar!
            new Claim(ClaimTypes.Role, "VENDEDOR")
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

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        // Devolvemos la respuesta simulando tu ApiResponse
        return Ok(new
        {
            Success = true,
            Message = "Login exitoso (Modo Prueba).",
            Data = new
            {
                Token = tokenString,
                ExpirationUtc = expiration
            }
        });
    }
}