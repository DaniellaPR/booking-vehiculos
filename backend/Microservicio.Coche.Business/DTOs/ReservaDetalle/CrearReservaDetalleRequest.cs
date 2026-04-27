namespace Microservicios.Coche.Business.DTOs.ReservaDetalle;

public class CrearReservaDetalleRequest
{
    public Guid RES_id { get; set; }
    public Guid? SEG_id { get; set; }
    public Guid? EXT_id { get; set; }
    public int REX_cantidad { get; set; } = 1;
    public string? REX_usuarioCreacion { get; set; }
}