namespace Microservicios.Coche.Business.DTOs.ReservaDetalle;

public class ReservaDetalleResponse
{
    public Guid REX_id { get; set; }
    public Guid RES_id { get; set; }
    public Guid? SEG_id { get; set; }
    public Guid? EXT_id { get; set; }
    public int REX_cantidad { get; set; }
    public DateTime? REX_fechaCreacion { get; set; }
    public string? REX_usuarioCreacion { get; set; }
    public DateTime? REX_fechaModificacion { get; set; }
    public string? REX_usuarioModificacion { get; set; }
}