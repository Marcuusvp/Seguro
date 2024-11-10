namespace Seguros.HttpApi.Dominio.Infra.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
