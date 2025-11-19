using ObligatorioDDA2.MinijuegosAPI.Data;
using ObligatorioDDA2.MinijuegosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ObligatorioDDA2.MinijuegosAPI.Services
{
    public class PreguntasRepository : IPreguntasRepository
    {
        private readonly AppDbContext _context;
        public PreguntasRepository(AppDbContext context) {
            _context = context;
        }

        public async Task<Pregunta?> TraerPreguntaPorId(int id)
        {
            return await _context.Preguntas.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task  AgregarPregunta(Pregunta pregunta)
        {
            _context.Preguntas.Add(pregunta);
            await _context.SaveChangesAsync();
        }

    }
}
