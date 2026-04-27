namespace Microservicios.Coche.Business.DTOs.Vehiculo;

public class CrearVehiculoRequest
{
    public Guid CAT_id { get; set; }
    public Guid SUC_id { get; set; }
    public string VEH_placa { get; set; } = null!;
    public string VEH_modelo { get; set; } = null!;
    public int VEH_anio { get; set; }
    public string? VEH_color { get; set; }
    public decimal VEH_kilometraje { get; set; } = 0;
    public string VEH_estado { get; set; } = "Disponible";
    public string? VEH_usuarioCreacion { get; set; }
}