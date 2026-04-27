namespace Microservicios.Coche.Business.DTOs.UsuarioApp;

public class ActualizarUsuarioAppRequest
{
    public Guid USU_id { get; set; }
    public Guid ROL_id { get; set; }
    public string USU_email { get; set; } = null!;
    public string USU_passwordHash { get; set; } = null!;
    public string? USU_usuarioModificacion { get; set; }
}