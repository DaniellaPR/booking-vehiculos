namespace Microservicios.Coche.Business.DTOs.ExtraAdicional;

public class ExtraAdicionalResponse
{
    public Guid EXT_id { get; set; }
    public string EXT_nombre { get; set; } = null!;
    public decimal EXT_costo { get; set; } // 🚨 Corregido aquí
}
