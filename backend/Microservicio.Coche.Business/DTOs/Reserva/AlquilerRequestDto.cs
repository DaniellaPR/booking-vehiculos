namespace Microservicios.Coche.Business.DTOs.Reserva;


public class AlquilerRequestDto
{
    public Guid ReservaId { get; set; }
    public int KmSalida { get; set; }
}