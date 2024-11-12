namespace Seguros.HttpApi.Dominio.Apolices
{
    public sealed class ApoliceRepository(SegurosDbContext dbContext)
    {
        public async Task Adicionar(Apolice apolice, CancellationToken cancellationToken)
        {
            await dbContext.Apolices.AddAsync(apolice, cancellationToken);
        }

        public async Task<Maybe<Apolice>> RecuperarApolice(Guid Id, CancellationToken cancellationToken)
        {
            return await dbContext.Apolices
                .Include(a => a.Proprietario)
                .Include(a => a.Condutores)
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }
    }
}
