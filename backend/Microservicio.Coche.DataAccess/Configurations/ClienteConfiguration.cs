using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservicios.Coche.DataAccess.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<ClienteEntity>
    {
        public void Configure(EntityTypeBuilder<ClienteEntity> builder)
        {
            builder.ToTable("cliente");

            builder.HasKey(x => x.CLI_id);

            builder.Property(x => x.CLI_id)
                .HasColumnName("cli_id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(x => x.CLI_nombres)
                .HasColumnName("cli_nombres")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.CLI_apellidos)
                .HasColumnName("cli_apellidos")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.CLI_cedula)
                .HasColumnName("cli_cedula")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(x => x.CLI_telefono)
                .HasColumnName("cli_telefono")
                .HasMaxLength(20);

            builder.Property(x => x.CLI_fechaCreacion)
                .HasColumnName("cli_fechacreacion")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.CLI_usuarioCreacion)
                .HasColumnName("cli_usuariocreacion")
                .HasMaxLength(50)
                .HasDefaultValueSql("'system'");

            builder.Property(x => x.CLI_fechaModificacion)
                .HasColumnName("cli_fechamodificacion");

            builder.Property(x => x.CLI_usuarioModificacion)
                .HasColumnName("cli_usuariomodificacion")
                .HasMaxLength(50);

            builder.HasIndex(x => x.CLI_cedula).IsUnique();
        }
    }
}