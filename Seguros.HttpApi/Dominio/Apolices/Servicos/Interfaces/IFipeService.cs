namespace Seguros.HttpApi.Dominio.Apolices.Servicos.Interfaces
{
    public interface IFipeService
    {
        Task<Result<decimal>> ObterValorVeiculoAsync(string tipoVeiculo, string marca, string modelo, string ano, CancellationToken cancellationToken);
    }

}
