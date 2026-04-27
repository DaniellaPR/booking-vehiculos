namespace Microservicios.Coche.Business.DTOs.UsuarioApp;

public class CrearUsuarioAppRequest
{
    public Guid ROL_id { get; set; }
    public string USU_email { get; set; } = null!;
    public string USU_passwordHash { get; set; } = null!;
    public string? USU_usuarioCreacion { get; set; }
}