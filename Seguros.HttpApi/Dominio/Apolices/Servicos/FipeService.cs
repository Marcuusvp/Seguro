using Newtonsoft.Json;
using Seguros.HttpApi.Dominio.Apolices.Servicos.Auxiliares;
using Seguros.HttpApi.Dominio.Apolices.Servicos.Interfaces;
using System.Globalization;

namespace Seguros.HttpApi.Dominio.Apolices.Servicos
{
    public class FipeService : IFipeService
    {
        private readonly HttpClient _httpClient;

        public FipeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Result<decimal>> ObterValorVeiculoAsync(string tipoVeiculo, string marca, string modelo, string ano, CancellationToken cancellationToken)
        {
            try
            {
                // 1. Obter a lista de marcas
                var marcasResponse = await _httpClient.GetAsync($"{tipoVeiculo}/marcas", cancellationToken);
                marcasResponse.EnsureSuccessStatusCode();
                var marcasJson = await marcasResponse.Content.ReadAsStringAsync(cancellationToken);
                var marcas = JsonConvert.DeserializeObject<List<FipeMarca>>(marcasJson);

                // 2. Encontrar o código da marca
                var marcaEncontrada = marcas.FirstOrDefault(m => m.Nome.Equals(marca, StringComparison.OrdinalIgnoreCase));
                if (marcaEncontrada == null)
                    return Result.Failure<decimal>("Marca não encontrada na tabela Fipe.");

                // 3. Obter a lista de modelos para a marca
                var modelosResponse = await _httpClient.GetAsync($"{tipoVeiculo}/marcas/{marcaEncontrada.Codigo}/modelos", cancellationToken);
                modelosResponse.EnsureSuccessStatusCode();
                var modelosJson = await modelosResponse.Content.ReadAsStringAsync(cancellationToken);
                var modelosResult = JsonConvert.DeserializeObject<FipeModelosResponse>(modelosJson);
                var modelos = modelosResult.Modelos;

                // 4. Encontrar o código do modelo
                var modeloEncontrado = modelos.FirstOrDefault(m => m.Nome.Equals(modelo, StringComparison.OrdinalIgnoreCase));
                if (modeloEncontrado == null)
                    return Result.Failure<decimal>("Modelo não encontrado na tabela Fipe.");

                // 5. Obter a lista de anos para o modelo
                var anosResponse = await _httpClient.GetAsync($"{tipoVeiculo}/marcas/{marcaEncontrada.Codigo}/modelos/{modeloEncontrado.Codigo}/anos", cancellationToken);
                anosResponse.EnsureSuccessStatusCode();
                var anosJson = await anosResponse.Content.ReadAsStringAsync(cancellationToken);
                var anos = JsonConvert.DeserializeObject<List<FipeAno>>(anosJson);

                // 6. Encontrar o código do ano
                var anoEncontrado = anos.FirstOrDefault(a => a.Nome.Contains(ano.ToString()));
                if (anoEncontrado == null)
                    return Result.Failure<decimal>("Ano não encontrado na tabela Fipe.");

                // 7. Obter o valor do veículo
                var valorResponse = await _httpClient.GetAsync($"{tipoVeiculo}/marcas/{marcaEncontrada.Codigo}/modelos/{modeloEncontrado.Codigo}/anos/{anoEncontrado.Codigo}", cancellationToken);
                valorResponse.EnsureSuccessStatusCode();
                var valorJson = await valorResponse.Content.ReadAsStringAsync(cancellationToken);
                var valorFipe = JsonConvert.DeserializeObject<FipeValorResponse>(valorJson);

                // 8. Converter o valor para decimal
                var valorLimpo = valorFipe.Valor.Replace("R$ ", "").Replace(".", "").Replace(",", ".");
                decimal valorDecimal = decimal.Parse(valorLimpo, CultureInfo.InvariantCulture);

                return Result.Success(valorDecimal);
            }
            catch (Exception ex)
            {
                return Result.Failure<decimal>("Erro ao consultar a tabela Fipe: " + ex.Message);
            }
        }
    }
}
