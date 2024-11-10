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
builder.Services.AddScoped<ApoliceRepository>();
builder.Services.AddScoped<CondutorRepository>();
builder.Services.AddScoped<ProprietarioRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IHistoricoAcidentesService, HistoricoAcidentesService>();
builder.Services.AddHttpClient<IFipeService, FipeService>(client =>
{
    client.BaseAddress = new Uri("https://parallelum.com.br/fipe/api/v1/");
});
builder.Services.AddHttpClient<IHistoricoAcidentesService, HistoricoAcidentesService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5000");
});

builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.MapCarter();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(options => { });

app.Run();
