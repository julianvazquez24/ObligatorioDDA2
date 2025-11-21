using Xunit;
using Moq;
using ObligatorioDDA2.MinijuegosAPI.Services;    
using ObligatorioDDA2.MinijuegosAPI.Models;      
using ObligatorioDDA2.MinijuegosAPI.Models.DTOs;


namespace Obligatorio2.Tests
{
    public class MiniJuegoLogicaTests
    {
        [Fact]
        public async Task MiniJuegoLogica_GenerarPreguntaServicio_NotNull()
        {
            var repomock = new Mock<IPreguntasRepository>();
            Pregunta? preguntaGuardada = null;

            repomock.Setup(r => r.AgregarPregunta(It.IsAny<Pregunta>()))
                .Callback<Pregunta>(p =>
                {
                    p.Id = 1;
                    preguntaGuardada = p;
                })
                .Returns(Task.CompletedTask);

            var minijuego = new MiniJuegoLogica(repomock.Object);

            // act
            PreguntaGeneralDTO dto = await minijuego.GenerarPreguntaServicio();

            // assert
            Assert.NotNull(dto);
            Assert.NotNull(preguntaGuardada);

            var dtoLogica = Assert.IsType<PreguntaLogicaDTO>(dto);

            Assert.Equal(1, dtoLogica.Id);
            Assert.Equal("logica", dtoLogica.tipo);
            Assert.Equal(preguntaGuardada!.numeros, dtoLogica.numeros);
            Assert.Equal(preguntaGuardada.codigo_pregunta, dtoLogica.codigo_proposicion);
            Assert.Equal(preguntaGuardada.pregunta, dtoLogica.proposicion);

            // fijamos que este bien la respuesta 
            bool valorEsperado = minijuego.EvaluarProposicion(
                preguntaGuardada.numeros!,
                preguntaGuardada.codigo_pregunta!
            );
            Assert.Equal(valorEsperado.ToString().ToUpper(), preguntaGuardada.respuesta);

            // el repositorio se llama una vez
            repomock.Verify(r => r.AgregarPregunta(It.IsAny<Pregunta>()), Times.Once);
        }



        [Fact]
        public void MinijuegoLogica_EvaluarProposicionHay2Pares_DevuelveTrueSiHayDosPares()
        {
            var minijuego = new MiniJuegoLogica(new Mock<IPreguntasRepository>().Object);
            int[] numeros = { 2, 3, 4 };

            bool resultado = minijuego.EvaluarProposicion(numeros, "2PARES");

            Assert.True(resultado);
        }

        [Fact]
        public void MinijuegoLogica_EvaluarProposicionHay2Pares_DevuelveFalseSiNoHayDosPares()
        {
            var minijuego = new MiniJuegoLogica(new Mock<IPreguntasRepository>().Object);
            int[] numeros ={ 1, 3, 4 }; 

            bool resultado = minijuego.EvaluarProposicion(numeros, "2PARES");

            Assert.False(resultado);
        }

        [Fact]
        public void MiniJuegoLogica_EvaluarProposicionSumaPar_DevuelveTrueSiEsPar()
        {
            var minijuego = new MiniJuegoLogica(new Mock<IPreguntasRepository>().Object);
            int[] numeros = { 2, 2, 10 };

            bool resultado = minijuego.EvaluarProposicion(numeros, "SUMA_PAR");

            Assert.True(resultado);
        }

        [Fact]
        public void MiniJuegoLogica_EvaluarProposicionSumaPar_DevuelveFalseSiNoEsPar()
        {
            var minijuego = new MiniJuegoLogica(new Mock<IPreguntasRepository>().Object);
            int[] numeros = { 1, 2, 8 };

            bool resultado = minijuego.EvaluarProposicion(numeros, "SUMA_PAR");

            Assert.False(resultado);
        }



        [Fact]
        public void MiniJuegoLogica_EvaluarProposicionTodosDiferentes_DevuelveFalseSiHayRepetidos()
        {
            var minijuego = new MiniJuegoLogica(new Mock<IPreguntasRepository>().Object);
            int[] numeros = { 2, 2, 3 };

            bool resultado = minijuego.EvaluarProposicion(numeros, "TODOS_DIFERENTES");

            Assert.False(resultado);
        }
        [Fact]
        public void MiniJuegoLogica_EvaluarProposicionTodosDiferentes_DevuelveTrueSiNoHayRepetidos()
        {
            var minijuego = new MiniJuegoLogica(new Mock<IPreguntasRepository>().Object);
            int[] numeros = { 2, 5, 3 };

            bool resultado = minijuego.EvaluarProposicion(numeros, "TODOS_DIFERENTES");

            Assert.True(resultado);
        }

        [Fact]
        public void MiniJuegoLogica_EvaluarProposicionConCodigoQueNoExiste_TiraError()
        {
            var minijuego = new MiniJuegoLogica(new Mock<IPreguntasRepository>().Object);
            int[] numeros = { 2, 5, 3 };

            var ex = Assert.Throws<ArgumentException>(() => minijuego.EvaluarProposicion(numeros, "NOEXISTE"));
            Assert.Equal("Código de proposición no válido", ex.Message);
        }

        [Fact]
        public async Task MinijuegoLogica_GenerarPregunta_GeneraTresNumeros()
        {
            var repomock = new Mock<IPreguntasRepository>();
            repomock.Setup(r => r.AgregarPregunta(It.IsAny<Pregunta>()))
                .Returns(Task.CompletedTask);

            var minijuego = new MiniJuegoLogica(repomock.Object);

            var dto = await minijuego.GenerarPreguntaServicio();
            var dtoLogica = Assert.IsType<PreguntaLogicaDTO>(dto);

            Assert.Equal(3, dtoLogica.numeros.Length);
        }
    }

 
}