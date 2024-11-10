using Seguros.HttpApi.Dominio.Apolices.CriarApolice;

namespace Seguros.HttpApi.Dominio.Apolices.Validators;

public class VeiculoApoliceValidator : AbstractValidator<VeiculoApolice>
{
    public VeiculoApoliceValidator()
    {
        RuleFor(v => v.Marca)
            .NotEmpty().WithMessage("A marca do veículo deve ser informada")
            .MaximumLength(50).WithMessage("A marca pode ter no máximo 50 caracteres");

        RuleFor(v => v.Modelo)
            .NotEmpty().WithMessage("O modelo do veículo deve ser informado")
            .MaximumLength(50).WithMessage("O modelo pode ter no máximo 50 caracteres");

        RuleFor(v => v.Ano)
            .NotEmpty().WithMessage("O ano do veículo deve ser informado");

        RuleFor(v => v.Tipo)
            .IsInEnum().WithMessage("O tipo de veículo é inválido");
    }
}
