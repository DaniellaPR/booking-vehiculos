using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservicios.Coche.DataAccess.Configurations
{
    public class CategoriaVehiculoConfiguration : IEntityTypeConfiguration<CategoriaVehiculoEntity>
    {
        public void Configure(EntityTypeBuilder<CategoriaVehiculoEntity> builder)
        {
            builder.ToTable("categoriavehiculo");

            builder.HasKey(x => x.CAT_id);

            builder.Property(x => x.CAT_id)
                .HasColumnName("cat_id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(x => x.CAT_nombre)
                .HasColumnName("cat_nombre")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.CAT_descripcion)
                .HasColumnName("cat_descripcion")
                .HasColumnType("text");

            builder.Property(x => x.CAT_fechaCreacion)
                .HasColumnName("cat_fechacreacion")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.CAT_usuarioCreacion)
                .HasColumnName("cat_usuariocreacion")
                .HasMaxLength(50)
                .HasDefaultValueSql("'system'");

            builder.Property(x => x.CAT_fechaModificacion)
                .HasColumnName("cat_fechamodificacion");

            builder.Property(x => x.CAT_usuarioModificacion)
                .HasColumnName("cat_usuariomodificacion")
                .HasMaxLength(50);
            builder.Property(x => x.CAT_costoBase)
                .HasColumnName("cat_costobase")
                .HasColumnType("numeric(10,2)")
                .IsRequired();

            builder.Property(x => x.CAT_capacidadPasajeros)
                .HasColumnName("cat_capacidadpasajeros")
                .IsRequired();

            builder.Property(x => x.CAT_capacidadMaletas)
                .HasColumnName("cat_capacidadmaletas")
                .IsRequired();

            builder.Property(x => x.CAT_tipoTransmision)
                .HasColumnName("cat_tipotransmision")
                .HasMaxLength(20);
        }
    }
}