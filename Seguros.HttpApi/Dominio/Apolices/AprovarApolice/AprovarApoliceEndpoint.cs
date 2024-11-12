
using Microsoft.AspNetCore.Mvc;

namespace Seguros.HttpApi.Dominio.Apolices.AprovarApolice;
public record AprovarApoliceResponse(byte[] Pdf);
public class AprovarApoliceEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/apolice/${ApoliceId}/aprovar", async (Guid ApoliceId, ISender sender) =>
        {
            var result = await sender.Send(new AprovarApoliceCommand(ApoliceId));
            if (result.IsFailure)
            {
                return Results.Problem(result.Error, statusCode: 404);
            }
            var response = result.Value.Adapt<AprovarApoliceResponse>();
            if (response.Pdf == null || response.Pdf.Length == 0)
            {
                return Results.Problem("O PDF gerado está vazio.", statusCode: 500);
            }
            return Results.File(response.Pdf, "application/pdf", $"apolice{Guid.NewGuid()}.pdf");
        })
        .WithName("AprovarApolice")
        .Produces<FileResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Aprova apolice")
        .WithDescription("Aprova uma apolice");
    }
}
