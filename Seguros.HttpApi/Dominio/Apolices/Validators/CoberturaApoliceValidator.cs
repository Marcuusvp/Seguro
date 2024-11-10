using Seguros.HttpApi.Dominio.Apolices.CriarApolice;

namespace Seguros.HttpApi.Dominio.Apolices.Validators;

public class CoberturaApoliceValidator : AbstractValidator<CoberturaApolice>
{
    public CoberturaApoliceValidator()
    {
        RuleFor(c => c)
            .Must(HaveAtLeastOneCoverage).WithMessage("Pelo menos uma cobertura deve ser selecionada");
    }

    private bool HaveAtLeastOneCoverage(CoberturaApolice cobertura)
    {
        return cobertura.RouboFurto || cobertura.Colisao || cobertura.Terceiros || cobertura.Residencial;
    }
}

