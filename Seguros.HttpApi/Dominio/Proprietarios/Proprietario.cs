using Seguros.HttpApi.Dominio.Apolices;

namespace Seguros.HttpApi.Dominio.Proprietarios
{
    public sealed class Proprietario
    {
        private readonly List<Apolice> _apolices;
        public Guid Id { get; }
        public string Cpf { get; }
        public string Nome { get; }
        public DateOnly DataNascimento { get; }
        public Endereco Residencia { get; private set; }
        public IReadOnlyCollection<Apolice> Apolices => _apolices.AsReadOnly();

        private Proprietario() { }
        private Proprietario(string cpf, string nome, DateOnly dataNascimento, Endereco residencia)
        {
            Id = Guid.NewGuid();
            Cpf = cpf;
            Nome = nome;
            DataNascimento = dataNascimento;
            Residencia = residencia;
            _apolices = new List<Apolice>();
        }

        public static Result<Proprietario> Criar(string cpf, string nome, DateOnly dataNascimento, Endereco residencia)
        {
            return Result.Success(new Proprietario(cpf, nome, dataNascimento, residencia));
        }

        public void AtualizaEndereco(Endereco endereco)
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
}
