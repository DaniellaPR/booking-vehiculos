using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservicios.Coche.DataAccess.Configurations
{
    public class ExtraAdicionalConfiguration : IEntityTypeConfiguration<ExtraAdicionalEntity>
    {
        public void Configure(EntityTypeBuilder<ExtraAdicionalEntity> builder)
        {
            builder.ToTable("extraadicional");

            builder.HasKey(x => x.EXT_id);

            builder.Property(x => x.EXT_id)
                .HasColumnName("ext_id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(x => x.EXT_nombre)
                .HasColumnName("ext_nombre")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.EXT_costo)
                .HasColumnName("ext_costo")
                .HasColumnType("numeric(10,2)")
                .IsRequired();

            builder.Property(x => x.EXT_fechaCreacion)
                .HasColumnName("ext_fechacreacion")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.EXT_usuarioCreacion)
                .HasColumnName("ext_usuariocreacion")
                .HasMaxLength(50)
                .HasDefaultValueSql("'system'");

            builder.Property(x => x.EXT_fechaModificacion)
                .HasColumnName("ext_fechamodificacion");

            builder.Property(x => x.EXT_usuarioModificacion)
                .HasColumnName("ext_usuariomodificacion")
                .HasMaxLength(50);
        }
    }
}