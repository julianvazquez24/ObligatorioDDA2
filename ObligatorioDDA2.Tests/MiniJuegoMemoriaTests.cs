using Xunit;
using Moq;
using ObligatorioDDA2.MinijuegosAPI.Services;
using ObligatorioDDA2.MinijuegosAPI.Models;
using ObligatorioDDA2.MinijuegosAPI.Models.DTOs;
using ObligatorioDDA2.MinijuegosAPI.Data;

namespace Obligatorio2.Tests
{
    public class MiniJuegoMemoriaTests
    {
        [Fact]
        public async Task MiniJuegoMemoria_GenerarPreguntaServicio_NoNulo()
        {
            Mock<IPreguntasRepository> repositorioMock = new Mock<IPreguntasRepository>();
            Pregunta preguntaGuardada = null;

            repositorioMock.Setup(r => r.AgregarPregunta(It.IsAny<Pregunta>()))
                .Callback<Pregunta>(p =>
                {
                    p.Id = 1;
                    preguntaGuardada = p;
                })
                .Returns(Task.CompletedTask);

            MiniJuegoMemoria minijuego = new MiniJuegoMemoria(repositorioMock.Object);

            // act
            PreguntaGeneralDTO dto = await minijuego.GenerarPreguntaServicio();

            // assert
            Assert.NotNull(dto);
            Assert.NotNull(preguntaGuardada);

            PreguntaMemoriaDTO dtoMemoria = Assert.IsType<PreguntaMemoriaDTO>(dto);

            Assert.Equal(1, dtoMemoria.Id);
            Assert.Equal("memoria", dtoMemoria.tipo);
            Assert.Equal(preguntaGuardada.numeros, dtoMemoria.secuencia);
            Assert.Equal(preguntaGuardada.codigo_pregunta, dtoMemoria.codigo_pregunta);
            Assert.Equal(preguntaGuardada.pregunta, dtoMemoria.pregunta);

            // verificar respuesta correcta
            bool respuestaEsperada = minijuego.EvaluarProposicion(
                preguntaGuardada.numeros,
                preguntaGuardada.codigo_pregunta
            );
            Assert.Equal(respuestaEsperada.ToString().ToUpper(), preguntaGuardada.respuesta);

            // se guarda una sola vez
            repositorioMock.Verify(r => r.AgregarPregunta(It.IsAny<Pregunta>()), Times.Once);
        }

        [Fact]
        public void MiniJuegoMemoria_EvaluarProposicion_Hay2Pares_DaVerdadero()
        {
            MiniJuegoMemoria minijuego = new MiniJuegoMemoria(new Mock<IPreguntasRepository>().Object);
            int[] secuencia = { 2, 4, 3, 7, 9 };

            bool resultado = minijuego.EvaluarProposicion(secuencia, "2PARES");

            Assert.True(resultado);
        }

        [Fact]
        public void MiniJuegoMemoria_EvaluarProposicion_Hay2Pares_DaFalso()
        {
            MiniJuegoMemoria minijuego = new MiniJuegoMemoria(new Mock<IPreguntasRepository>().Object);
            int[] secuencia = { 2, 4, 6, 7, 9 };

            bool resultado = minijuego.EvaluarProposicion(secuencia, "2PARES");

            Assert.False(resultado);
        }

        [Fact]
        public void MiniJuegoMemoria_EvaluarProposicion_Hay2Impares_DaVerdadero()
        {
            MiniJuegoMemoria minijuego = new MiniJuegoMemoria(new Mock<IPreguntasRepository>().Object);
            int[] secuencia = { 1, 3, 4, 8, 6 };

            bool resultado = minijuego.EvaluarProposicion(secuencia, "2IMPARES");

            Assert.True(resultado);
        }

        [Fact]
        public void MiniJuegoMemoria_EvaluarProposicion_SumaMayor50_DaVerdadero()
        {
            MiniJuegoMemoria minijuego = new MiniJuegoMemoria(new Mock<IPreguntasRepository>().Object);
            int[] secuencia = { 20, 10, 15, 5, 4 };

            bool resultado = minijuego.EvaluarProposicion(secuencia, "SUMATODOMAYOR50");

            Assert.True(resultado);
        }

        [Fact]
        public void MiniJuegoMemoria_EvaluarProposicion_SumaMayor50_DaFalso()
        {
            MiniJuegoMemoria minijuego = new MiniJuegoMemoria(new Mock<IPreguntasRepository>().Object);
            int[] secuencia = { 5, 5, 5, 5, 5 };

            bool resultado = minijuego.EvaluarProposicion(secuencia, "SUMATODOMAYOR50");

            Assert.False(resultado);
        }

        [Fact]
        public void MiniJuegoMemoria_EvaluarProposicion_2Iguales_DaVerdadero()
        {
            MiniJuegoMemoria minijuego = new MiniJuegoMemoria(new Mock<IPreguntasRepository>().Object);
            int[] secuencia = { 3, 7, 7, 4, 1 };

            bool resultado = minijuego.EvaluarProposicion(secuencia, "2IGUALES");

            Assert.True(resultado);
        }

        [Fact]
        public void MiniJuegoMemoria_EvaluarProposicion_2Iguales_DaFalso()
        {
            MiniJuegoMemoria minijuego = new MiniJuegoMemoria(new Mock<IPreguntasRepository>().Object);
            int[] secuencia = { 3, 7, 8, 4, 1 };

            bool resultado = minijuego.EvaluarProposicion(secuencia, "2IGUALES");

            Assert.False(resultado);
        }

        [Fact]
        public void MiniJuegoMemoria_EvaluarProposicion_AlgunoMenor10_DaVerdadero()
        {
            MiniJuegoMemoria minijuego = new MiniJuegoMemoria(new Mock<IPreguntasRepository>().Object);
            int[] secuencia = { 15, 18, 3, 20, 12 };

            bool resultado = minijuego.EvaluarProposicion(secuencia, "ALGUNO_MENOR10");

            Assert.True(resultado);
        }

        [Fact]
        public void MiniJuegoMemoria_EvaluarProposicion_AlgunoMenor10_DaFalso()
        {
            MiniJuegoMemoria minijuego = new MiniJuegoMemoria(new Mock<IPreguntasRepository>().Object);
            int[] secuencia = { 15, 18, 12, 20, 12 };

            bool resultado = minijuego.EvaluarProposicion(secuencia, "ALGUNO_MENOR10");

            Assert.False(resultado);
        }


        [Fact]
        public async Task MiniJuegoMemoria_GenerarPregunta_GeneraCincoNumeros()
        {
            Mock<IPreguntasRepository> repoMock = new Mock<IPreguntasRepository>();
            repoMock.Setup(r => r.AgregarPregunta(It.IsAny<Pregunta>()))
                .Returns(Task.CompletedTask);

            MiniJuegoMemoria minijuego = new MiniJuegoMemoria(repoMock.Object);

            PreguntaGeneralDTO dto = await minijuego.GenerarPreguntaServicio();
            PreguntaMemoriaDTO dtoMemoria = Assert.IsType<PreguntaMemoriaDTO>(dto);

            Assert.Equal(5, dtoMemoria.secuencia.Length);
        }
    }
}
