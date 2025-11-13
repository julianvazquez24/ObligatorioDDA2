using ObligatorioDDA2.MinijuegosAPI.Models;

namespace ObligatorioDDA2.MinijuegosAPI.Services
{
    public interface IMiniJuegoServicio
    {
        public Task<Pregunta> GenerarPreguntaServicio();

    }
}
