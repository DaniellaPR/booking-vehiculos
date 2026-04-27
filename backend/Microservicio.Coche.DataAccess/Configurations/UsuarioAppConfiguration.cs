using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservicios.Coche.DataAccess.Configurations
{
    public class UsuarioAppConfiguration : IEntityTypeConfiguration<UsuarioAppEntity>
    {
        public void Configure(EntityTypeBuilder<UsuarioAppEntity> builder)
        {
            builder.ToTable("usuarioapp");

            builder.HasKey(x => x.USU_id);

            builder.Property(x => x.USU_id)
                .HasColumnName("usu_id")
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(x => x.ROL_id)
                .HasColumnName("rol_id")
                .IsRequired();

            builder.Property(x => x.USU_email)
                .HasColumnName("usu_email")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.USU_passwordHash)
                .HasColumnName("usu_passwordhash")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.USU_fechaCreacion)
                .HasColumnName("usu_fechacreacion")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.USU_usuarioCreacion)
                .HasColumnName("usu_usuariocreacion")
                .HasMaxLength(50)
                .HasDefaultValueSql("'system'");

            builder.Property(x => x.USU_fechaModificacion)
                .HasColumnName("usu_fechamodificacion");

            builder.Property(x => x.USU_usuarioModificacion)
                .HasColumnName("usu_usuariomodificacion")
                .HasMaxLength(50);

            builder.HasIndex(x => x.USU_email).IsUnique();

            builder.HasOne(x => x.Rol)
                .WithMany()
                .HasForeignKey(x => x.ROL_id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
