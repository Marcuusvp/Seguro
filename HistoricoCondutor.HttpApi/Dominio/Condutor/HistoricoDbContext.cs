using Microsoft.EntityFrameworkCore;

namespace HistoricoCondutor.HttpApi.Dominio.Condutor;

public class HistoricoContext : DbContext
{
    public HistoricoContext(DbContextOptions<HistoricoContext> options)
        : base(options)
    {
    }

    public DbSet<CondutorHistorico> CondutoresHistorico { get; set; }
}
