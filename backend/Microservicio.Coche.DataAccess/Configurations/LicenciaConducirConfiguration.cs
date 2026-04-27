using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservicios.Coche.DataAccess.Configurations
{
    public class LicenciaConducirConfiguration : IEntityTypeConfiguration<LicenciaConducirEntity>
    {
        public void Configure(EntityTypeBuilder<LicenciaConducirEntity> builder)
        {
            // Nombre de la tabla en PostgreSQL (minúsculas)
            builder.ToTable("licenciaconducir");

            builder.HasKey(x => x.LIC_id);

            // 🚨 Forzar TODO a minúsculas para Postgres
            builder.Property(x => x.LIC_id)
                .HasColumnName("lic_id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(x => x.CLI_id)
                .HasColumnName("cli_id")
                .IsRequired();

            builder.Property(x => x.LIC_numero)
                .HasColumnName("lic_numero")
                .HasMaxLength(20)
                .IsRequired();

            // CORRECCIÓN: Usar la propiedad correcta (LIC_categoria) y mapearla a la columna correcta
            builder.Property(x => x.LIC_categoria)
                .HasColumnName("lic_categoria")
                .HasMaxLength(10)
                .IsRequired();

            // CORRECCIÓN: Propiedad correcta (LIC_vigencia)
            builder.Property(x => x.LIC_vigencia)
                .HasColumnName("lic_vigencia")
                .IsRequired();

            // Campos de auditoría (Forzados a minúsculas)
            builder.Property(x => x.LIC_fechaCreacion)
                .HasColumnName("lic_fechacreacion")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.LIC_usuarioCreacion)
                .HasColumnName("lic_usuariocreacion")
                .HasMaxLength(50);

            builder.Property(x => x.LIC_fechaModificacion)
                .HasColumnName("lic_fechamodificacion");

            builder.Property(x => x.LIC_usuarioModificacion)
                .HasColumnName("lic_usuariomodificacion")
                .HasMaxLength(50);

            // Relación opcional (Depende de si ClienteEntity tiene la lista o no)
            builder.HasOne(x => x.Cliente)
                .WithMany(c => c.Licencias) // Asume que tienes public virtual ICollection<LicenciaConducirEntity> Licencias en ClienteEntity
                .HasForeignKey(x => x.CLI_id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}