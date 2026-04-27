using System;

namespace Microservicios.Coche.DataAccess.Entities
{
    public class SeguroEntity
    {
        public Guid SEG_id { get; set; }
        public string SEG_nombre { get; set; } = null!;
        public decimal SEG_costoDiario { get; set; }
        public string? SEG_cobertura { get; set; }
        public DateTime? SEG_fechaCreacion { get; set; }
        public string? SEG_usuarioCreacion { get; set; }
        public DateTime? SEG_fechaModificacion { get; set; }
        public string? SEG_usuarioModificacion { get; set; }
    }
}