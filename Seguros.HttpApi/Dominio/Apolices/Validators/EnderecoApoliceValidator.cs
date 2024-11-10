using Seguros.HttpApi.Dominio.Apolices.CriarApolice;

namespace Seguros.HttpApi.Dominio.Apolices.Validators;

public class EnderecoApoliceValidator : AbstractValidator<EnderecoApolice>
{
    public EnderecoApoliceValidator()
    {
        RuleFor(e => e.Uf)
            .NotEmpty().WithMessage("A UF deve ser informada")
            .Length(2).WithMessage("A UF deve ter 2 caracteres");

        RuleFor(e => e.Cidade)
            .NotEmpty().WithMessage("A cidade deve ser informada")
            .MaximumLength(100).WithMessage("A cidade pode ter no máximo 100 caracteres");

        RuleFor(e => e.Bairro)
            .NotEmpty().WithMessage("O bairro deve ser informado")
            .MaximumLength(100).WithMessage("O bairro pode ter no máximo 100 caracteres");
    }
}

