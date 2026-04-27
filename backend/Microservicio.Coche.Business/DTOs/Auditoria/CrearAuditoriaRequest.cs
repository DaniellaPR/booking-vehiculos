namespace Microservicios.Coche.Business.DTOs.Auditoria;

public class CrearAuditoriaRequest
{
    public string AUD_nombreTabla { get; set; } = null!;
    public string AUD_operacion { get; set; } = null!;
    public string? AUD_usuario { get; set; }
    public string? AUD_detalleJsonb { get; set; }
}