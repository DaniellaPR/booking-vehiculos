using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Xml;

namespace Microservicios.Coche.DataAccess.Context
{
    public class CocheDbContext : DbContext
    {
        public CocheDbContext(DbContextOptions<CocheDbContext> options) : base(options)
        {
        }

        // DbSets (Agregaremos los restantes conforme avancemos, aquí declaro los 2 primeros)
        public DbSet<AuditoriaEntity> Auditorias { get; set; } = null!;
        public DbSet<RolEntity> Roles { get; set; } = null!;
        public DbSet<UsuarioAppEntity> UsuariosApp { get; set; } = null!;
        public DbSet<ClienteEntity> Clientes { get; set; } = null!;
        public DbSet<LicenciaConducirEntity> LicenciasConducir { get; set; } = null!;
        public DbSet<SucursalEntity> Sucursales { get; set; } = null!;
        public DbSet<HorarioAtencionEntity> HorariosAtencion { get; set; } = null!;
        public DbSet<CategoriaVehiculoEntity> CategoriasVehiculos { get; set; } = null!;
        public DbSet<VehiculoEntity> Vehiculos { get; set; } = null!;
        public DbSet<MantenimientoEntity> Mantenimientos { get; set; } = null!;
        public DbSet<SeguroEntity> Seguros { get; set; } = null!;
        public DbSet<ExtraAdicionalEntity> ExtrasAdicionales { get; set; } = null!;
        public DbSet<TarifaEntity> Tarifas { get; set; } = null!;
        public DbSet<ReservaEntity> Reservas { get; set; } = null!;
        public DbSet<ReservaDetalleEntity> ReservaDetalles { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplica automáticamente todas las clases Configuration que heredan de IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


    }

}
