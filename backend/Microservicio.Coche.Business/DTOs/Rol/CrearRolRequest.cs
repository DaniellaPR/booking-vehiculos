namespace Microservicios.Coche.Business.DTOs.Rol;

public class CrearRolRequest
{
    public string ROL_nombre { get; set; } = null!;
    public string? ROL_usuarioCreacion { get; set; }
}