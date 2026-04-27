namespace Microservicios.Coche.Business.DTOs.Cliente;

public class ActualizarClienteRequest
{
    public Guid CLI_id { get; set; }
    public string CLI_nombres { get; set; } = null!;
    public string CLI_apellidos { get; set; } = null!;
    public string CLI_cedula { get; set; } = null!;
    public string? CLI_telefono { get; set; }
    public string? CLI_usuarioModificacion { get; set; }
}