using FarmaciaVerifarmaChallenge.Application.Interfaces;
using FarmaciaVerifarmaChallenge.Application.Services;
using FarmaciaVerifarmaChallenge.Infrastructure;
using FarmaciaVerifarmaChallenge.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Agrego la dbContext
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
    builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<FarmaciaDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddScoped<IFarmaciaRepository, FarmaciaRepository>();
builder.Services.AddScoped<IFarmaciaService, FarmaciaService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Farmacia Verifarma API",
        Version = "v1",
        Description = " Web API crud farmacias y traer farmacias por cercania",
    });
});

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Configura Kestrel antes de construir la aplicación.
if (builder.Environment.IsDevelopment())
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(8080); // Puerto para HTTP
    });
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();