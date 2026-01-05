using Amazon.Lambda.AspNetCoreServer.Hosting;
using FarmaciaVerifarmaChallenge.Application.Interfaces;
using FarmaciaVerifarmaChallenge.Application.Services;
using FarmaciaVerifarmaChallenge.Infrastructure;
using FarmaciaVerifarmaChallenge.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

// Agrego la dbContext
var connectionString =
    Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
    ?? builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<FarmaciaDbContext>(o => o.UseNpgsql(connectionString));

builder.Services.AddScoped<IFarmaciaRepository, FarmaciaRepository>();
builder.Services.AddScoped<IFarmaciaService, FarmaciaService>();

// Controllers (tu API actual)
builder.Services.AddControllers();

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
builder.Host.UseSerilog();

var app = builder.Build();

// retry simple para migraciones en cold start
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FarmaciaDbContext>();
    for (var i = 0; i < 5; i++)
    {
        try { db.Database.Migrate(); break; }
        catch when (i < 4) { await Task.Delay(2000); }
    }
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }