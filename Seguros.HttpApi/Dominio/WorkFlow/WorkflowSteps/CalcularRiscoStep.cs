using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Seguros.HttpApi.Dominio.WorkFlow.WorkflowSteps;

public class CalcularRiscoStep(
        IHistoricoAcidentesService _historicoAcidentesService,
        RiscoPorLocalidadeRepository _riscoPorLocalidadeRepository,
        CalculoRiscoService _calculoRiscoService,
        CondutorRepository _condutorRespository) : StepBodyAsync
{
    public List<Guid> CondutoresIds { get; set; }
    public int RiscoApolice { get; set; }

    public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        int riscoTotal = 0;
        var token = new CancellationToken();
        foreach (var condutor in CondutoresIds)
        {
            var condutorResgatado = await _condutorRespository.ObterPorIdAsync(condutor);
            var acidentesResult = await _historicoAcidentesService.ObterQuantidadeAcidentesAsync(condutorResgatado.Value.Cpf, token);
            if (acidentesResult.IsFailure)
                throw new Exception(acidentesResult.Error);

            var riscoLocalidade = await _riscoPorLocalidadeRepository.ObterNivelRiscoLocalidadeAsync(
                condutorResgatado.Value.Residencia.Uf,
                condutorResgatado.Value.Residencia.Cidade,
                condutorResgatado.Value.Residencia.Bairro);

            var riscoCondutor = await _calculoRiscoService.CalcularNivelRiscoAsync(condutorResgatado.Value, acidentesResult.Value, riscoLocalidade);
            riscoTotal += riscoCondutor;
        }

        RiscoApolice = riscoTotal;
        return ExecutionResult.Next();
    }
}
