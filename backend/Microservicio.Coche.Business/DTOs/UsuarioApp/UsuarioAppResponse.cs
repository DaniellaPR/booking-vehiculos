namespace Microservicios.Coche.Business.DTOs.UsuarioApp;

public class UsuarioAppResponse
{
    public Guid USU_id { get; set; }
    public Guid ROL_id { get; set; }
    public string USU_email { get; set; } = null!;
    // Nota de Arquitectura: Normalmente no se expone el Hash en la respuesta, 
    // pero lo incluimos para mantener estricta paridad con tu DataModel si lo requieres.
    public string USU_passwordHash { get; set; } = null!;
    public DateTime? USU_fechaCreacion { get; set; }
    public string? USU_usuarioCreacion { get; set; }
    public DateTime? USU_fechaModificacion { get; set; }
    public string? USU_usuarioModificacion { get; set; }
}