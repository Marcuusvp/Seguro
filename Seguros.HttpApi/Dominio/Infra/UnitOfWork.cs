using Seguros.HttpApi.Dominio.Infra.Interfaces;

namespace Seguros.HttpApi.Dominio.Infra
{
    public class UnitOfWork(SegurosDbContext _dbContext) : IUnitOfWork
    {
        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
