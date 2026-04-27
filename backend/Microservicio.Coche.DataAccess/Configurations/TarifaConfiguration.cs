using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservicios.Coche.DataAccess.Configurations
{
    public class TarifaConfiguration : IEntityTypeConfiguration<TarifaEntity>
    {
        public void Configure(EntityTypeBuilder<TarifaEntity> builder)
        {
            builder.ToTable("tarifa");

            builder.HasKey(x => x.TAR_id);

            builder.Property(x => x.TAR_id)
                .HasColumnName("tar_id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(x => x.CAT_id)
                .HasColumnName("cat_id")
                .IsRequired();

            builder.Property(x => x.TAR_precioDiario)
                .HasColumnName("tar_preciodiario")
                .HasColumnType("numeric(10,2)")
                .IsRequired();

            builder.Property(x => x.TAR_fechaCreacion)
                .HasColumnName("tar_fechacreacion")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.TAR_usuarioCreacion)
                .HasColumnName("tar_usuariocreacion")
                .HasMaxLength(50)
                .HasDefaultValueSql("'system'");

            builder.Property(x => x.TAR_fechaModificacion)
                .HasColumnName("tar_fechamodificacion");

            builder.Property(x => x.TAR_usuarioModificacion)
                .HasColumnName("tar_usuariomodificacion")
                .HasMaxLength(50);

            // Relación con Categoría
            builder.HasOne(x => x.Categoria)
                .WithMany() // No navegamos desde categoría hacia tarifa para evitar bucles innecesarios
                .HasForeignKey(x => x.CAT_id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}