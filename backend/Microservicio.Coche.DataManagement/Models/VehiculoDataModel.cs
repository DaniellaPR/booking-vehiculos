using System;

namespace Microservicios.Coche.DataManagement.Models;

public class VehiculoDataModel
{
    public Guid VEH_id { get; set; }
    public Guid CAT_id { get; set; }
    public Guid SUC_id { get; set; }
    public string VEH_placa { get; set; } = null!;
    public string VEH_modelo { get; set; } = null!;
    public int VEH_anio { get; set; }
    public string? VEH_color { get; set; }
    public decimal? VEH_kilometraje { get; set; }
    public string? VEH_estado { get; set; }
    public DateTime? VEH_fechaCreacion { get; set; }
    public string? VEH_usuarioCreacion { get; set; }
    public DateTime? VEH_fechaModificacion { get; set; }
    public string? VEH_usuarioModificacion { get; set; }
}