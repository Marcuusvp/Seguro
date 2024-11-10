using Seguros.HttpApi.Dominio.Infra;

namespace Seguros.HttpApi.Dominio.Apolices
{
    public sealed class ApoliceRepository(SegurosDbContext dbContext)
    {
        public async Task Adicionar(Apolice apolice, CancellationToken cancellationToken)
        {
            await dbContext.Apolices.AddAsync(apolice, cancellationToken);
        }
    }
}
