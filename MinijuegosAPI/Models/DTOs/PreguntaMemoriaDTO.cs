namespace ObligatorioDDA2.MinijuegosAPI.Models.DTOs
{
    public class PreguntaMemoriaDTO : PreguntaGeneralDTO
    {
        public int[] secuencia { get; set; }
        public string pregunta { get; set; }
        public string codigo_pregunta { get; set; }

    }
}
