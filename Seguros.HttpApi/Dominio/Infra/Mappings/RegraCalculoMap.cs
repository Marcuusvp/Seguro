using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seguros.HttpApi.Dominio.Infra.Mappings;

public class RegraCalculoMap : IEntityTypeConfiguration<RegraCalculo>
{
    public void Configure(EntityTypeBuilder<RegraCalculo> builder)
    {
        builder.ToTable("RegrasCalculo");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Nome)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasMaxLength(200);

        builder.Property(r => r.Tipo)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

        builder.Property(r => r.ConteudoJson)
            .HasColumnType("varchar(max)")
            .IsRequired();

        builder.Property(r => r.Ativa)
            .IsRequired();
    }
}
