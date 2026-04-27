using System.Text.Json.Serialization;
namespace Microservicios.Coche.Business.DTOs.ExtraAdicional;

public class ActualizarExtraAdicionalRequest
{
    [JsonIgnore] public Guid EXT_id { get; set; }
    public string EXT_nombre { get; set; } = null!;
    public decimal EXT_costo { get; set; } // 🚨 Corregido aquí
    [JsonIgnore] public string? EXT_usuarioModificacion { get; set; }
}