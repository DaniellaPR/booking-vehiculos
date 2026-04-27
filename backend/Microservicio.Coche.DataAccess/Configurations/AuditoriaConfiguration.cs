using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservicios.Coche.DataAccess.Configurations
{
    public class AuditoriaConfiguration : IEntityTypeConfiguration<AuditoriaEntity>
    {
        public void Configure(EntityTypeBuilder<AuditoriaEntity> builder)
        {
            builder.ToTable("auditoria");

            builder.HasKey(x => x.AUD_id);

            builder.Property(x => x.AUD_id)
                .HasColumnName("aud_id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(x => x.AUD_nombreTabla)
                .HasColumnName("aud_nombretabla")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.AUD_operacion)
                .HasColumnName("aud_operacion")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.AUD_usuario)
                .HasColumnName("aud_usuario")
                .HasMaxLength(100);

            builder.Property(x => x.AUD_fecha)
                .HasColumnName("aud_fecha")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.AUD_detalleJsonb)
                .HasColumnName("aud_detallejsonb")
                .HasColumnType("jsonb");
        }
    }
}
