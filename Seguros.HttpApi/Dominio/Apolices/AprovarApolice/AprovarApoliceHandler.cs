
namespace Seguros.HttpApi.Dominio.Apolices.AprovarApolice;
public record AprovarApoliceCommand(Guid Id) : ICommand<Result<AprovarApoliceResult>>;
public record AprovarApoliceResult(byte[] Pdf);

public class AprovarApoliceHandlerValidator : AbstractValidator<AprovarApoliceCommand>
{
    public AprovarApoliceHandlerValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Informe o Id da apolice desejada");
    }
}
internal class AprovarApoliceHandler(ApoliceRepository repository, GerarApoliceService gerarApolice) : ICommandHandler<AprovarApoliceCommand, Result<AprovarApoliceResult>>
{
    public async Task<Result<AprovarApoliceResult>> Handle(AprovarApoliceCommand request, CancellationToken cancellationToken)
    {
        var recuperarApolice = repository.RecuperarApolice(request.Id, cancellationToken);
        if (recuperarApolice.Result.HasNoValue)
            return Result.Failure<AprovarApoliceResult>("Apólice não encontrada");

        var apolice = recuperarApolice.Result.Value;

        apolice.AtualizarStatus(EApoliceStatus.Aprovada);
        var pdf = gerarApolice.GerarApolice(apolice);
        if (pdf == null || pdf.Length == 0)
        {
            return Result.Failure<AprovarApoliceResult>("Falha ao gerar o PDF da apólice");
        }
        return Result.Success(new AprovarApoliceResult(pdf));
    }
}
