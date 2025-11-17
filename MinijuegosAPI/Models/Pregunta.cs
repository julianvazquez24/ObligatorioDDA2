namespace ObligatorioDDA2.MinijuegosAPI.Models
{
    public class Pregunta
    {
        public int Id { get; set; }
        public string? tipo { get; set; }
        public int[]? numeros { get; set; }
        public string? pregunta { get; set; }
        public string? codigo_pregunta { get; set; } 
        public DateTime fechaCreacion   { get; set; }
        public string? respuesta { get; set; }
    }
}
