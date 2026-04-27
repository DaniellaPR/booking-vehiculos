using System;
using System.Collections.Generic;

namespace Microservicios.Coche.DataAccess.Entities
{
    public class CategoriaVehiculoEntity
    {
        public Guid CAT_id { get; set; }
        public string CAT_nombre { get; set; } = null!;
        public string? CAT_descripcion { get; set; }

        // 👇 ESTOS SON LOS 4 ATRIBUTOS QUE TE FALTABAN Y CAUSABAN EL ERROR
        public decimal CAT_costoBase { get; set; }
        public int CAT_capacidadPasajeros { get; set; }
        public int CAT_capacidadMaletas { get; set; }
        public string? CAT_tipoTransmision { get; set; }

        // Auditoría
        public DateTime? CAT_fechaCreacion { get; set; }
        public string? CAT_usuarioCreacion { get; set; }
        public DateTime? CAT_fechaModificacion { get; set; }
        public string? CAT_usuarioModificacion { get; set; }

        // Propiedades de navegación
        public virtual ICollection<VehiculoEntity> Vehiculos { get; set; } = new List<VehiculoEntity>();
    }
}