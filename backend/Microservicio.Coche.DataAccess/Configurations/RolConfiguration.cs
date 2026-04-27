using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservicios.Coche.DataAccess.Configurations
{
    public class RolConfiguration : IEntityTypeConfiguration<RolEntity>
    {
        public void Configure(EntityTypeBuilder<RolEntity> builder)
        {
            builder.ToTable("rol");

            builder.HasKey(x => x.ROL_id);

            builder.Property(x => x.ROL_id)
                .HasColumnName("rol_id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(x => x.ROL_nombre)
                .HasColumnName("rol_nombre")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.ROL_fechaCreacion)
                .HasColumnName("rol_fechacreacion")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.ROL_usuarioCreacion)
                .HasColumnName("rol_usuariocreacion")
                .HasMaxLength(50)
                .HasDefaultValueSql("'system'");

            builder.Property(x => x.ROL_fechaModificacion)
                .HasColumnName("rol_fechamodificacion");

            builder.Property(x => x.ROL_usuarioModificacion)
                .HasColumnName("rol_usuariomodificacion")
                .HasMaxLength(50);

            builder.HasIndex(x => x.ROL_nombre).IsUnique();
        }
    }
}
