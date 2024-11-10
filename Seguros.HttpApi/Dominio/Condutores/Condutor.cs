using Seguros.HttpApi.Dominio.Apolices;

namespace Seguros.HttpApi.Dominio.Condutores;

public sealed class Condutor
{
    private readonly List<Apolice> _apolices;
    public Guid Id { get; }
    public string Cpf { get; }
    public DateOnly DataNascimento { get; }
    public Endereco Residencia { get; private set; }
    public IReadOnlyCollection<Apolice> Apolices => _apolices.AsReadOnly();

    private Condutor() { }
    private Condutor(string cpf, DateOnly dataNascimento, Endereco residencia)
    {
        Id = Guid.NewGuid();
        Cpf = cpf;
        DataNascimento = dataNascimento;
        Residencia = residencia;
        _apolices = new List<Apolice>();
    }

    public static Result<Condutor> Criar(string cpf, DateOnly dataNascimento, Endereco residencia)
    {
        return Result.Success(new Condutor(cpf, dataNascimento, residencia));
    }

    public void AtualizarEndereco(Endereco endereco)
    {
        Residencia = endereco;
    }

    public void AdicionarApolice(Apolice apolice)
    {
        _apolices.Add(apolice);
    }

    public void RemoverApolice(Apolice apolice)
    {
        _apolices.Remove(apolice);
    }
}

