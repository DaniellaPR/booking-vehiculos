using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microservicios.Coche.DataAccess.Entities;

namespace Microservicios.Coche.DataAccess.Configurations;

public class PagoConfiguration : IEntityTypeConfiguration<PagoEntity>
{
    public void Configure(EntityTypeBuilder<PagoEntity> builder)
    {
        // El nombre exacto de la tabla
        builder.ToTable("pago");

        builder.HasKey(e => e.PAG_id);

        // Mapeo milimétrico respetando las mayúsculas del script SQL
        builder.Property(e => e.PAG_id).HasColumnName("PAG_id");
        builder.Property(e => e.RES_id).HasColumnName("RES_id");
        builder.Property(e => e.PAG_monto).HasColumnName("PAG_monto").HasColumnType("decimal(10,2)");
        builder.Property(e => e.PAG_metodo).HasColumnName("PAG_metodo");
        builder.Property(e => e.PAG_estado).HasColumnName("PAG_estado");
        builder.Property(e => e.PAG_fechaPago).HasColumnName("PAG_fechaPago");

        builder.HasOne(e => e.Reserva)
               .WithMany()
               .HasForeignKey(e => e.RES_id)
               .OnDelete(DeleteBehavior.Cascade);
    }
}