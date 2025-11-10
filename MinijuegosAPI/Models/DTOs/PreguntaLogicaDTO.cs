namespace ObligatorioDDA2.MinijuegosAPI.Models.DTOs
{
    public class PreguntaLogicaDTO
    {
        public int Id { get; set; }
        public string tipo { get; set; }
        public int[] numeros { get; set; }
        public string proposicion { get; set; }
        public string codigo_proposicion { get; set; }  //MAYUS
        public DateTime fechaCreacion   { get; set; }
    }
}
