using System.Text.Json.Serialization;

namespace Microservicios.Coche.Business.DTOs.Vehiculo;

public class VehiculoResponse
{
    [JsonPropertyName("id")]
    public Guid VEH_id { get; set; }

    [JsonIgnore]
    public Guid CAT_id { get; set; }

    [JsonIgnore]
    public Guid SUC_id { get; set; }

    [JsonPropertyName("placa")]
    public string VEH_placa { get; set; } = null!;

    [JsonPropertyName("modelo")]
    public string VEH_modelo { get; set; } = null!;

    [JsonPropertyName("anio")]
    public int VEH_anio { get; set; }

    [JsonPropertyName("color")]
    public string? VEH_color { get; set; }

    [JsonPropertyName("kilometraje")]
    public decimal VEH_kilometraje { get; set; }

    [JsonPropertyName("estado")]
    public string VEH_estado { get; set; } = null!;

    [JsonPropertyName("imagenUrl")]
    public string? VEH_imagenUrl { get; set; }

    [JsonIgnore]
    public DateTime? VEH_fechaCreacion { get; set; }

    [JsonIgnore]
    public string? VEH_usuarioCreacion { get; set; }

    [JsonIgnore]
    public DateTime? VEH_fechaModificacion { get; set; }

    [JsonIgnore]
    public string? VEH_usuarioModificacion { get; set; }
}