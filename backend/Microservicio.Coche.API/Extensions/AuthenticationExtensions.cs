using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microservicios.Coche.Api.Models.Settings;

namespace Microservicios.Coche.Api.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // ✅ Lectura directa — más robusta que Bind() para evitar el IDX10703
        var secretKey = configuration["JwtSettings:SecretKey"];
        var issuer = configuration["JwtSettings:Issuer"];
        var audience = configuration["JwtSettings:Audience"];

        if (string.IsNullOrWhiteSpace(secretKey))
            throw new InvalidOperationException(
                "JwtSettings:SecretKey vacío. Verifica que appsettings.json esté configurado " +
                "con 'Copiar si es más reciente' en las propiedades del archivo.");

        // Registrar para IOptions<JwtSettings> (requerido por AuthController)
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false, // 🚨 Desactivado para debug
                ValidateAudience = false, // 🚨 Desactivado para debug
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ClockSkew = TimeSpan.Zero
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine("\n🚨 FALLO JWT: " + context.Exception.Message);
                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }
}