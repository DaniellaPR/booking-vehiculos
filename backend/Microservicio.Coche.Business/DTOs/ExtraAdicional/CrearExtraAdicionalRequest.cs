using System.Text.Json.Serialization;
namespace Microservicios.Coche.Business.DTOs.ExtraAdicional;

public class CrearExtraAdicionalRequest
{
    public string EXT_nombre { get; set; } = null!;
    public decimal EXT_costo { get; set; } // 🚨 Corregido aquí
    [JsonIgnore] public string? EXT_usuarioCreacion { get; set; }
}