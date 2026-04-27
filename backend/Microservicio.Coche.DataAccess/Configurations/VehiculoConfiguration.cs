using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservicios.Coche.DataAccess.Configurations
{
    public class VehiculoConfiguration : IEntityTypeConfiguration<VehiculoEntity>
    {
        public void Configure(EntityTypeBuilder<VehiculoEntity> builder)
        {
            // Nombre de la tabla en PostgreSQL (minúsculas)
            builder.ToTable("vehiculo");

            builder.HasKey(x => x.VEH_id);

            builder.Property(x => x.VEH_id)
                .HasColumnName("veh_id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            // 🚨 CORRECCIÓN 1: Llave foránea de Categoría
            builder.Property(x => x.CAT_id)
                .HasColumnName("cat_id")
                .IsRequired();

            // 🚨 CORRECCIÓN 2: Llave foránea de Sucursal
            builder.Property(x => x.SUC_id)
                .HasColumnName("suc_id")
                .IsRequired();

            builder.Property(x => x.VEH_placa)
                .HasColumnName("veh_placa")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(x => x.VEH_modelo)
                .HasColumnName("veh_modelo")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.VEH_anio)
                .HasColumnName("veh_anio")
                .IsRequired();

            builder.Property(x => x.VEH_color)
                .HasColumnName("veh_color")
                .HasMaxLength(30);

            builder.Property(x => x.VEH_kilometraje)
                .HasColumnName("veh_kilometraje")
                .HasColumnType("numeric(10,2)")
                .HasDefaultValue(0m);

            builder.Property(x => x.VEH_estado)
                .HasColumnName("veh_estado")
                .HasMaxLength(20)
                .HasDefaultValueSql("'Disponible'");

            // Auditoría
            builder.Property(x => x.VEH_fechaCreacion)
                .HasColumnName("veh_fechacreacion")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.VEH_usuarioCreacion)
                .HasColumnName("veh_usuariocreacion")
                .HasMaxLength(50)
                .HasDefaultValueSql("'system'");

            builder.Property(x => x.VEH_fechaModificacion)
                .HasColumnName("veh_fechamodificacion");

            builder.Property(x => x.VEH_usuarioModificacion)
                .HasColumnName("veh_usuariomodificacion")
                .HasMaxLength(50);

            builder.HasIndex(x => x.VEH_placa).IsUnique();

            // ==========================================
            // Relaciones (Foreign Keys exactas)
            // ==========================================

            builder.HasOne(x => x.Categoria)
                .WithMany(c => c.Vehiculos)
                .HasForeignKey(x => x.CAT_id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Sucursal)
                .WithMany(s => s.Vehiculos)
                .HasForeignKey(x => x.SUC_id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}