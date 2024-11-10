namespace Seguros.HttpApi.Dominio.Apolices.Servicos.Interfaces;

public interface IHistoricoAcidentesService
{
    Task<Result<int>> ObterQuantidadeAcidentesAsync(string cpf, CancellationToken cancellationToken);
}
