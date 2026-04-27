namespace Microservicios.Coche.Business.DTOs.Reserva;

public class ActualizarReservaRequest
{
    public Guid RES_id { get; set; }
    public Guid CLI_id { get; set; }
    public Guid RES_sucursalRetiroId { get; set; }
    public Guid RES_sucursalEntregaId { get; set; }
    public DateTime RES_fechaRetiro { get; set; }
    public DateTime RES_fechaEntrega { get; set; }
    public string RES_estado { get; set; } = null!;
    public string? RES_usuarioModificacion { get; set; }
}