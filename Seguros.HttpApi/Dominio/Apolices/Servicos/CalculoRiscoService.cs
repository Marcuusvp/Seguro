namespace Seguros.HttpApi.Dominio.Apolices.Servicos;

public class CalculoRiscoService(RegrasRepository regrasRepository)
{
    public async Task<int> CalcularNivelRiscoAsync(Condutor condutor, int quantidadeAcidentes, string nivelRiscoLocalidade)
    {
        int idadeCondutor = CalcularIdade(condutor.DataNascimento);
        int pontuacaoTotal = 0;

        var regrasIdade = await regrasRepository.ObterRegrasAsync<RegraPontuacaoIdade>("PontuacaoIdade");
        var regraIdade = regrasIdade.FirstOrDefault(r => idadeCondutor >= r.IdadeMinima && idadeCondutor <= r.IdadeMaxima);
        if (regraIdade != null)
            pontuacaoTotal += regraIdade.Pontuacao;

        var regrasAcidentes = await regrasRepository.ObterRegrasAsync<RegraPontuacaoAcidentes>("PontuacaoAcidentes");
        var regraAcidentes = regrasAcidentes.FirstOrDefault(r => quantidadeAcidentes >= r.AcidentesMinimos && quantidadeAcidentes <= r.AcidentesMaximos);
        if (regraAcidentes != null)
            pontuacaoTotal += regraAcidentes.Pontuacao;

        var regrasLocalidade = await regrasRepository.ObterRegrasAsync<RegraPontuacaoLocalidade>("PontuacaoLocalidade");
        var regraLocalidade = regrasLocalidade.FirstOrDefault(r => r.NivelRiscoLocalidade.Equals(nivelRiscoLocalidade, StringComparison.OrdinalIgnoreCase));
        if (regraLocalidade != null)
            pontuacaoTotal += regraLocalidade.Pontuacao;

        var regrasClassificacao = await regrasRepository.ObterRegrasAsync<RegraClassificacaoRisco>("ClassificacaoRisco");
        var classificacao = regrasClassificacao.FirstOrDefault(r => pontuacaoTotal >= r.PontuacaoMinima && pontuacaoTotal <= r.PontuacaoMaxima);
        if (classificacao != null)
            return classificacao.NivelRisco;

        //Retorna nivel maximo caso não encontre regra
        return 5;
    }

    private int CalcularIdade(DateOnly dataNasc)
    {
        int idade = DateTime.Now.Year - dataNasc.Year;
        if (DateTime.Now.DayOfYear < dataNasc.DayOfYear)
        {
            idade = idade - 1;
        }
        return idade;
    }
}
