using System.Threading;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Seguros.HttpApi.Dominio.WorkFlow.WorkflowSteps;

public class ProprietarioStep(ProprietarioRepository _proprietarioRepository, IUnitOfWork _unitOfWork) : StepBodyAsync
{
    public ProprietarioApolice ProprietarioInput { get; set; }
    public Guid ProprietarioId { get; set; }

    public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        var token = new CancellationToken();
        var proprietario = await _proprietarioRepository.ObterPorCpfAsync(ProprietarioInput.Cpf, token);

        if (proprietario == null)
        {
            var proprietarioResult = ProprietarioInput.ToEntity();

            if (proprietarioResult.IsFailure)
                throw new Exception(proprietarioResult.Error);

            var proprietarioCadastro = proprietarioResult.Value;
            await _proprietarioRepository.AdicionarAsync(proprietarioCadastro, token);
            ProprietarioId = proprietarioResult.Value.Id;
        }
        else
        {
            var novoEndereco = new Endereco(
                ProprietarioInput.Residencia.Uf,
                ProprietarioInput.Residencia.Cidade,
                ProprietarioInput.Residencia.Bairro);

            if (!proprietario.Value.Residencia.Equals(novoEndereco))
            {
                proprietario.Value.AtualizaEndereco(novoEndereco);
                await _proprietarioRepository.AtualizarAsync(proprietario.Value, token);
            }

            ProprietarioId = proprietario.Value.Id;
        }
        await _unitOfWork.CommitAsync(token);
        return ExecutionResult.Next();
    }
}
