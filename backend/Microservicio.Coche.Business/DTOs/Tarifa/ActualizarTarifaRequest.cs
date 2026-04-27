namespace Microservicios.Coche.Business.DTOs.Tarifa;

public class ActualizarTarifaRequest
{
    public Guid TAR_id { get; set; }
    public Guid CAT_id { get; set; }
    public decimal TAR_precioDiario { get; set; }
    public string? TAR_usuarioModificacion { get; set; }
}