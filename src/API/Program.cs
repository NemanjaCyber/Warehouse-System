using Microsoft.EntityFrameworkCore;
using WarehouseSystem.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Registracija baze
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SkladisteDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();

// U .NET 10, ovo je dovoljno za osnovni Swagger/OpenApi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();