
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
internal class CriarApoliceHandler(IWorkflowHost _workflowHost,
    CondutorRepository _condutorRepository,
    IUnitOfWork _unitOfWork,
    ApoliceRepository _apoliceRepository,
    ProprietarioRepository _proprietarioRepository) : ICommandHandler<CriarApoliceCommand, Result<CriarApoliceResult>>
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
            var condutoresApolice = new List<Condutor>();
            var proprietario = await _proprietarioRepository.ObterPorIdAsync(resultData.ProprietarioId, cancellationToken);
            if (proprietario.HasNoValue)
                return Result.Failure<CriarApoliceResult>("Não foi possível resgatar proprietario");
            foreach (var condutor in resultData.CondutoresIds)
            {
                var condutorResult = await _condutorRepository.ObterPorIdAsync(condutor);
                if (condutorResult.HasNoValue)
                    return Result.Failure<CriarApoliceResult>("Não foi possível resgatar condutores");
                var result = condutorResult.Value;
                condutoresApolice.Add(result);
            }
            var apoliceResult = Apolice.Criar(veiculo: resultData.Veiculo,
                proprietario: proprietario.Value,
                condutores: condutoresApolice,
                endereco: resultData.Endereco,
                cobertura: resultData.Cobertura,
                valorTotal: resultData.ValorApolice);

            if (apoliceResult.IsFailure)
                return Result.Failure<CriarApoliceResult>("Não foi possível criar a apolice.");

            await _apoliceRepository.Adicionar(apoliceResult.Value, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Result.Success(new CriarApoliceResult(apoliceResult.Value.Id));
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
