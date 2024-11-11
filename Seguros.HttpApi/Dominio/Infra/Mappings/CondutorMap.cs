using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seguros.HttpApi.Dominio.Infra.Mappings
{
    public class CondutorMap : IEntityTypeConfiguration<Condutor>
    {
        public void Configure(EntityTypeBuilder<Condutor> condutor)
        {
            condutor.HasKey(c => c.Id);

            condutor.Property(c => c.Id)
                    .ValueGeneratedNever();

            condutor.Property(c => c.Cpf)
                    .HasColumnType("varchar(11)")
                    .HasMaxLength(11)
                    .IsRequired();

            condutor.Property(c => c.DataNascimento)
                    .IsRequired();

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

            // Configuração do relacionamento muitos-para-muitos com Apolice
            condutor.HasMany(c => c.Apolices)
                    .WithMany(a => a.Condutores)
                    .UsingEntity<Dictionary<string, object>>(
                        "ApoliceCondutor",
                        j => j.HasOne<Apolice>().WithMany().HasForeignKey("ApoliceId"),
                        j => j.HasOne<Condutor>().WithMany().HasForeignKey("CondutorId"));

        }
    }
}
