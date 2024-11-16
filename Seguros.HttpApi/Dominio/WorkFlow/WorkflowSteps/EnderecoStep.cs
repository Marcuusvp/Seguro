using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Seguros.HttpApi.Dominio.WorkFlow.WorkflowSteps;

public class EnderecoStep : StepBodyAsync
{
    public EnderecoApolice EnderecoInput { get; set; }
    public CoberturaApolice CoberturaInput { get; set; }

    public Endereco Endereco { get; set; }
    public Cobertura Cobertura { get; set; }
    public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {

        Endereco = new Endereco(
            EnderecoInput.Uf,
            EnderecoInput.Cidade,
            EnderecoInput.Bairro);

        Cobertura = new Cobertura(
            CoberturaInput.RouboFurto,
            CoberturaInput.Colisao,
            CoberturaInput.Terceiros,
            CoberturaInput.Residencial);

        return ExecutionResult.Next();
    }
}
