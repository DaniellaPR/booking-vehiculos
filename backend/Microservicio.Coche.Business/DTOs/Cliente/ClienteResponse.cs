namespace Microservicios.Coche.Business.DTOs.Cliente;

public class ClienteResponse
{
    public Guid CLI_id { get; set; }
    public string CLI_nombres { get; set; } = null!;
    public string CLI_apellidos { get; set; } = null!;
    public string CLI_cedula { get; set; } = null!;
    public string? CLI_telefono { get; set; }
    public DateTime? CLI_fechaCreacion { get; set; }
    public string? CLI_usuarioCreacion { get; set; }
    public DateTime? CLI_fechaModificacion { get; set; }
    public string? CLI_usuarioModificacion { get; set; }
}