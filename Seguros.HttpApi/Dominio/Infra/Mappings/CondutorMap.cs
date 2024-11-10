using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Seguros.HttpApi.Dominio.Condutores;

namespace Seguros.HttpApi.Dominio.Infra.Mappings
{
    public class CondutorMap : IEntityTypeConfiguration<Condutor>
    {
        public void Configure(EntityTypeBuilder<Condutor> condutor)
        {
            condutor.HasKey(c => c.Cpf);

            condutor.Property(c => c.Cpf)
                    .HasColumnType("varchar(11)")
                    .HasMaxLength(11)
                    .IsRequired();

            condutor.Property(c => c.DataNascimento)
                    .IsRequired();

            condutor.Property<Guid>("ApoliceId"); // Chave estrangeira sombra

            condutor.OwnsOne(c => c.Residencia, endereco =>
            {
                endereco.Property(e => e.Uf)
                        .HasColumnType("varchar(2)")
                        .IsRequired();

                endereco.Property(e => e.Cidade)
                        .HasColumnType("varchar(100)")
                        .IsRequired();

                endereco.Property(e => e.Bairro)
                        .HasColumnType("varchar(100)")
                        .IsRequired();
            });
        }
    }
}
