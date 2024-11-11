namespace Seguros.HttpApi.Dominio.Apolices.Servicos;

public class CalculoValorSeguroService(RegrasRepository regrasRepository)
{
    public async Task<decimal> CalcularValorSeguroAsync(decimal valorVeiculo, int nivelRisco, List<string> coberturas)
    {
        decimal valorTotal = 0;

        var regrasCustoCobertura = await regrasRepository.ObterRegrasAsync<RegraCustoCobertura>("CustoCoberturas");

        var regrasAjusteRisco = await regrasRepository.ObterRegrasAsync<RegraAjusteNivelRisco>("AjusteNivelRisco");
        var ajuste = regrasAjusteRisco.FirstOrDefault(r => r.NivelRisco == nivelRisco)?.PercentualAjuste ?? 0;

        foreach (var cobertura in coberturas)
        {
            var regraCusto = regrasCustoCobertura.FirstOrDefault(r => r.Cobertura.Equals(cobertura, StringComparison.OrdinalIgnoreCase));
            if (regraCusto != null)
            {
                decimal custoCobertura = 0;

                if (regraCusto.PercentualCustoBase > 0)
                {
                    custoCobertura = valorVeiculo * (regraCusto.PercentualCustoBase / 100);
                }
                else
                {
                    custoCobertura = regraCusto.CustoFixo;
                }

                custoCobertura += custoCobertura * (ajuste / 100);

                valorTotal += custoCobertura;
            }
        }

        return valorTotal;
    }
}
