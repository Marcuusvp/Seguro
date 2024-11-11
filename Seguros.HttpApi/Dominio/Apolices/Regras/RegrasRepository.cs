using Newtonsoft.Json;

namespace Seguros.HttpApi.Dominio.Apolices.Regras;

public sealed class RegrasRepository(SegurosDbContext dbContext)
{
    public async Task<List<T>> ObterRegrasAsync<T>(string tipoRegra)
    {
        var regra = await dbContext.Set<RegraCalculo>()
            .FirstOrDefaultAsync(r => r.Tipo == tipoRegra && r.Ativa);

        return JsonConvert.DeserializeObject<List<T>>(regra.ConteudoJson);
    }
}
