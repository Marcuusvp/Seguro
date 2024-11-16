using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Seguros.HttpApi.Dominio.WorkFlow.WorkflowSteps;

public class CondutorStep(CondutorRepository _condutorRepository) : StepBodyAsync
{
    public List<CondutorApolice> CondutoresInput { get; set; }
    public List<Condutor> Condutores { get; set; } = new();
    public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        var token = new CancellationToken();
        foreach (var condutorInput in CondutoresInput)
        {
            var condutor = await _condutorRepository.ObterPorCpfAsync(condutorInput.Cpf, token);

            if (condutor == null)
            {
                var condutorResult = condutorInput.ToEntity();

                if (condutorResult.IsFailure)
                    throw new Exception(condutorResult.Error);

                condutor = condutorResult.Value;
                await _condutorRepository.AdicionarAsync(condutor.Value, token);
            }
            else
            {
                var novoEndereco = new Endereco(
                    condutorInput.Residencia.Uf,
                    condutorInput.Residencia.Cidade,
                    condutorInput.Residencia.Bairro);

                if (!condutor.Value.Residencia.Equals(novoEndereco))
                {
                    condutor.Value.AtualizarEndereco(novoEndereco);
                    await _condutorRepository.AtualizarAsync(condutor.Value, token);
                }
            }

            Condutores.Add(condutor.Value);
        }

        return ExecutionResult.Next();
    }
}
