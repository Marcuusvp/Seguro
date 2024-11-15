﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seguros.HttpApi.Dominio.Infra.Mappings
{
    public class ProprietarioMap : IEntityTypeConfiguration<Proprietario>
    {
        public void Configure(EntityTypeBuilder<Proprietario> proprietario)
        {
            proprietario.ToTable("Proprietarios");
            proprietario.HasKey(p => p.Id);

            proprietario.Property(p => p.Id)
                        .ValueGeneratedNever();

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

            // Configuração do relacionamento um-para-muitos com Apolice
            proprietario.HasMany(p => p.Apolices)
                        .WithOne(a => a.Proprietario)
                        .HasForeignKey(a => a.ProprietarioId);
        }
    }
}
