using System;

namespace Microservicios.Coche.DataAccess.Entities
{
    public class AuditoriaEntity
    {
        public Guid AUD_id { get; set; }
        public string AUD_nombreTabla { get; set; } = null!;
        public string AUD_operacion { get; set; } = null!;
        public string? AUD_usuario { get; set; }
        public DateTime? AUD_fecha { get; set; }
        public string? AUD_detalleJsonb { get; set; }
    }
}
