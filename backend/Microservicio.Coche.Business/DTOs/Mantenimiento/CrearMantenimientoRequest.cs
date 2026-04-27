namespace Microservicios.Coche.Business.DTOs.Mantenimiento;

public class CrearMantenimientoRequest
{
    public Guid VEH_id { get; set; }
    public DateTime MAN_fecha { get; set; }
    public string? MAN_descripcion { get; set; }
    public decimal? MAN_costo { get; set; }
    public string? MAN_usuarioCreacion { get; set; }
}