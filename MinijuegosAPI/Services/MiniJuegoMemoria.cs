using ObligatorioDDA2.MinijuegosAPI.Data;
using ObligatorioDDA2.MinijuegosAPI.Models;
using ObligatorioDDA2.MinijuegosAPI.Models.DTOs;
namespace ObligatorioDDA2.MinijuegosAPI.Services
 
{
    public class MiniJuegoMemoria : IMiniJuegoServicio
    {
        private readonly AppDbContext _context;
        public MiniJuegoMemoria(AppDbContext context)
        {
            _context = context;
        }

        public Task<Pregunta> GenerarPreguntaServicio()
        {
            Random generador = new Random();
            int[] secuencia = new int[5];
            for (int i = 0; i < 5; i++)
            {
                secuencia[i] = generador.Next(1, 21);
            }

            List<(string texto, string codigo)> proposiciones = new List<(string texto, string codigo)>
            {
                ("¿Había exactamente 2 números pares?", "2PARES"),
                ("¿Había exactamente 2 números impares?", "2IMPARES"),
                ("¿La suma de todos los números superaba 50?", "SUMATODOMAYOR50"),
                ("¿Había 2 números iguales?", "2IGUALES"),
                ("¿Había algún número menor a 10?", "ALGUNO_MENOR10")
            };

            (string texto, string codigo) proposicionSeleccionada = proposiciones[generador.Next(proposiciones.Count)];
            bool valorRespuesta = EvaluarProposicion(secuencia, proposicionSeleccionada.codigo);
            Pregunta pregunta = new Pregunta
            {
                tipo = "memoria",
                secuencia = secuencia,
                pregunta = proposicionSeleccionada.texto,
                codigo_pregunta = proposicionSeleccionada.codigo,
                respuesta = valorRespuesta.ToString(),
                fechaCreacion = DateTime.Now
            };
            _context.Preguntas.Add(pregunta);
            _context.SaveChanges();
            return Task.FromResult(pregunta);

        }

        public bool EvaluarProposicion(int[] secuencia, string codigoProposicion)
        {
            switch (codigoProposicion)
            {
                case "2PARES":
                    int contadorPares = secuencia.Count(n => n % 2 == 0);
                    return contadorPares == 2;
                case "2IMPARES":
                    int contadorImpares = secuencia.Count(n => n % 2 != 0);
                    return contadorImpares == 2;
                case "SUMATODOMAYOR50":
                    int sumaTotal = secuencia.Sum();
                    return sumaTotal > 50;
                case "2IGUALES":
                    var grupos = secuencia.GroupBy(n => n);
                    return grupos.Any(g => g.Count() >= 2);
                case "ALGUNO_MENOR10":
                    return secuencia.Any(n => n < 10);
                default:
                    return false;
            }
        }

        public ValidacionRespuestaDTO ValidarRespuesta(int id, string respuesta)
        {
            
            Pregunta pregunta =_context.Preguntas.Find(id);
            
            if (respuesta != pregunta.respuesta)
            {
                return new ValidacionRespuestaDTO
                {
                    esCorrecta = false,
                    respuestaCorrecta = pregunta.respuesta,
                    mensaje = "Respuesta incorrecta.",
                    tipoMiniJuego = "memoria"
                };
            }
            else
            {
                return new ValidacionRespuestaDTO
                {
                    esCorrecta = true,
                    respuestaCorrecta = pregunta.respuesta,
                    mensaje = "Respuesta correcta.",
                    tipoMiniJuego = "memoria"
                };
            }

        }
    }
}
