using Microsoft.EntityFrameworkCore;

namespace Seguros.HttpApi.Dominio.Condutores
{
    public sealed class CondutorRepository(SegurosDbContext dbContext)
    {
        public async Task<Maybe<Condutor>> ObterPorCpfAsync(string cpf, CancellationToken cancellationToken)
        {
            return await dbContext.Condutores
                .FirstOrDefaultAsync(c => c.Cpf == cpf, cancellationToken);
        }

        public async Task AdicionarAsync(Condutor condutor, CancellationToken cancellationToken)
        {
            await dbContext.Condutores.AddAsync(condutor, cancellationToken);
        }

        public async Task AtualizarAsync(Condutor condutor, CancellationToken cancellationToken)
        {
            dbContext.Condutores.Update(condutor);
        }
    }
}
