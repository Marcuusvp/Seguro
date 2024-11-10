
namespace Seguros.HttpApi.Dominio.Apolices.CriarApolice;
public record CriarApoliceCommand(VeiculoApolice Veiculo,
        ProprietarioApolice Proprietario,
        List<CondutorApolice> Condutores,
        EnderecoApolice Endereco,
        CoberturaApolice Cobertura) : ICommand<Result<CriarApoliceResult>>;
public record CriarApoliceResult(Guid Id);

public class CreateApoliceCommandValidator : AbstractValidator<CriarApoliceCommand>
{
    public CreateApoliceCommandValidator()
    {
        RuleFor(p => p.Proprietario).NotEmpty().WithMessage("O Condutor deve ser informado");
        RuleFor(p => p.Condutores).NotEmpty().WithMessage("Um ou mais condutores devem ser informados");
        RuleFor(p => p.Endereco).NotEmpty().WithMessage("Endereço deve ser informado");
        RuleFor(p => p.Cobertura).NotEmpty().WithMessage("Informe os serviços de cobertura");
        RuleFor(p => p.Veiculo).NotEmpty().WithMessage("Veiculo deve ser informado");
    }
}
internal class CriarApoliceHandler : ICommandHandler<CriarApoliceCommand, Result<CriarApoliceResult>>
{
    public Task<Result<CriarApoliceResult>> Handle(CriarApoliceCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public record VeiculoApolice(string Marca, string Modelo, int Ano);
public record ProprietarioApolice(string Cpf, string Nome, DateOnly DataNascimento, EnderecoRequest Residencia);
public record CondutorApolice(string Cpf, DateOnly DataNascimento, EnderecoRequest Residencia);
public record EnderecoApolice(string Uf, string Cidade, string Bairro);
public record CoberturaApolice(bool RouboFurto, bool Colisao, bool Terceiros, bool Residencial);
