using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seguros.HttpApi.Dominio.Apolices;
using Seguros.HttpApi.Dominio.Condutores;
using Seguros.HttpApi.Dominio.Proprietarios;

namespace Seguros.HttpApi.Dominio.Infra.Mappings
{
    public class ApoliceMap : IEntityTypeConfiguration<Apolice>
    {
        public void Configure(EntityTypeBuilder<Apolice> apolice)
        {
            apolice.ToTable("Apolices");
            apolice.HasKey(a => a.Id);

            // Relacionamento com Proprietario
            apolice.HasOne(a => a.Proprietario)
                   .WithMany(p => p.Apolices)
                   .HasForeignKey(a => a.ProprietarioId)
                   .IsRequired();

            // Propriedades Possuídas (Owned Types)
            apolice.OwnsOne(a => a.Veiculo, veiculo =>
            {
                veiculo.Property(v => v.Marca)
                       .HasColumnType("varchar(50)")
                       .IsRequired();

                veiculo.Property(v => v.Modelo)
                       .HasColumnType("varchar(50)")
                       .IsRequired();

                veiculo.Property(v => v.Ano)
                       .IsRequired();
            });

            apolice.OwnsOne(a => a.Endereco, endereco =>
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

            apolice.OwnsOne(a => a.Cobertura, cobertura =>
            {
                cobertura.Property(c => c.RouboFurto).IsRequired();
                cobertura.Property(c => c.Colisao).IsRequired();
                cobertura.Property(c => c.Terceiros).IsRequired();
                cobertura.Property(c => c.Residencial).IsRequired();
            });

            // Relacionamento com Condutores
            apolice.HasMany(a => a.Condutores)
                  .WithMany(c => c.Apolices)
                  .UsingEntity<Dictionary<string, object>>(
                      "ApoliceCondutor",
                      j => j.HasOne<Condutor>().WithMany().HasForeignKey("CondutorId"),
                      j => j.HasOne<Apolice>().WithMany().HasForeignKey("ApoliceId"));
        }
    }
}
