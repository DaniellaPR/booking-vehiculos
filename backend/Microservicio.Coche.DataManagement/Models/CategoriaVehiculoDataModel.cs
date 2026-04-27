namespace Microservicios.Coche.DataManagement.Models;

public class CategoriaVehiculoDataModel
{
    public Guid CAT_id { get; set; }
    public string CAT_nombre { get; set; } = null!;
    public string? CAT_descripcion { get; set; }
    public decimal CAT_costoBase { get; set; }
    public int CAT_capacidadPasajeros { get; set; }
    public int CAT_capacidadMaletas { get; set; }
    public string? CAT_tipoTransmision { get; set; }
    public DateTime? CAT_fechaCreacion { get; set; }
    public string? CAT_usuarioCreacion { get; set; }
    public DateTime? CAT_fechaModificacion { get; set; }
    public string? CAT_usuarioModificacion { get; set; }
}