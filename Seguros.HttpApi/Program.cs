using Seguros.HttpApi.Dominio.WorkFlow;
using Seguros.HttpApi.Dominio.WorkFlow.WorkflowSteps;
using WorkflowCore.Interface;
using WorkflowCore.Services;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddCarter();
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddDbContext<SegurosDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registre as etapas do fluxo de trabalho
builder.Services.AddTransient<ProprietarioStep>();
builder.Services.AddTransient<CondutorStep>();
builder.Services.AddTransient<VeiculoStep>();
builder.Services.AddTransient<CalcularRiscoStep>();
builder.Services.AddTransient<EnderecoStep>();
builder.Services.AddTransient<CalcularValorStep>();
//builder.Services.AddTransient<CriarApoliceStep>();

builder.Services.AddWorkflow(cfg =>
{
    cfg.UseSqlServer(builder.Configuration.GetConnectionString("WorkFlowDb"), true, true);
});
//Register services
builder.Services.AddScoped<ApoliceRepository>();
builder.Services.AddScoped<CondutorRepository>();
builder.Services.AddScoped<ProprietarioRepository>();
builder.Services.AddScoped<RegrasRepository>();
builder.Services.AddScoped<CalculoRiscoService>();
builder.Services.AddScoped<CalculoValorSeguroService>();
builder.Services.AddScoped<RiscoPorLocalidadeRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IHistoricoAcidentesService, HistoricoAcidentesService>();
builder.Services.AddScoped<GerarApoliceService>();

builder.Services.AddHttpClient<IFipeService, FipeService>(client =>
{
    client.BaseAddress = new Uri("https://parallelum.com.br/fipe/api/v1/");
});
builder.Services.AddHttpClient<IHistoricoAcidentesService, HistoricoAcidentesService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:8080");
});

builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();


//BUILD 
var app = builder.Build();
// Obter o IWorkflowHost dos serviços
var workflowHost = app.Services.GetService<IWorkflowHost>();

if (workflowHost != null)
{
    // Registrar o fluxo de trabalho
    workflowHost.RegisterWorkflow<CriarApoliceWorkflow, CriarApoliceWorkflowData>();

    // Iniciar o host do WorkflowCore
    await workflowHost.StartAsync(new CancellationToken());

    // Opcional: Registrar o evento de parada do aplicativo para parar o WorkflowCore
    app.Lifetime.ApplicationStopping.Register(() =>
    {
        workflowHost.StopAsync(new CancellationToken());
    });
}
else
{
    throw new Exception("IWorkflowHost não está registrado nos serviços.");
}

app.MapCarter();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(options => { });

app.Run();
