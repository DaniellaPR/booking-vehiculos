namespace Microservicios.Coche.Business.DTOs.Auth;
public class LoginRequest {
    public string Correo { get; set; } = null!;
    public string Password { get; set; } = null!;
}