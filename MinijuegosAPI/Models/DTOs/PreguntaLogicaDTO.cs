namespace ObligatorioDDA2.MinijuegosAPI.Models.DTOs
{
    public class PreguntaLogicaDTO : PreguntaGeneralDTO
    {
        public int[] numeros { get; set; }
        public string proposicion { get; set; }
        public string codigo_proposicion { get; set; }

    }
}
