using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Seguros.HttpApi.Dominio.WorkFlow.WorkflowSteps;

public class CalcularRiscoStep(
        IHistoricoAcidentesService _historicoAcidentesService,
        RiscoPorLocalidadeRepository _riscoPorLocalidadeRepository,
        CalculoRiscoService _calculoRiscoService) : StepBodyAsync
{
    public List<Condutor> Condutores { get; set; }
    public int RiscoApolice { get; set; }

    public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        int riscoTotal = 0;
        var token = new CancellationToken();
        foreach (var condutor in Condutores)
        {
            var acidentesResult = await _historicoAcidentesService.ObterQuantidadeAcidentesAsync(condutor.Cpf, token);
            if (acidentesResult.IsFailure)
                throw new Exception(acidentesResult.Error);

            var riscoLocalidade = await _riscoPorLocalidadeRepository.ObterNivelRiscoLocalidadeAsync(
                condutor.Residencia.Uf,
                condutor.Residencia.Cidade,
                condutor.Residencia.Bairro);

            var riscoCondutor = await _calculoRiscoService.CalcularNivelRiscoAsync(condutor, acidentesResult.Value, riscoLocalidade);
            riscoTotal += riscoCondutor;
        }

        RiscoApolice = riscoTotal;
        return ExecutionResult.Next();
    }
}
