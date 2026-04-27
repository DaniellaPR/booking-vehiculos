using System;

namespace Microservicios.Coche.DataManagement.Models;

public class TarifaDataModel
{
    public Guid TAR_id { get; set; }
    public Guid CAT_id { get; set; }
    public decimal TAR_precioDiario { get; set; }
    public DateTime? TAR_fechaCreacion { get; set; }
    public string? TAR_usuarioCreacion { get; set; }
    public DateTime? TAR_fechaModificacion { get; set; }
    public string? TAR_usuarioModificacion { get; set; }
}