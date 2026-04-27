using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservicios.Coche.DataAccess.Configurations
{
    public class HorarioAtencionConfiguration : IEntityTypeConfiguration<HorarioAtencionEntity>
    {
        public void Configure(EntityTypeBuilder<HorarioAtencionEntity> builder)
        {
            builder.ToTable("horarioatencion");

            builder.HasKey(x => x.HOR_id);

            builder.Property(x => x.HOR_id)
                .HasColumnName("hor_id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(x => x.SUC_id)
                .HasColumnName("suc_id")
                .IsRequired();

            builder.Property(x => x.HOR_diaSemana)
                .HasColumnName("hor_diasemana")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(x => x.HOR_apertura)
                .HasColumnName("hor_apertura")
                .HasColumnType("time")
                .IsRequired();

            builder.Property(x => x.HOR_cierre)
                .HasColumnName("hor_cierre")
                .HasColumnType("time")
                .IsRequired();

            builder.Property(x => x.HOR_fechaCreacion)
                .HasColumnName("hor_fechacreacion")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.HOR_usuarioCreacion)
                .HasColumnName("hor_usuariocreacion")
                .HasMaxLength(50)
                .HasDefaultValueSql("'system'");

            builder.Property(x => x.HOR_fechaModificacion)
                .HasColumnName("hor_fechamodificacion");

            builder.Property(x => x.HOR_usuarioModificacion)
                .HasColumnName("hor_usuariomodificacion")
                .HasMaxLength(50);

            // Foreign Key
            builder.HasOne(x => x.Sucursal)
                .WithMany(s => s.HorariosAtencion)
                .HasForeignKey(x => x.SUC_id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}