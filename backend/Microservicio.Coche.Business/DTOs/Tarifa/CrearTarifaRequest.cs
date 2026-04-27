namespace Microservicios.Coche.Business.DTOs.Tarifa;

public class CrearTarifaRequest
{
    public Guid CAT_id { get; set; }
    public decimal TAR_precioDiario { get; set; }
    public string? TAR_usuarioCreacion { get; set; }
}