namespace Microservicios.Coche.Business.DTOs.Pago;

public class PagoResponse
{
    public Guid PAG_id { get; set; }
    public Guid RES_id { get; set; }
    public decimal PAG_monto { get; set; }
    public string PAG_metodo { get; set; } = null!;
    public string PAG_estado { get; set; } = null!;
    public DateTime? PAG_fechaPago { get; set; }
}