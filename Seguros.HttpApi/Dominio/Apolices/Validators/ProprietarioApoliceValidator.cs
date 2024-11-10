namespace Seguros.HttpApi.Dominio.Apolices.Validators;

public class ProprietarioApoliceValidator : AbstractValidator<ProprietarioApolice>
{
    public ProprietarioApoliceValidator()
    {
        RuleFor(p => p.Cpf)
            .NotEmpty().WithMessage("O CPF do proprietário deve ser informado")
            .Length(11).WithMessage("O CPF deve ter 11 caracteres")
            .Must(RegraCpf.IsCpf).WithMessage("Cpf inválido");

        RuleFor(p => p.Nome)
            .NotEmpty().WithMessage("O nome do proprietário deve ser informado")
            .MaximumLength(100).WithMessage("O nome pode ter no máximo 100 caracteres");

        RuleFor(p => p.DataNascimento)
            .NotEmpty().WithMessage("Informe a data de nascimento do contratante")
            .Must(IdadeMinima.IdadeValida).WithMessage("Contratante não pode ser menor de idade");

        RuleFor(p => p.Residencia)
            .NotNull().WithMessage("O endereço de residência deve ser informado")
            .SetValidator(new EnderecoApoliceValidator());
    }
}

