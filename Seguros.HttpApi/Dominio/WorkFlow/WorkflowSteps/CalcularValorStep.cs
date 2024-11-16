using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Seguros.HttpApi.Dominio.WorkFlow.WorkflowSteps;

public class CalcularValorStep(CalculoValorSeguroService _calculoValorSeguroService) : StepBodyAsync
{
    public decimal ValorVeiculo { get; set; }
    public int RiscoApolice { get; set; }
    public Cobertura Cobertura { get; set; }
    public decimal ValorApolice { get; set; }
    public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        var coberturasDesejadas = ListaCoberturasSeleciondas.GerarListaDeCoberturas(Cobertura);
        ValorApolice = await _calculoValorSeguroService.CalcularValorSeguroAsync(ValorVeiculo, RiscoApolice, coberturasDesejadas);
        return ExecutionResult.Next();
    }
}
