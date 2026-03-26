using Microsoft.EntityFrameworkCore;
using WarehouseSystem.Infrastructure.Persistence;
using WarehouseSystem.Application.Interfaces;
using WarehouseSystem.Application.Services;
using WarehouseSystem.Infrastructure.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Registracija baze
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SkladisteDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();

// Mapiranje interfejsa na DbContext
// Ovo omogućava servisima u Application sloju da dobiju bazu preko interfejsa
builder.Services.AddScoped<IApplicationDbContext>(provider => 
    provider.GetRequiredService<SkladisteDbContext>());

// Registracija servisa (Povezujemo interfejs sa konkretnom klasom)
builder.Services.AddScoped<IArtikalService, ArtikalService>();
builder.Services.AddScoped<ILokacijaService, LokacijaService>();
builder.Services.AddScoped<IStanjeZalihaService, StanjeZalihaService>();

builder.Services.AddScoped<IKafkaProducer, KafkaProducer>();

// U .NET 10, ovo je dovoljno za osnovni Swagger/OpenApi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.MapControllers();
app.Run();