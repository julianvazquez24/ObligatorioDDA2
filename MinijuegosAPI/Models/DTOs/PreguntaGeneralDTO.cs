namespace ObligatorioDDA2.MinijuegosAPI.Models.DTOs
{
    public class PreguntaGeneralDTO
    {
        public int Id { get; set; }
        public string tipo { get; set; }
        public int[] numeros { get; set; }
        public string pregunta { get; set; }
        public DateTime fechaCreacion { get; set; }
    }

    public class PreguntaConCodigoDTO : PreguntaGeneralDTO
    {
        public string codigo_pregunta { get; set; }
    }
}
