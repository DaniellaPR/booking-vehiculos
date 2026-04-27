namespace Microservicios.Coche.DataManagement.Models;

public class SucursalDataModel
{
    public Guid SUC_id { get; set; }
    public string SUC_nombre { get; set; } = null!;
    public string SUC_ciudad { get; set; } = null!;
    public string SUC_direccion { get; set; } = null!;
    public string? SUC_coordenadas { get; set; }

    // 🚨 Corregido a DateTime? para coincidir con tu Entity
    public DateTime? SUC_fechaCreacion { get; set; }
    public string? SUC_usuarioCreacion { get; set; }
    public DateTime? SUC_fechaModificacion { get; set; }
    public string? SUC_usuarioModificacion { get; set; }
}