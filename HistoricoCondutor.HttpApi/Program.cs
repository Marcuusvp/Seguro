using HistoricoCondutor.HttpApi.Dominio.Condutor;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HistoricoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapGet("/historico/{cpf}", async (string cpf, HistoricoContext context) =>
{
    var historico = await context.CondutoresHistorico
        .FirstOrDefaultAsync(ch => ch.Cpf == cpf);

    if (historico != null)
    {
        return Results.Ok(new
        {
            Cpf = historico.Cpf,
            QuantidadeAcidentes = historico.QuantidadeAcidentes
        });
    }
    else
    {
        return Results.Ok(new
        {
            Cpf = cpf,
            QuantidadeAcidentes = 0
        });
    }
});

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<HistoricoContext>();

    // Verifica se o banco de dados já possui dados
    if (!context.CondutoresHistorico.Any())
    {
        context.CondutoresHistorico.AddRange(
            new CondutorHistorico { Cpf = "02943543012", QuantidadeAcidentes = 2 },
            new CondutorHistorico { Cpf = "12345678910", QuantidadeAcidentes = 1 }
        );
        context.SaveChanges();
    }
}

app.Run();
