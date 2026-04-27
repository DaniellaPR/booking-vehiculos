namespace Microservicios.Coche.Business.DTOs.LicenciaConducir;

public class CrearLicenciaConducirRequest
{
    public Guid CLI_id { get; set; }
    public string LIC_numero { get; set; } = null!;
    public string LIC_categoria { get; set; } = null!;
    public DateTime LIC_vigencia { get; set; }
    public string? LIC_usuarioCreacion { get; set; }
}