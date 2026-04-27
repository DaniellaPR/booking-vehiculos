namespace Microservicios.Coche.Business.DTOs.Rol;

public class ActualizarRolRequest
{
    public Guid ROL_id { get; set; }
    public string ROL_nombre { get; set; } = null!;
    public string? ROL_usuarioModificacion { get; set; }
}