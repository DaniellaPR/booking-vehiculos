using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Microservicios.Coche.Business.DTOs.CategoriaVehiculo;

public class CrearCategoriaVehiculoRequest
{
    [Required] public string CAT_nombre { get; set; } = null!;
    public string? CAT_descripcion { get; set; }
    public decimal CAT_costoBase { get; set; }

    // 🚨 Campos de auditoría con prefijo exacto
    [JsonIgnore] public string? CAT_usuarioCreacion { get; set; }
    [JsonIgnore] public string? CAT_usuarioNombre { get; set; } // Opcional
    [JsonIgnore] public string? CAT_creadoDesdeIp { get; set; }
    [JsonIgnore] public string? CAT_creadoDesdeServicio { get; set; }
    [JsonIgnore] public string? CAT_creadoDesdeEquipo { get; set; }
}