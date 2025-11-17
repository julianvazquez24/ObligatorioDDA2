using ObligatorioDDA2.MinijuegosAPI.Models;
using ObligatorioDDA2.MinijuegosAPI.Models.DTOs;

namespace ObligatorioDDA2.MinijuegosAPI.Services
{
    public interface IMiniJuegoServicio
    {
        public Task<Pregunta> GenerarPreguntaServicio();

        public ValidacionRespuestaDTO ValidarRespuesta(int id, string respuesta);

    }
}
