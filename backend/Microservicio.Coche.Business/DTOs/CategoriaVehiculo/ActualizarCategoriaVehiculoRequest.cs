using System.Text.Json.Serialization;

namespace Microservicios.Coche.Business.DTOs.CategoriaVehiculo;

public class ActualizarCategoriaVehiculoRequest
{
    [JsonIgnore] public Guid CAT_id { get; set; }
    public string CAT_nombre { get; set; } = null!;
    public string? CAT_descripcion { get; set; }
    public decimal CAT_costoBase { get; set; }

    // 🚨 Campos de auditoría con prefijo exacto
    [JsonIgnore] public string? CAT_usuarioModificacion { get; set; }
    [JsonIgnore] public string? CAT_modificadoDesdeIp { get; set; }
    [JsonIgnore] public string? CAT_modificadoDesdeServicio { get; set; }
    [JsonIgnore] public string? CAT_modificadoDesdeEquipo { get; set; }
}