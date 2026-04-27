using System.Text.Json.Serialization;

namespace Microservicios.Coche.Business.DTOs.Seguro;

public class ActualizarSeguroRequest
{
    [JsonIgnore]
    public Guid SEG_id { get; set; }

    public string SEG_nombre { get; set; } = null!;

    public decimal SEG_costoDiario { get; set; }

    // 🚨 ESTA ES LA LÍNEA QUE FALTABA
    [JsonIgnore]
    public string? SEG_usuarioModificacion { get; set; }
}