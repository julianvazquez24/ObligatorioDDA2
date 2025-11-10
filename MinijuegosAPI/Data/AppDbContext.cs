using Microsoft.EntityFrameworkCore;
using ObligatorioDDA2.MinijuegosAPI.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Pregunta> Preguntas { get; set; }
}