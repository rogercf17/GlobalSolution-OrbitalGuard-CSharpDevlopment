using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore.Infrastructure;
using OrbitalGuard.Data;
using OrbitalGuard.Repositories;
using OrbitalGuard.Repositories.Interfaces;
using OrbitalGuard.Services;
using OrbitalGuard.Services.Interfaces;
using System.Text.Json.Serialization; // Adicionado para ReferenceHandler

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configuração dos Controllers com tratamento de ciclos JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do Oracle com compatibilidade de versão
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("Oracle"),
        oracleOptions => oracleOptions.UseOracleSQLCompatibility(OracleSQLCompatibility.DatabaseVersion19)));

// Repositories
builder.Services.AddScoped<ISateliteRepository, SateliteRepository>();
builder.Services.AddScoped<IRegiaoMonitoradaRepository, RegiaoMonitoradaRepository>();
builder.Services.AddScoped<ILeituraClimaticaRepository, LeituraClimaticaRepository>();
builder.Services.AddScoped<IAlertaDesastreRepository, AlertaDesastreRepository>();

// Services
builder.Services.AddScoped<ISateliteService, SateliteService>();
builder.Services.AddScoped<IRegiaoMonitoradaService, RegiaoMonitoradaService>();
builder.Services.AddScoped<ILeituraClimaticaService, LeituraClimaticaService>();
builder.Services.AddScoped<IAlertaDesastreService, AlertaDesastreService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();