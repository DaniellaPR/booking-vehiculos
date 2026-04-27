using System;
using System.Collections.Generic;

namespace Microservicios.Coche.DataAccess.Entities
{
    public class SucursalEntity
    {
        public Guid SUC_id { get; set; }
        public string SUC_nombre { get; set; } = null!;
        public string SUC_ciudad { get; set; } = null!;
        public string SUC_direccion { get; set; } = null!;
        public string? SUC_coordenadas { get; set; }
        public DateTime? SUC_fechaCreacion { get; set; }
        public string? SUC_usuarioCreacion { get; set; }
        public DateTime? SUC_fechaModificacion { get; set; }
        public string? SUC_usuarioModificacion { get; set; }

        // Propiedades de navegación
        public virtual ICollection<HorarioAtencionEntity> HorariosAtencion { get; set; } = new List<HorarioAtencionEntity>();

        public virtual ICollection<VehiculoEntity> Vehiculos { get; set; } = new List<VehiculoEntity>();
    }
}