namespace Microservicios.Coche.Business.DTOs.Pago;

public class PagoFiltroRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? RES_id { get; set; }
    public string? PAG_estado { get; set; }
}