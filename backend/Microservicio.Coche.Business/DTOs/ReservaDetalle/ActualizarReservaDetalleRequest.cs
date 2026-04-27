namespace Microservicios.Coche.Business.DTOs.ReservaDetalle;

public class ActualizarReservaDetalleRequest
{
    public Guid REX_id { get; set; }
    public Guid RES_id { get; set; }
    public Guid? SEG_id { get; set; }
    public Guid? EXT_id { get; set; }
    public int REX_cantidad { get; set; }
    public string? REX_usuarioModificacion { get; set; }
}