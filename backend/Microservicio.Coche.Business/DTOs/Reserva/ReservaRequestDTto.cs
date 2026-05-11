namespace Microservicios.Coche.Business.DTOs.Reserva;


public class ReservaRequestDto
{
	public Guid VehiculoId { get; set; }
	public Guid ClienteId { get; set; }
	public Guid SucursalRetiroId { get; set; }
	public Guid SucursalEntregaId { get; set; }
	public DateTime FechaRetiro { get; set; }
	public DateTime FechaEntrega { get; set; }
}