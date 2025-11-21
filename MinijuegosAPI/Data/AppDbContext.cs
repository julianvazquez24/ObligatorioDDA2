using Microsoft.EntityFrameworkCore;
using ObligatorioDDA2.MinijuegosAPI.Models;
using System.Diagnostics.CodeAnalysis;

namespace ObligatorioDDA2.MinijuegosAPI.Data
{
    [ExcludeFromCodeCoverage]
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Tablas
        public DbSet<Pregunta> Preguntas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // PKs (explícitas)
            modelBuilder.Entity<Pregunta>().HasKey(pregunta => pregunta.Id);

        }
    }
}
