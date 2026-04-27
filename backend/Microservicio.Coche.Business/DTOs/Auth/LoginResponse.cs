namespace Microservicios.Coche.Business.DTOs.Auth;

public class LoginResponse
{
    public string Token { get; set; } = null!;
    public Guid USU_id { get; set; }
    public string Email { get; set; } = null!;
    public string Rol { get; set; } = null!;
}