namespace Microservicios.Coche.Business.DTOs.CategoriaVehiculo;

public class CategoriaVehiculoResponse
{
    public Guid CAT_id { get; set; }
    public string CAT_nombre { get; set; } = null!;
    public string? CAT_descripcion { get; set; }
    public decimal CAT_costoBase { get; set; }
}