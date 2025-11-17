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


        [HttpPost("pregunta")]
        public async Task<IActionResult> GenerarPregunta([FromBody] string tipo)
        {
            IMiniJuegoServicio minijuego = CrearMinijuegoPregunta(tipo);
            if (minijuego == null)
            {
                return BadRequest("Tipo de minijuego no válido.");
            }
            PreguntaGeneralDTO preguntaGenerada = await minijuego.GenerarPreguntaServicio();
            return Ok(preguntaGenerada);
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