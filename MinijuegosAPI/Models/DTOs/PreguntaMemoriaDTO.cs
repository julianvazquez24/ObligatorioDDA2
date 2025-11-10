namespace ObligatorioDDA2.MinijuegosAPI.Models.DTOs
{
    public class PreguntaMemoriaDTO
    {
        public int Id { get; set; }
        public string tipo { get; set; }
        public int[] secuencia { get; set; }
        public string pregunta { get; set; }
        public string codigo_pregunta { get; set; } //MAYUS
        public DateTime fechaCreacion   { get; set; }
    }
}
