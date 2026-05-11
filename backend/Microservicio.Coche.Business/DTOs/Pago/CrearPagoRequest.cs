namespace Microservicios.Coche.Business.DTOs.Pago;

public class CrearPagoRequest
{
    public Guid RES_id { get; set; }
    public decimal PAG_monto { get; set; }
    public string PAG_metodo { get; set; } = null!; // Ej: "TARJETA", "EFECTIVO"
    public string PAG_estado { get; set; } = "Completado";
}