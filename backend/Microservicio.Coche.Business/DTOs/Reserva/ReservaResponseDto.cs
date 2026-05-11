namespace Microservicios.Coche.Business.DTOs.Reserva;


public class ReservaResponseDto
{
    public Guid Id { get; set; }
    public Guid VehiculoId { get; set; }
    public string Estado { get; set; } = null!;
    public decimal Total { get; set; }
}