namespace Microservicios.Coche.Business.DTOs.Rol;

public class RolResponse
{
    public Guid ROL_id { get; set; }
    public string ROL_nombre { get; set; } = null!;
    public DateTime? ROL_fechaCreacion { get; set; }
    public string? ROL_usuarioCreacion { get; set; }
    public DateTime? ROL_fechaModificacion { get; set; }
    public string? ROL_usuarioModificacion { get; set; }
}