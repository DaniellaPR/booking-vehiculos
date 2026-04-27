namespace Microservicios.Coche.Business.DTOs.Reserva;

public class CrearReservaRequest
{
    public Guid CLI_id { get; set; }
    public Guid RES_sucursalRetiroId { get; set; }
    public Guid RES_sucursalEntregaId { get; set; }
    public DateTime RES_fechaRetiro { get; set; }
    public DateTime RES_fechaEntrega { get; set; }
    public string RES_estado { get; set; } = "Pendiente";
    public string? RES_usuarioCreacion { get; set; }
}