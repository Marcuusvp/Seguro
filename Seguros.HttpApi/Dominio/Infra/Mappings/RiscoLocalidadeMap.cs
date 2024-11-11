using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seguros.HttpApi.Dominio.RiscoPorLocalidade;

namespace Seguros.HttpApi.Dominio.Infra.Mappings
{
    public class RiscoLocalidadeMap : IEntityTypeConfiguration<RiscoLocalidade>
    {
        public void Configure(EntityTypeBuilder<RiscoLocalidade> builder)
        {
            builder.ToTable("RiscoPorLocalidade");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.UF)
                .IsRequired()
                .HasColumnType("varchar(2)")
                .HasMaxLength(2);

            builder.Property(r => r.Cidade)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(r => r.Bairro)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(r => r.NivelRisco)
                .IsRequired()
                .HasColumnType("varchar(15)")
                .HasMaxLength(10);

            builder.HasIndex(r => new { r.UF, r.Cidade, r.Bairro }).IsUnique();
        }
    }
}
