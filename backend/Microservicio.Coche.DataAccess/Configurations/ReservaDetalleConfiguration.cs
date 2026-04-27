using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservicios.Coche.DataAccess.Configurations
{
    public class ReservaDetalleConfiguration : IEntityTypeConfiguration<ReservaDetalleEntity>
    {
        public void Configure(EntityTypeBuilder<ReservaDetalleEntity> builder)
        {
            builder.ToTable("reservadetalle");

            builder.HasKey(x => x.REX_id);

            builder.Property(x => x.REX_id)
                .HasColumnName("rex_id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            // ✅ CORRECCIÓN: Nombre real de la columna FK
            builder.Property(x => x.RES_id)
                .HasColumnName("res_id")
                .IsRequired();

            // ✅ CORRECCIÓN: Nombre real de la columna FK
            builder.Property(x => x.SEG_id)
                .HasColumnName("seg_id");

            // ✅ CORRECCIÓN: Nombre real de la columna FK
            builder.Property(x => x.EXT_id)
                .HasColumnName("ext_id");

            builder.Property(x => x.REX_cantidad)
                .HasColumnName("rex_cantidad")
                .HasDefaultValue(1);

            builder.Property(x => x.REX_fechaCreacion)
                .HasColumnName("rex_fechacreacion")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.REX_usuarioCreacion)
                .HasColumnName("rex_usuariocreacion")
                .HasMaxLength(50)
                .HasDefaultValueSql("'system'");

            builder.Property(x => x.REX_fechaModificacion)
                .HasColumnName("rex_fechamodificacion");

            builder.Property(x => x.REX_usuarioModificacion)
                .HasColumnName("rex_usuariomodificacion")
                .HasMaxLength(50);

            // Relaciones
            builder.HasOne(x => x.Reserva)
                .WithMany(r => r.Detalles)
                .HasForeignKey(x => x.RES_id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Seguro)
                .WithMany()
                .HasForeignKey(x => x.SEG_id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Extra)
                .WithMany()
                .HasForeignKey(x => x.EXT_id)
                .OnDelete(DeleteBehavior.Restrict);

            // si
            builder.ToTable("reservadetalle", t =>
            {
                t.HasCheckConstraint("chk_rex_tipo", "\"seg_id\" IS NOT NULL OR \"ext_id\" IS NOT NULL");
            });
        }
    }
}