namespace Microservicios.Coche.Business.DTOs.HorarioAtencion;

public class ActualizarHorarioAtencionRequest
{
    public Guid HOR_id { get; set; }
    public Guid SUC_id { get; set; }
    public string HOR_diaSemana { get; set; } = null!;
    public TimeSpan HOR_apertura { get; set; }
    public TimeSpan HOR_cierre { get; set; }
    public string? HOR_usuarioModificacion { get; set; }
}