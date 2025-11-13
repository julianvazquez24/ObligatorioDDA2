using Microsoft.EntityFrameworkCore;
using ObligatorioDDA2.MinijuegosAPI.Data;
using ObligatorioDDA2.MinijuegosAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<MiniJuegoMatematica>();
builder.Services.AddScoped<MiniJuegoLogica>();
builder.Services.AddScoped<MiniJuegoMemoria>();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=localhost\\SQLEXPRESS;Database=ObligatorioDDA2;Trusted_Connection=True;TrustServerCertificate=True;";

// Configura el DbContext con SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
