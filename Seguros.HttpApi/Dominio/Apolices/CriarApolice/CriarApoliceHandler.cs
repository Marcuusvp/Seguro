
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Seguros.HttpApi.Dominio.Apolices.CriarApolice;
public record CriarApoliceCommand(VeiculoApolice Veiculo,
        ProprietarioApolice Proprietario,
        List<CondutorApolice> Condutores,
        EnderecoApolice Endereco,
        CoberturaApolice Cobertura) : ICommand<Result<CriarApoliceResult>>;
public record CriarApoliceResult(Guid Id);

public class CreateApoliceCommandValidator : AbstractValidator<CriarApoliceCommand>
{
    public CreateApoliceCommandValidator()
    {
        RuleFor(p => p.Veiculo)
            .NotNull().WithMessage("O veículo deve ser informado")
            .SetValidator(new VeiculoApoliceValidator());

        RuleFor(p => p.Proprietario)
            .NotNull().WithMessage("O proprietário deve ser informado")
            .SetValidator(new ProprietarioApoliceValidator());

        RuleFor(p => p.Condutores)
            .NotEmpty().WithMessage("Um ou mais condutores devem ser informados")
            .ForEach(condutorRule => condutorRule.SetValidator(new CondutorApoliceValidator()));

        RuleFor(p => p.Endereco)
            .NotNull().WithMessage("Endereço deve ser informado")
            .SetValidator(new EnderecoApoliceValidator());

        RuleFor(p => p.Cobertura)
            .NotNull().WithMessage("Informe os serviços de cobertura")
            .SetValidator(new CoberturaApoliceValidator());
    }
}
internal class CriarApoliceHandler(IWorkflowHost _workflowHost) : ICommandHandler<CriarApoliceCommand, Result<CriarApoliceResult>>
{
    public async Task<Result<CriarApoliceResult>> Handle(CriarApoliceCommand request, CancellationToken cancellationToken)
    {
        var data = new CriarApoliceWorkflowData
        {
            VeiculoInput = request.Veiculo,
            ProprietarioInput = request.Proprietario,
            CondutoresInput = request.Condutores,
            EnderecoInput = request.Endereco,
            CoberturaInput = request.Cobertura
        };

        string workflowId = await _workflowHost.StartWorkflow("CriarApoliceWorkflow", 1, data);

        // Aguardar a conclusão do fluxo de trabalho
        var instance = await _workflowHost.PersistenceStore.GetWorkflowInstance(workflowId);
        while (instance.Status == WorkflowStatus.Runnable || instance.Status == WorkflowStatus.Suspended)
        {
            await Task.Delay(500); // Aguarda meio segundo antes de verificar novamente
            instance = await _workflowHost.PersistenceStore.GetWorkflowInstance(workflowId);
        }

        if (instance.Status == WorkflowStatus.Complete)
        {
            var resultData = instance.Data as CriarApoliceWorkflowData;
            return Result.Success(new CriarApoliceResult(resultData.ApoliceId));
        }
        else
        {
            // Trate casos onde o fluxo não foi concluído com sucesso
            return Result.Failure<CriarApoliceResult>("O fluxo de trabalho não foi concluído com sucesso.");
        }
    }
}

public record VeiculoApolice(string Marca, string Modelo, string Ano, ETipoVeiculo Tipo);
public record ProprietarioApolice(string Cpf, string Nome, DateOnly DataNascimento, EnderecoApolice Residencia);
public record CondutorApolice(string Cpf, DateOnly DataNascimento, EnderecoApolice Residencia);
public record EnderecoApolice(string Uf, string Cidade, string Bairro);
public record CoberturaApolice(bool RouboFurto, bool Colisao, bool Terceiros, bool Residencial);
