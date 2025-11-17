using ObligatorioDDA2.MinijuegosAPI.Models;
using ObligatorioDDA2.MinijuegosAPI.Data;
using ObligatorioDDA2.MinijuegosAPI.Models.DTOs;

namespace ObligatorioDDA2.MinijuegosAPI.Services
{
    public class MiniJuegoMatematica : IMiniJuegoServicio
    {
        private readonly AppDbContext _context;

        public MiniJuegoMatematica(AppDbContext context)
        {
            _context = context;
        }
        public Task<PreguntaGeneralDTO> GenerarPreguntaServicio()
        {
            Random generador = new Random();
            int a = generador.Next(1, 101);
            int b = generador.Next(1, 101);
            int c = generador.Next(1, 101);
            int suma = a + b + c;

            Pregunta pregunta = new Pregunta
            {
                tipo = "matematica",
                numeros = new int[] { a, b, c },
                pregunta = $"{a} + {b} + {c} = ?",
                respuesta = suma.ToString(),
                fechaCreacion = DateTime.Now
            };

            _context.Preguntas.Add(pregunta);
            _context.SaveChanges();

            PreguntaGeneralDTO dtoPregunta = new PreguntaGeneralDTO
            {
                Id = pregunta.Id,
                tipo = pregunta.tipo,
                pregunta = pregunta.pregunta,
                numeros = pregunta.numeros,
                fechaCreacion = pregunta.fechaCreacion
            };

            return Task.FromResult(dtoPregunta);
        }

        public ValidacionRespuestaDTO ValidarRespuesta(int id, string respuesta)
        {
            Pregunta pregunta = _context.Preguntas.Find(id);

            if (respuesta != pregunta.respuesta)
            {
                return new ValidacionRespuestaDTO
                {
                    esCorrecta = false,
                    respuestaCorrecta = pregunta.respuesta,
                    mensaje = "Respuesta incorrecta.",
                    tipoMiniJuego = "matematica"

                };
            }
            else
            {
                return new ValidacionRespuestaDTO
                {
                    esCorrecta = true,
                    respuestaCorrecta = pregunta.respuesta,
                    mensaje = "Respuesta correcta.",
                    tipoMiniJuego = "matematica"
                };
            }

        }
    }
}
