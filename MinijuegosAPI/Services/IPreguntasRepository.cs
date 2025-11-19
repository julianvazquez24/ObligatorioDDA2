using ObligatorioDDA2.MinijuegosAPI.Models;
namespace ObligatorioDDA2.MinijuegosAPI.Services
{
    public interface IPreguntasRepository
    {
        Task AgregarPregunta(Pregunta pregunta);
        Task<Pregunta?> TraerPreguntaPorId(int id);
    }
}
