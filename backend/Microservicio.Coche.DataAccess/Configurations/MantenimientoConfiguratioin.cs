using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservicios.Coche.DataAccess.Configurations
{
    public class MantenimientoConfiguration : IEntityTypeConfiguration<MantenimientoEntity>
    {
        public void Configure(EntityTypeBuilder<MantenimientoEntity> builder)
        {
            builder.ToTable("mantenimiento");

            builder.HasKey(x => x.MAN_id);

            builder.Property(x => x.MAN_id)
                .HasColumnName("man_id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(x => x.VEH_id)
                .HasColumnName("man_vehiculoid")
                .IsRequired();

            builder.Property(x => x.MAN_fecha)
                .HasColumnName("man_fecha")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(x => x.MAN_descripcion)
                .HasColumnName("man_descripcion")
                .HasColumnType("text");

            builder.Property(x => x.MAN_costo)
                .HasColumnName("man_costo")
                .HasColumnType("numeric(10,2)");

            builder.Property(x => x.MAN_fechaCreacion)
                .HasColumnName("man_fechacreacion")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.MAN_usuarioCreacion)
                .HasColumnName("man_usuariocreacion")
                .HasMaxLength(50)
                .HasDefaultValueSql("'system'");

            builder.Property(x => x.MAN_fechaModificacion)
                .HasColumnName("man_fechamodificacion");

            builder.Property(x => x.MAN_usuarioModificacion)
                .HasColumnName("man_usuariomodificacion")
                .HasMaxLength(50);

            builder.HasOne(x => x.Vehiculo)
                .WithMany(v => v.Mantenimientos)
                .HasForeignKey(x => x.VEH_id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}