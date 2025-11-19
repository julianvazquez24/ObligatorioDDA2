using ObligatorioDDA2.MinijuegosAPI.Models;
using ObligatorioDDA2.MinijuegosAPI.Models.DTOs;

namespace ObligatorioDDA2.MinijuegosAPI.Services
{
    public class MiniJuegoMatematica : IMiniJuegoServicio
    {
        private readonly IPreguntasRepository _repositorio;

        public MiniJuegoMatematica(IPreguntasRepository repository)
        {
            _repositorio = repository;
        }
        public async Task<PreguntaGeneralDTO> GenerarPreguntaServicio()
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
                respuesta = suma.ToString(),
                fechaCreacion = DateTime.Now
            };

            await _repositorio.AgregarPregunta(pregunta);

            PreguntaGeneralDTO dtoPregunta = new PreguntaMatematicaDTO
            {
                Id = pregunta.Id,
                tipo = pregunta.tipo,
                numeros = pregunta.numeros,
                fechaCreacion = pregunta.fechaCreacion
            };

            return dtoPregunta;
        }

        public async Task<ValidacionRespuestaDTO> ValidarRespuesta(int id, string respuesta)
        {
            Pregunta pregunta =  await _repositorio.TraerPreguntaPorId(id);


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
