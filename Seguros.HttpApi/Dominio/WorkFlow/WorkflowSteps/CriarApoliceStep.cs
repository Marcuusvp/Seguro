//using WorkflowCore.Interface;
//using WorkflowCore.Models;

//namespace Seguros.HttpApi.Dominio.WorkFlow.WorkflowSteps;

//public class CriarApoliceStep(ApoliceRepository _apoliceRepository,
//    IUnitOfWork _unitOfWork) : StepBodyAsync
//{
//    public Veiculo Veiculo { get; set; }
//    public Proprietario Proprietario { get; set; }
//    public List<Condutor> Condutores { get; set; }
//    public Endereco Endereco { get; set; }
//    public Cobertura Cobertura { get; set; }
//    public decimal ValorApolice { get; set; }
//    public Apolice Apolice { get; set; }
//    public Guid ApoliceId { get; set; }
//    public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
//    {
//        var token = new CancellationToken();
//        var apoliceResult = Apolice.Criar(Veiculo, Proprietario, Condutores, Endereco, Cobertura, ValorApolice);

//        if (apoliceResult.IsFailure)
//            throw new Exception(apoliceResult.Error);

//        Apolice = apoliceResult.Value;

//        await _apoliceRepository.Adicionar(Apolice, token);
//        await _unitOfWork.CommitAsync();

//        ApoliceId = Apolice.Id;

//        return ExecutionResult.Next();
//    }
//}
