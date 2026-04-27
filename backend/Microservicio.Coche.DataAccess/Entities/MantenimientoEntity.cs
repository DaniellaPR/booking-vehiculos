using System;

namespace Microservicios.Coche.DataAccess.Entities
{
    public class MantenimientoEntity
    {
        public Guid MAN_id { get; set; }
        public Guid VEH_id { get; set; }
        public DateTime MAN_fecha { get; set; }
        public string? MAN_descripcion { get; set; }
        public decimal? MAN_costo { get; set; }
        public DateTime? MAN_fechaCreacion { get; set; }
        public string? MAN_usuarioCreacion { get; set; }
        public DateTime? MAN_fechaModificacion { get; set; }
        public string? MAN_usuarioModificacion { get; set; }

        // Propiedad de navegación
        public virtual VehiculoEntity? Vehiculo { get; set; }
    }
}