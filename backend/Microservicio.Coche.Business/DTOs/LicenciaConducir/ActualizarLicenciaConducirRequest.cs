namespace Microservicios.Coche.Business.DTOs.LicenciaConducir;

public class ActualizarLicenciaConducirRequest
{
    public Guid LIC_id { get; set; }
    public Guid CLI_id { get; set; }
    public string LIC_numero { get; set; } = null!;
    public string LIC_categoria { get; set; } = null!;
    public DateTime LIC_vigencia { get; set; }
    public string? LIC_usuarioModificacion { get; set; }
}