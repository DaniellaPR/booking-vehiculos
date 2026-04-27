using System;

namespace Microservicios.Coche.DataManagement.Models;

public class LicenciaConducirDataModel
{
    public Guid LIC_id { get; set; }
    public Guid CLI_id { get; set; }
    public string LIC_numero { get; set; } = null!;
    public string LIC_categoria { get; set; } = null!;
    public DateTime LIC_vigencia { get; set; }
    public DateTime? LIC_fechaCreacion { get; set; }
    public string? LIC_usuarioCreacion { get; set; }
    public DateTime? LIC_fechaModificacion { get; set; }
    public string? LIC_usuarioModificacion { get; set; }
}