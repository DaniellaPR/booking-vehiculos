using System;

namespace Microservicios.Coche.DataAccess.Entities
{
    public class ExtraAdicionalEntity
    {
        public Guid EXT_id { get; set; }
        public string EXT_nombre { get; set; } = null!;
        public decimal EXT_costo { get; set; }
        public DateTime? EXT_fechaCreacion { get; set; }
        public string? EXT_usuarioCreacion { get; set; }
        public DateTime? EXT_fechaModificacion { get; set; }
        public string? EXT_usuarioModificacion { get; set; }
    }
}