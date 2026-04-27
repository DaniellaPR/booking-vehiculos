using System;

namespace Microservicios.Coche.DataAccess.Entities
{
    public class ReservaDetalleEntity
    {
        public Guid REX_id { get; set; }
        public Guid RES_id { get; set; }
        public Guid? SEG_id { get; set; }
        public Guid? EXT_id { get; set; }
        public int? REX_cantidad { get; set; }
        public DateTime? REX_fechaCreacion { get; set; }
        public string? REX_usuarioCreacion { get; set; }
        public DateTime? REX_fechaModificacion { get; set; }
        public string? REX_usuarioModificacion { get; set; }

        // Propiedades de navegación
        public virtual ReservaEntity? Reserva { get; set; }
        public virtual SeguroEntity? Seguro { get; set; }
        public virtual ExtraAdicionalEntity? Extra { get; set; }
    }
}