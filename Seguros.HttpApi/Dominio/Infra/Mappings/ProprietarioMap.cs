using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Seguros.HttpApi.Dominio.Proprietarios;

namespace Seguros.HttpApi.Dominio.Infra.Mappings
{
    public class ProprietarioMap : IEntityTypeConfiguration<Proprietario>
    {
        public void Configure(EntityTypeBuilder<Proprietario> proprietario)
        {
            proprietario.ToTable("Proprietarios");
            proprietario.HasKey(p => p.Cpf);

            proprietario.Property(p => p.Cpf)
                        .HasColumnType("varchar(11)")
                        .HasMaxLength(11)
                        .IsRequired();

            proprietario.Property(p => p.Nome)
                        .HasColumnType("varchar(100)")
                        .IsRequired();

            proprietario.Property(p => p.DataNascimento)
                        .IsRequired();

            proprietario.OwnsOne(p => p.Residencia, endereco =>
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
