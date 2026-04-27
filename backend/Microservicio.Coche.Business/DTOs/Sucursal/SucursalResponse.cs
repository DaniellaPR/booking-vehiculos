namespace Microservicios.Coche.Business.DTOs.Sucursal
{
    public class SucursalResponse
    {
        public Guid SUC_id { get; set; }
        public string SUC_nombre { get; set; } = null!;
        public string SUC_ciudad { get; set; } = null!;
        public string SUC_direccion { get; set; } = null!;
        public string? SUC_coordenadas { get; set; }

        // Campos de auditoría (opcionales para mostrar en la respuesta)
        public DateTime? SUC_fechaCreacion { get; set; }
        public string? SUC_usuarioCreacion { get; set; }
        public DateTime? SUC_fechaModificacion { get; set; }
        public string? SUC_usuarioModificacion { get; set; }
    }
}