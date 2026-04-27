using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservicios.Coche.DataAccess.Configurations
{
    public class SeguroConfiguration : IEntityTypeConfiguration<SeguroEntity>
    {
        public void Configure(EntityTypeBuilder<SeguroEntity> builder)
        {
            builder.ToTable("seguro");

            builder.HasKey(x => x.SEG_id);

            builder.Property(x => x.SEG_id)
                .HasColumnName("seg_id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(x => x.SEG_nombre)
                .HasColumnName("seg_nombre")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.SEG_costoDiario)
                .HasColumnName("seg_costodiario")
                .HasColumnType("numeric(10,2)")
                .IsRequired();

            builder.Property(x => x.SEG_cobertura)
                .HasColumnName("seg_cobertura")
                .HasColumnType("text");

            builder.Property(x => x.SEG_fechaCreacion)
                .HasColumnName("seg_fechacreacion")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.SEG_usuarioCreacion)
                .HasColumnName("seg_usuariocreacion")
                .HasMaxLength(50)
                .HasDefaultValueSql("'system'");

            builder.Property(x => x.SEG_fechaModificacion)
                .HasColumnName("seg_fechamodificacion");

            builder.Property(x => x.SEG_usuarioModificacion)
                .HasColumnName("seg_usuariomodificacion")
                .HasMaxLength(50);
        }
    }
}