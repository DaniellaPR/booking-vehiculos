using System;
using System.Collections.Generic;

namespace Microservicios.Coche.DataAccess.Entities
{
    public class ReservaEntity
    {
        public Guid RES_id { get; set; }
        public Guid CLI_id { get; set; }
        public Guid RES_sucursalRetiroId { get; set; }
        public Guid RES_sucursalEntregaId { get; set; }
        public DateTime RES_fechaRetiro { get; set; }
        public DateTime RES_fechaEntrega { get; set; }
        public string? RES_estado { get; set; }
        public DateTime? RES_fechaCreacion { get; set; }
        public string? RES_usuarioCreacion { get; set; }
        public DateTime? RES_fechaModificacion { get; set; }
        public string? RES_usuarioModificacion { get; set; }

        // Propiedades de navegación
        public virtual ClienteEntity? Cliente { get; set; }
        public virtual SucursalEntity? SucursalRetiro { get; set; }
        public virtual SucursalEntity? SucursalEntrega { get; set; }
        public virtual ICollection<ReservaDetalleEntity> Detalles { get; set; } = new List<ReservaDetalleEntity>();
    }
}