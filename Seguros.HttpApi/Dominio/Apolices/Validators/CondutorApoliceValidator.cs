namespace Seguros.HttpApi.Dominio.Apolices.Validators;

public class CondutorApoliceValidator : AbstractValidator<CondutorApolice>
{
    public CondutorApoliceValidator()
    {
        RuleFor(c => c.Cpf)
            .NotEmpty().WithMessage("O CPF do condutor deve ser informado")
            .Length(11).WithMessage("O CPF deve ter 11 caracteres")
            .Must(RegraCpf.IsCpf).WithMessage("Cpf inválido");

        RuleFor(c => c.DataNascimento)
            .NotEmpty().WithMessage("Data de nascimento deve ser informada")
            .Must(IdadeMinima.IdadeValida).WithMessage("Condutor deve ser maior de idade");

        RuleFor(c => c.Residencia)
            .NotNull().WithMessage("O endereço de residência deve ser informado")
            .SetValidator(new EnderecoApoliceValidator());
    }
}
