using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Microservicios.Coche.Business.DTOs.Sucursal
{
    public class CrearSucursalRequest
    {
        [Required(ErrorMessage = "El nombre de la sucursal es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string SUC_nombre { get; set; } = null!;

        [Required(ErrorMessage = "La ciudad es obligatoria.")]
        [StringLength(50, ErrorMessage = "La ciudad no puede exceder los 50 caracteres.")]
        public string SUC_ciudad { get; set; } = null!;

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        public string SUC_direccion { get; set; } = null!;

        [StringLength(50, ErrorMessage = "Las coordenadas no pueden exceder los 50 caracteres.")]
        public string? SUC_coordenadas { get; set; }

        // Datos de auditoría inyectados por el Controller (No se piden en el JSON de Swagger)
        [JsonIgnore]
        public string? CreadoPorUsuario { get; set; }
        [JsonIgnore]
        public string? CreadoPorNombre { get; set; }
        [JsonIgnore]
        public string? CreadoDesdeIp { get; set; }
        [JsonIgnore]
        public string? CreadoDesdeServicio { get; set; }
        [JsonIgnore]
        public string? CreadoDesdeEquipo { get; set; }
    }
}