using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservicios.Coche.DataAccess.Configurations
{
    public class SucursalConfiguration : IEntityTypeConfiguration<SucursalEntity>
    {
        public void Configure(EntityTypeBuilder<SucursalEntity> builder)
        {
            builder.ToTable("sucursal");

            builder.HasKey(x => x.SUC_id);

            builder.Property(x => x.SUC_id)
                .HasColumnName("suc_id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(x => x.SUC_nombre)
                .HasColumnName("suc_nombre")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.SUC_ciudad)
                .HasColumnName("suc_ciudad")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.SUC_direccion)
                .HasColumnName("suc_direccion")
                .IsRequired();

            builder.Property(x => x.SUC_coordenadas)
                .HasColumnName("suc_coordenadas")
                .HasMaxLength(50);

            builder.Property(x => x.SUC_fechaCreacion)
                .HasColumnName("suc_fechacreacion")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.SUC_usuarioCreacion)
                .HasColumnName("suc_usuariocreacion")
                .HasMaxLength(50)
                .HasDefaultValueSql("'system'");

            builder.Property(x => x.SUC_fechaModificacion)
                .HasColumnName("suc_fechamodificacion");

            builder.Property(x => x.SUC_usuarioModificacion)
                .HasColumnName("suc_usuariomodificacion")
                .HasMaxLength(50);
        }
    }
}