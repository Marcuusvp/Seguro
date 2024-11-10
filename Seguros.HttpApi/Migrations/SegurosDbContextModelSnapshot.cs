﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Seguros.HttpApi.Dominio.Infra;

#nullable disable

namespace Seguros.HttpApi.Migrations
{
    [DbContext(typeof(SegurosDbContext))]
    partial class SegurosDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApoliceCondutor", b =>
                {
                    b.Property<Guid>("ApoliceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CondutorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ApoliceId", "CondutorId");

                    b.HasIndex("CondutorId");

                    b.ToTable("ApoliceCondutor");
                });

            modelBuilder.Entity("Seguros.HttpApi.Dominio.Apolices.Apolice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProprietarioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ProprietarioId");

                    b.ToTable("Apolices", (string)null);
                });

            modelBuilder.Entity("Seguros.HttpApi.Dominio.Condutores.Condutor", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.Property<DateOnly>("DataNascimento")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("Condutores");
                });

            modelBuilder.Entity("Seguros.HttpApi.Dominio.Proprietarios.Proprietario", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.Property<DateOnly>("DataNascimento")
                        .HasColumnType("date");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Proprietarios", (string)null);
                });

            modelBuilder.Entity("ApoliceCondutor", b =>
                {
                    b.HasOne("Seguros.HttpApi.Dominio.Apolices.Apolice", null)
                        .WithMany()
                        .HasForeignKey("ApoliceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Seguros.HttpApi.Dominio.Condutores.Condutor", null)
                        .WithMany()
                        .HasForeignKey("CondutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Seguros.HttpApi.Dominio.Apolices.Apolice", b =>
                {
                    b.HasOne("Seguros.HttpApi.Dominio.Proprietarios.Proprietario", "Proprietario")
                        .WithMany("Apolices")
                        .HasForeignKey("ProprietarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Seguros.HttpApi.Dominio.Apolices.Endereco", "Endereco", b1 =>
                        {
                            b1.Property<Guid>("ApoliceId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Bairro")
                                .IsRequired()
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Cidade")
                                .IsRequired()
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Uf")
                                .IsRequired()
                                .HasColumnType("varchar(2)");

                            b1.HasKey("ApoliceId");

                            b1.ToTable("Apolices");

                            b1.WithOwner()
                                .HasForeignKey("ApoliceId");
                        });

                    b.OwnsOne("Seguros.HttpApi.Dominio.Apolices.Cobertura", "Cobertura", b1 =>
                        {
                            b1.Property<Guid>("ApoliceId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<bool>("Colisao")
                                .HasColumnType("bit");

                            b1.Property<bool>("Residencial")
                                .HasColumnType("bit");

                            b1.Property<bool>("RouboFurto")
                                .HasColumnType("bit");

                            b1.Property<bool>("Terceiros")
                                .HasColumnType("bit");

                            b1.HasKey("ApoliceId");

                            b1.ToTable("Apolices");

                            b1.WithOwner()
                                .HasForeignKey("ApoliceId");
                        });

                    b.OwnsOne("Seguros.HttpApi.Dominio.Apolices.Veiculo", "Veiculo", b1 =>
                        {
                            b1.Property<Guid>("ApoliceId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Ano")
                                .IsRequired()
                                .HasColumnType("varchar(25)");

                            b1.Property<string>("Marca")
                                .IsRequired()
                                .HasColumnType("varchar(50)");

                            b1.Property<string>("Modelo")
                                .IsRequired()
                                .HasColumnType("varchar(50)");

                            b1.Property<int>("Tipo")
                                .HasColumnType("int");

                            b1.HasKey("ApoliceId");

                            b1.ToTable("Apolices");

                            b1.WithOwner()
                                .HasForeignKey("ApoliceId");
                        });

                    b.Navigation("Cobertura")
                        .IsRequired();

                    b.Navigation("Endereco")
                        .IsRequired();

                    b.Navigation("Proprietario");

                    b.Navigation("Veiculo")
                        .IsRequired();
                });

            modelBuilder.Entity("Seguros.HttpApi.Dominio.Condutores.Condutor", b =>
                {
                    b.OwnsOne("Seguros.HttpApi.Dominio.Apolices.Endereco", "Residencia", b1 =>
                        {
                            b1.Property<Guid>("CondutorId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Bairro")
                                .IsRequired()
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Cidade")
                                .IsRequired()
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Uf")
                                .IsRequired()
                                .HasColumnType("varchar(2)");

                            b1.HasKey("CondutorId");

                            b1.ToTable("Condutores");

                            b1.WithOwner()
                                .HasForeignKey("CondutorId");
                        });

                    b.Navigation("Residencia")
                        .IsRequired();
                });

            modelBuilder.Entity("Seguros.HttpApi.Dominio.Proprietarios.Proprietario", b =>
                {
                    b.OwnsOne("Seguros.HttpApi.Dominio.Apolices.Endereco", "Residencia", b1 =>
                        {
                            b1.Property<Guid>("ProprietarioId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Bairro")
                                .IsRequired()
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Cidade")
                                .IsRequired()
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Uf")
                                .IsRequired()
                                .HasColumnType("varchar(2)");

                            b1.HasKey("ProprietarioId");

                            b1.ToTable("Proprietarios");

                            b1.WithOwner()
                                .HasForeignKey("ProprietarioId");
                        });

                    b.Navigation("Residencia")
                        .IsRequired();
                });

            modelBuilder.Entity("Seguros.HttpApi.Dominio.Proprietarios.Proprietario", b =>
                {
                    b.Navigation("Apolices");
                });
#pragma warning restore 612, 618
        }
    }
}
