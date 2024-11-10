using Microsoft.EntityFrameworkCore;
using Seguros.HttpApi.Dominio.Apolices;
using Seguros.HttpApi.Dominio.Condutores;
using Seguros.HttpApi.Dominio.Infra.Mappings;
using Seguros.HttpApi.Dominio.Proprietarios;
using System;

namespace Seguros.HttpApi.Dominio.Infra
{
    public class SegurosDbContext : DbContext
    {
        public SegurosDbContext(DbContextOptions<SegurosDbContext> options) : base(options) { }

        public DbSet<Apolice> Apolices { get; set; }
        public DbSet<Proprietario> Proprietarios { get; set; }
        public DbSet<Condutor> Condutores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApoliceMap());
            modelBuilder.ApplyConfiguration(new ProprietarioMap());
            modelBuilder.ApplyConfiguration(new CondutorMap());
        }
    }
}
