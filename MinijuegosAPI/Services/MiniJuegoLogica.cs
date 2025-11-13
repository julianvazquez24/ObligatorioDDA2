using System;
using ObligatorioDDA2.MinijuegosAPI.Data;
using ObligatorioDDA2.MinijuegosAPI.Models;


namespace ObligatorioDDA2.MinijuegosAPI.Services
{
    public class MiniJuegoLogica : IMiniJuegoServicio
    {
        private readonly AppDbContext _context;

        public MiniJuegoLogica(AppDbContext context)
        {
            _context = context;
        }

        public Task<Pregunta> GenerarPreguntaServicio()
        {
            Random generador = new Random();
            int[] numeros = new int[3];
            for (int i = 0; i < 3; i++)
            {

                numeros[i] = generador.Next(1, 101);
            }

            List<(string texto, string codigo)> proposiciones =
            new List<(string texto, string codigo)>
            {   
                ("Exactamente 2 números son pares", "2PARES"),
                ("La suma de los 3 números es par", "SUMA_PAR"),
                ("El número mayor es mayor que la suma de los otros dos", "MAYOR_SUMA_OTROS"),
                ("Hay al menos un número mayor que 50", "ALGUNO_MAYOR50"),
                ("Todos los números son diferentes", "TODOS_DIFERENTES")
            };

            (string texto, string codigo) proposicionSeleccionada = proposiciones[generador.Next(proposiciones.Count)];
            bool valorRespuesta = EvaluarProposicion(numeros, proposicionSeleccionada.codigo);

            Pregunta pregunta = new Pregunta
            {
                tipo = "logica",
                numeros = numeros,
                proposicion = proposicionSeleccionada.texto,
                codigo_proposicion = proposicionSeleccionada.codigo,
                respuesta = valorRespuesta.ToString(),
                fechaCreacion = DateTime.Now
            };
            _context.Preguntas.Add(pregunta);
            _context.SaveChanges();
            return Task.FromResult(pregunta);


        }

        public bool EvaluarProposicion(int[] numeros, string codigoProposicion)
        {
            switch (codigoProposicion)
            {
                case "2PARES":
                    int countPares = numeros.Count(n => n % 2 == 0);
                    return countPares == 2;
                case "SUMA_PAR":
                    int suma = numeros.Sum();
                    return suma % 2 == 0;
                case "MAYOR_SUMA_OTROS":
                    int mayor = numeros.Max();
                    int sumaOtros = numeros.Sum() - mayor;
                    return mayor > sumaOtros;
                case "ALGUNO_MAYOR50":
                    return numeros.Any(n => n > 50);
                case "TODOS_DIFERENTES":
                    return numeros.Distinct().Count() == 3;
                default:
                    throw new ArgumentException("Código de proposición no válido");
            }
        }
    }
}
