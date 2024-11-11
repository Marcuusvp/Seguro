using Seguros.HttpApi.Dominio.Infra.Mappings;
using Seguros.HttpApi.Dominio.RiscoPorLocalidade;

namespace Seguros.HttpApi.Dominio.Infra
{
    public class SegurosDbContext : DbContext
    {
        public SegurosDbContext(DbContextOptions<SegurosDbContext> options) : base(options) { }

        public DbSet<Apolice> Apolices { get; set; }
        public DbSet<Proprietario> Proprietarios { get; set; }
        public DbSet<Condutor> Condutores { get; set; }
        public DbSet<RegraCalculo> RegrasCalculo { get; set; }
        public DbSet<RiscoLocalidade> RiscoPorLocalidade { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApoliceMap());
            modelBuilder.ApplyConfiguration(new ProprietarioMap());
            modelBuilder.ApplyConfiguration(new CondutorMap());
            modelBuilder.ApplyConfiguration(new RegraCalculoMap());
            modelBuilder.ApplyConfiguration(new RiscoLocalidadeMap());
        }
    }
}
