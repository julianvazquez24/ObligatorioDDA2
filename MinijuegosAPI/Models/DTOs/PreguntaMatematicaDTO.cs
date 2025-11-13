namespace ObligatorioDDA2.MinijuegosAPI.Models.DTOs
{
    public class PreguntaMatematicaDTO
    {
        public int Id { get; set; }
        public string tipo { get; set; }
        public int[] numeros { get; set; }

        public string pregunta { get; set; }
        public DateTime fechaCreacion   { get; set; }
    }
}
