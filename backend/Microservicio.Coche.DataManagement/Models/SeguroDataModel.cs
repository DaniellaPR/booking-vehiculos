namespace Microservicios.Coche.DataManagement.Models;

public class SeguroDataModel
{
    public Guid SEG_id { get; set; }
    public string SEG_nombre { get; set; } = null!;
    public decimal SEG_costoDiario { get; set; }

    // 🚨 Corregido a DateTime? para coincidir con tu Entity
    public DateTime? SEG_fechaCreacion { get; set; }
    public string? SEG_usuarioCreacion { get; set; }
    public DateTime? SEG_fechaModificacion { get; set; }
    public string? SEG_usuarioModificacion { get; set; }
}