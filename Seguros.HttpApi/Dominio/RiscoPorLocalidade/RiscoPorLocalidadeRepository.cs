namespace Seguros.HttpApi.Dominio.RiscoPorLocalidade
{
    public class RiscoPorLocalidadeRepository(SegurosDbContext dbContext)
    {
        public async Task<string> ObterNivelRiscoLocalidadeAsync(string uf, string cidade, string bairro)
        {
            var riscoLocalidade = await dbContext.RiscoPorLocalidade
                .FirstOrDefaultAsync(r => r.UF == uf && r.Cidade == cidade && r.Bairro == bairro);

            if (riscoLocalidade != null)
                return riscoLocalidade.NivelRisco;

            return "Alto";
        }
    }
}
