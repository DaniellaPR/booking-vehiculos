namespace Microservicios.Coche.Business.DTOs.HorarioAtencion;

public class CrearHorarioAtencionRequest
{
    public Guid SUC_id { get; set; }
    public string HOR_diaSemana { get; set; } = null!;
    public TimeSpan HOR_apertura { get; set; }
    public TimeSpan HOR_cierre { get; set; }
    public string? HOR_usuarioCreacion { get; set; }
}