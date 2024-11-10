using Seguros.HttpApi.Dominio.Apolices.Enums;

namespace Seguros.HttpApi.Dominio.Apolices.CriarApolice;
public record CriarApoliceRequest(VeiculoRequest Veiculo,
        ProprietarioRequest Proprietario,
        List<CondutorRequest> Condutores,
        EnderecoRequest Endereco,
        CoberturaRequest Cobertura);
public record CriarApoliceResponse(Guid Id);

public class CriarApoliceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/apolice", async (CriarApoliceRequest request, ISender sender) =>
        {
            var command = request.Adapt<CriarApoliceCommand>();

            var result = await sender.Send(command);
            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            var reponse = result.Value.Adapt<CriarApoliceResponse>();

            return Results.Created($"/apolice/{reponse.Id}", reponse);
        })
        .WithName("Criar nova apolice")
        .Produces<CriarApoliceResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Cria uma nova apolice")
        .WithDescription("Criar apolice");
    }
}
public record VeiculoRequest(string Marca, string Modelo, string Ano, ETipoVeiculo Tipo);
public record ProprietarioRequest(string Cpf, string Nome, DateOnly DataNascimento, EnderecoRequest Residencia);
public record CondutorRequest(string Cpf, DateOnly DataNascimento, EnderecoRequest Residencia);
public record EnderecoRequest(string Uf, string Cidade, string Bairro);
public record CoberturaRequest(bool RouboFurto, bool Colisao, bool Terceiros, bool Residencial);

