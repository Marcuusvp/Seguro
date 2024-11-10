namespace Seguros.HttpApi.Dominio.Proprietarios
{
    public sealed class ProprietarioRepository(SegurosDbContext dbContext)
    {
        public async Task<Maybe<Proprietario>> ObterPorCpfAsync(string cpf, CancellationToken cancellationToken)
        {
            return await dbContext.Proprietarios
                .FirstOrDefaultAsync(p => p.Cpf == cpf, cancellationToken);
        }
        public async Task AdicionarAsync(Proprietario proprietario, CancellationToken cancellationToken)
        {
            await dbContext.Proprietarios.AddAsync(proprietario, cancellationToken);
        }
        public async Task AtualizarAsync(Proprietario proprietario, CancellationToken cancellationToken)
        {
            dbContext.Proprietarios.Update(proprietario);
        }
    }
}
