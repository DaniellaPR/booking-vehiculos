namespace Microservicios.Coche.Business.DTOs.Seguro;

public class SeguroResponse
{
    public Guid SEG_id { get; set; }
    public string SEG_nombre { get; set; } = null!;
    public decimal SEG_costoDiario { get; set; }
}