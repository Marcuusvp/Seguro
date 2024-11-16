using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Seguros.HttpApi.Dominio.WorkFlow.WorkflowSteps;

public class VeiculoStep(IFipeService fipe) : StepBodyAsync
{
    public VeiculoApolice VeiculoInput { get; set; }
    public Veiculo Veiculo { get; set; }
    public decimal ValorVeiculo { get; set; }
    public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        Veiculo = new Veiculo(VeiculoInput.Marca, VeiculoInput.Modelo, VeiculoInput.Ano, VeiculoInput.Tipo);

        var valorVeiculoResult = await fipe.ObterValorVeiculoAsync(
            Veiculo.Tipo.ToString(),
            Veiculo.Marca,
            Veiculo.Modelo,
            Veiculo.Ano,
            new CancellationToken());

        if (valorVeiculoResult.IsFailure)
            throw new Exception(valorVeiculoResult.Error);

        ValorVeiculo = valorVeiculoResult.Value;
        return ExecutionResult.Next();
    }
}
