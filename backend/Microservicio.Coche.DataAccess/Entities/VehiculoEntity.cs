using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema; // <-- 1. AGREGAR ESTO PARA LAS ETIQUETAS

namespace Microservicios.Coche.DataAccess.Entities
{
    public class VehiculoEntity
    {
        public Guid VEH_id { get; set; }
        public Guid CAT_id { get; set; }
        public Guid SUC_id { get; set; }
        public string VEH_placa { get; set; } = null!;
        public string VEH_modelo { get; set; } = null!;
        public int VEH_anio { get; set; }
        public string? VEH_color { get; set; }
        public decimal? VEH_kilometraje { get; set; }
        public string? VEH_estado { get; set; } 
        public DateTime? VEH_fechaCreacion { get; set; }
        public string? VEH_usuarioCreacion { get; set; }
        public DateTime? VEH_fechaModificacion { get; set; }
        public string? VEH_usuarioModificacion { get; set; }

        // ==========================================
        // Propiedades de navegación blindadas
        // ==========================================
            
        [ForeignKey("CAT_id")]
        public virtual CategoriaVehiculoEntity? Categoria { get; set; }

        [ForeignKey("SUC_id")] 
        public virtual SucursalEntity? Sucursal { get; set; }

        public virtual ICollection<MantenimientoEntity> Mantenimientos { get; set; } = new List<MantenimientoEntity>();
    }
}