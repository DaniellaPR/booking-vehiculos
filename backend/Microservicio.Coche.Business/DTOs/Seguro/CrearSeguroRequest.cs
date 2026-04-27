using System.Text.Json.Serialization;
namespace Microservicios.Coche.Business.DTOs.Seguro;

public class CrearSeguroRequest
{
    public string SEG_nombre { get; set; } = null!;
    public decimal SEG_costoDiario { get; set; }
    [JsonIgnore] public string? SEG_usuarioCreacion { get; set; }
}