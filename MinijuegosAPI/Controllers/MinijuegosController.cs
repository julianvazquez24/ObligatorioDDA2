using Microsoft.AspNetCore.Mvc;
using ObligatorioDDA2.MinijuegosAPI.Models.DTOs;
using ObligatorioDDA2.MinijuegosAPI.Models;
using ObligatorioDDA2.MinijuegosAPI.Services;
using ObligatorioDDA2.MinijuegosAPI.Data;

namespace ObligatorioDDA2.MinijuegosAPI.Controllers
{

    [Route("api/minijuegos")]
    [ApiController]
    public class MinijuegosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly MiniJuegoMatematica _minijuegoMatematica;
        private readonly MiniJuegoLogica _minijuegoLogica;
        private readonly MiniJuegoMemoria _minijuegoMemoria;


        public MinijuegosController(AppDbContext context, MiniJuegoMatematica minijuegoMatematica, MiniJuegoLogica minijuegoLogica, MiniJuegoMemoria minijuegoMemoria)
        {
            _context = context;
            _minijuegoMatematica = minijuegoMatematica;
            _minijuegoLogica = minijuegoLogica;
            _minijuegoMemoria = minijuegoMemoria;
        }

        private IMiniJuegoServicio CrearMinijuegoPregunta(string tipo)
        {


            if (tipo.ToUpper() == "LOGICA")
            {
                IMiniJuegoServicio minijuegoLogica = _minijuegoLogica;
                return minijuegoLogica;
            }
            else if (tipo.ToUpper() == "MEMORIA")
            {
                IMiniJuegoServicio minijuegoMemoria = _minijuegoMemoria;
                return minijuegoMemoria;
            }
            else if (tipo.ToUpper() == "MATEMATICA")
            {
                IMiniJuegoServicio minijuegoMatematica = _minijuegoMatematica;
                return minijuegoMatematica;
            }
            else
            {
                return null;
            }
        }


        [HttpGet("pregunta")]
        public async Task<IActionResult> GetPregunta(string tipo)
        {

            IMiniJuegoServicio minijuegoPregunta = CrearMinijuegoPregunta(tipo);
            if (tipo == null)
            {
                return BadRequest("El tipo de minijuego es requerido.");
            }
            if (minijuegoPregunta == null)
            {
                return BadRequest("Tipo de minijuego no válido.");
            }
            Pregunta preguntaGenerada = await minijuegoPregunta.GenerarPreguntaServicio();
            if (preguntaGenerada == null)
            {
                return BadRequest("No se pudo generar la pregunta.");
            }
            else if (tipo.ToUpper() == "MATEMATICA")
            {
                PreguntaMatematicaDTO dto = new PreguntaMatematicaDTO
                {
                    Id = preguntaGenerada.Id,
                    tipo = preguntaGenerada.tipo,
                    pregunta = preguntaGenerada.pregunta,
                    numeros = preguntaGenerada.numeros,
                    fechaCreacion = preguntaGenerada.fechaCreacion
                };
                return Ok(dto);
            }
            else if (tipo.ToUpper() == "LOGICA")
            {
                PreguntaLogicaDTO dto = new PreguntaLogicaDTO
                {
                    Id = preguntaGenerada.Id,
                    tipo = preguntaGenerada.tipo,
                    numeros = preguntaGenerada.numeros,
                    codigo_proposicion = preguntaGenerada.codigo_proposicion,
                    proposicion = preguntaGenerada.proposicion,
                };
                return Ok(dto);
            }
            else if (tipo.ToUpper() == "MEMORIA")
            {
                PreguntaMemoriaDTO dto = new PreguntaMemoriaDTO
                {
                    Id = preguntaGenerada.Id,
                    tipo = preguntaGenerada.tipo,
                    secuencia = preguntaGenerada.secuencia,
                    codigo_pregunta = preguntaGenerada.codigo_pregunta,
                    pregunta = preguntaGenerada.pregunta,
                };
                return Ok(dto);
            }
            else
            {
                return BadRequest("Tipo de minijuego no válido.");
            }



        }

        [HttpGet("validar")]
        public async Task<IActionResult> ValidarRespuesta(int id, string respuesta)
        {
            Pregunta pregunta = await _context.Preguntas.FindAsync(id);
            if (pregunta == null)
            {
                return NotFound("Pregunta no encontrada.");
            }
            if (pregunta.tipo == "matematica")
            {
                IMiniJuegoServicio minijuegoMatematica = _minijuegoMatematica;
                ValidacionRespuestaDTO resultadoValidacion = minijuegoMatematica.ValidarRespuesta(id, respuesta);
                return Ok(resultadoValidacion);
            }
            else if (pregunta.tipo == "logica")
            {
                IMiniJuegoServicio minijuegoLogica = _minijuegoLogica;
                ValidacionRespuestaDTO resultadoValidacion = minijuegoLogica.ValidarRespuesta(id, respuesta);
                return Ok(resultadoValidacion);
            }
            else if (pregunta.tipo == "memoria")
            {
                IMiniJuegoServicio minijuegoMemoria = _minijuegoMemoria;
                ValidacionRespuestaDTO resultadoValidacion = minijuegoMemoria.ValidarRespuesta(id, respuesta);
                return Ok(resultadoValidacion);
            }
            else
            {
                return BadRequest("Tipo de minijuego no válido.");
            }

        }
    }
}