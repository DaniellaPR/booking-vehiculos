using System.ComponentModel.DataAnnotations;

namespace Microservicios.Coche.Business.DTOs.Cliente;

public class CrearClienteRequest
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
    public string CLI_nombres { get; set; } = null!;

    [Required(ErrorMessage = "Los apellidos son obligatorios")]
    [StringLength(100, ErrorMessage = "Los apellidos no pueden exceder 100 caracteres")]
    public string CLI_apellidos { get; set; } = null!;

    [Required(ErrorMessage = "La cédula es obligatoria")]
    [StringLength(15, MinimumLength = 10, ErrorMessage = "La cédula debe tener entre 10 y 15 caracteres")]
    public string CLI_cedula { get; set; } = null!;

    [Phone(ErrorMessage = "El teléfono tiene formato inválido")]
    [StringLength(20)]
    public string? CLI_telefono { get; set; }

    [StringLength(50)]
    public string? CLI_usuarioCreacion { get; set; }
}