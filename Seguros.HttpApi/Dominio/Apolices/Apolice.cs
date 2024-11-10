using Seguros.HttpApi.Dominio.Apolices.Enums;
using Seguros.HttpApi.Dominio.Condutores;
using Seguros.HttpApi.Dominio.Proprietarios;

namespace Seguros.HttpApi.Dominio.Apolices;
public sealed class Apolice
{
    private Apolice() { }

    public Guid Id { get; }
    public Guid ProprietarioId { get; private set; }
    public Veiculo Veiculo { get; }
    public Proprietario Proprietario { get; }
    public List<Condutor> Condutores { get; }
    public Endereco Endereco {  get; }
    public Cobertura Cobertura { get; }
    public decimal ValorTotal { get; }
    public EApoliceStatus Status { get; }

    private Apolice(Veiculo veiculo, Proprietario proprietario, List<Condutor> condutores,  Endereco endereco, Cobertura cobertura, decimal valorTotal)
    {
        Id = new Guid();
        ProprietarioId = proprietario.Id;
        Veiculo = veiculo;
        Proprietario = proprietario;
        Condutores = condutores;
        Endereco = endereco;
        Cobertura = cobertura;
        Status = EApoliceStatus.EmAnalise;
        ValorTotal = valorTotal;
    }

    public static Result<Apolice> Criar(Veiculo veiculo, Proprietario proprietario, List<Condutor> condutores, Endereco endereco, Cobertura cobertura, decimal valorTotal)
    {
        return Result.Success(new Apolice(veiculo, proprietario, condutores, endereco, cobertura, valorTotal));
    }
}

public record Veiculo(string Marca, string Modelo, string Ano, ETipoVeiculo Tipo);
public record Endereco(string Uf, string Cidade, string Bairro);
public record Cobertura(bool RouboFurto, bool Colisao, bool Terceiros, bool Residencial);
