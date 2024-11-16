using Seguros.HttpApi.Dominio.WorkFlow.WorkflowSteps;
using WorkflowCore.Interface;

namespace Seguros.HttpApi.Dominio.WorkFlow;

public class CriarApoliceWorkflow : IWorkflow<CriarApoliceWorkflowData>
{
    public string Id => "CriarApoliceWorkflow";
    public int Version => 1;

    public void Build(IWorkflowBuilder<CriarApoliceWorkflowData> builder)
    {
        builder
            .StartWith<VeiculoStep>()
                .Input(step => step.VeiculoInput, data => data.VeiculoInput)
                .Output(data => data.Veiculo, step => step.Veiculo)
                .Output(data => data.ValorVeiculo, step => step.ValorVeiculo)

            .Then<ProprietarioStep>()
                .Input(step => step.ProprietarioInput, data => data.ProprietarioInput)
                .Output(data => data.Proprietario, step => step.Proprietario)

            .Then<CondutorStep>()
                .Input(step => step.CondutoresInput, data => data.CondutoresInput)
                .Output(data => data.Condutores, step => step.Condutores)

            .Then<CalcularRiscoStep>()
                .Input(step => step.Condutores, data => data.Condutores)
                .Output(data => data.RiscoApolice, step => step.RiscoApolice)

            .Then<EnderecoStep>()
                .Input(step => step.EnderecoInput, data => data.EnderecoInput)
                .Input(step => step.CoberturaInput, data => data.CoberturaInput)
                .Output(data => data.Endereco, step => step.Endereco)
                .Output(data => data.Cobertura, step => step.Cobertura)

            .Then<CalcularValorStep>()
                .Input(step => step.ValorVeiculo, data => data.ValorVeiculo)
                .Input(step => step.RiscoApolice, data => data.RiscoApolice)
                .Input(step => step.Cobertura, data => data.Cobertura)
                .Output(data => data.ValorApolice, step => step.ValorApolice)

            .Then<CriarApoliceStep>()
                .Input(step => step.Veiculo, data => data.Veiculo)
                .Input(step => step.Proprietario, data => data.Proprietario)
                .Input(step => step.Condutores, data => data.Condutores)
                .Input(step => step.Endereco, data => data.Endereco)
                .Input(step => step.Cobertura, data => data.Cobertura)
                .Input(step => step.ValorApolice, data => data.ValorApolice)
                .Output(data => data.Apolice, step => step.Apolice)
                .Output(data => data.ApoliceId, step => step.ApoliceId);
    }
}

