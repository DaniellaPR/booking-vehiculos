using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservicios.Coche.DataAccess.Configurations
{
    public class ReservaConfiguration : IEntityTypeConfiguration<ReservaEntity>
    {
        public void Configure(EntityTypeBuilder<ReservaEntity> builder)
        {
            // Nombre de la tabla en minúsculas para Postgres
            builder.ToTable("reserva");

            builder.HasKey(x => x.RES_id);

            builder.Property(x => x.RES_id)
                .HasColumnName("res_id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            // 🚨 CORRECCIÓN AQUÍ: Estaba "res_clienteid", debe ser "cli_id"
            builder.Property(x => x.CLI_id)
                .HasColumnName("cli_id")
                .IsRequired();

            builder.Property(x => x.RES_sucursalRetiroId)
                .HasColumnName("res_sucursalretiroid")
                .IsRequired();

            builder.Property(x => x.RES_sucursalEntregaId)
                .HasColumnName("res_sucursalentregaid")
                .IsRequired();

            builder.Property(x => x.RES_fechaRetiro)
                .HasColumnName("res_fecharetiro")
                .IsRequired();

            builder.Property(x => x.RES_fechaEntrega)
                .HasColumnName("res_fechaentrega")
                .IsRequired();

            builder.Property(x => x.RES_estado)
                .HasColumnName("res_estado")
                .HasMaxLength(20)
                .HasDefaultValueSql("'Pendiente'");

            builder.Property(x => x.RES_fechaCreacion)
                .HasColumnName("res_fechacreacion")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.RES_usuarioCreacion)
                .HasColumnName("res_usuariocreacion")
                .HasMaxLength(50)
                .HasDefaultValueSql("'system'");

            builder.Property(x => x.RES_fechaModificacion)
                .HasColumnName("res_fechamodificacion");

            builder.Property(x => x.RES_usuarioModificacion)
                .HasColumnName("res_usuariomodificacion")
                .HasMaxLength(50);

            // ==========================================
            // Relaciones (Foreign Keys)
            // ==========================================

            // Relación con Cliente
            builder.HasOne(x => x.Cliente)
                .WithMany() // No navegamos hacia atrás para no sobrecargar el cliente
                .HasForeignKey(x => x.CLI_id)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación con Sucursal (Retiro)
            builder.HasOne(x => x.SucursalRetiro)
                .WithMany()
                .HasForeignKey(x => x.RES_sucursalRetiroId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación con Sucursal (Entrega)
            builder.HasOne(x => x.SucursalEntrega)
                .WithMany()
                .HasForeignKey(x => x.RES_sucursalEntregaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}