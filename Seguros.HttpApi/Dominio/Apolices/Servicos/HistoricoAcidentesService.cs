
using Newtonsoft.Json;

namespace Seguros.HttpApi.Dominio.Apolices.Servicos;

public class HistoricoAcidentesService : IHistoricoAcidentesService
{
    private readonly HttpClient _httpClient;

    public HistoricoAcidentesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<Result<int>> ObterQuantidadeAcidentesAsync(string cpf, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/historico/{cpf}", cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                var historico = JsonConvert.DeserializeObject<HistoricoAcidentesResponse>(content);
                return Result.Success(historico.QuantidadeAcidentes);
            }
            else
            {
                return Result.Failure<int>("Erro ao consultar o histórico de acidentes.");
            }
        }
        catch (Exception ex)
        {
            return Result.Failure<int>($"Exceção ao consultar o histórico de acidentes: {ex.Message}");
        }
    }
}
public class HistoricoAcidentesResponse
{
    public string Cpf { get; set; }
    public int QuantidadeAcidentes { get; set; }
}
