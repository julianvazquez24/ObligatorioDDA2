namespace ObligatorioDDA2.MinijuegosAPI.Models.DTOs
{
    public class ValidacionRespuestaDTO
    {
        public bool esCorrecta { get; set; }
        public string mensaje { get; set; }
        public string respuestaCorrecta { get; set; }
        public string tipoMiniJuego { get; set; }
    }
}
