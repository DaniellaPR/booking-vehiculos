using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Microservicios.Coche.Business.DTOs.Sucursal
{
    public class ActualizarSucursalRequest
    {
        [JsonIgnore] // El ID viene por la URL, no por el Body
        public Guid SucursalId { get; set; }

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
        public string? ModificadoPorUsuario { get; set; }
        [JsonIgnore]
        public string? ModificadoPorNombre { get; set; }
        [JsonIgnore]
        public string? ModificadoDesdeIp { get; set; }
        [JsonIgnore]
        public string? ModificadoDesdeServicio { get; set; }
        [JsonIgnore]
        public string? ModificadoDesdeEquipo { get; set; }
    }
}