using HistoricoCondutor.HttpApi.Dominio.Condutor;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HistoricoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null
        );
    }
    ));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<HistoricoContext>();

    // Tentar aplicar migrações com retry
    try
    {
        context.Database.Migrate();
        if (!context.CondutoresHistorico.Any())
        {
            context.CondutoresHistorico.AddRange(
                new CondutorHistorico { Cpf = "02943543012", QuantidadeAcidentes = 2 },
                new CondutorHistorico { Cpf = "12345678910", QuantidadeAcidentes = 1 }
            );
            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        // Logar o erro ou tratar conforme necessário
        Console.WriteLine(ex);
        throw;
    }
}


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

app.Run();
