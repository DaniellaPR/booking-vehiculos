namespace Microservicios.Coche.Business.DTOs.Reserva;


public class DevolucionRequestDto
{
    public Guid AlquilerId { get; set; }
    public int KmEntrada { get; set; }
    public decimal CargoExtra { get; set; }
}