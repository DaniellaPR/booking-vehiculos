namespace Microservicios.Coche.Business.DTOs.HorarioAtencion;

public class HorarioAtencionResponse
{
    public Guid HOR_id { get; set; }
    public Guid SUC_id { get; set; }
    public string HOR_diaSemana { get; set; } = null!;
    public TimeSpan HOR_apertura { get; set; }
    public TimeSpan HOR_cierre { get; set; }
    public DateTime? HOR_fechaCreacion { get; set; }
    public string? HOR_usuarioCreacion { get; set; }
    public DateTime? HOR_fechaModificacion { get; set; }
    public string? HOR_usuarioModificacion { get; set; }
}