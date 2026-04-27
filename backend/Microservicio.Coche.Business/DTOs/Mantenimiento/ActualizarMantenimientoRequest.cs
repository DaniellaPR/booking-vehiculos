namespace Microservicios.Coche.Business.DTOs.Mantenimiento;

public class ActualizarMantenimientoRequest
{
    public Guid MAN_id { get; set; }
    public Guid VEH_id { get; set; }
    public DateTime MAN_fecha { get; set; }
    public string? MAN_descripcion { get; set; }
    public decimal? MAN_costo { get; set; }
    public string? MAN_usuarioModificacion { get; set; }
}