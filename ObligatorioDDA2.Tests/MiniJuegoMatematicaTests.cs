using Xunit;
using Moq;
using ObligatorioDDA2.MinijuegosAPI.Services;    
using ObligatorioDDA2.MinijuegosAPI.Models;      
using ObligatorioDDA2.MinijuegosAPI.Models.DTOs;


namespace Obligatorio2.Tests
{
    public class MiniJuegoMatematicaTests
    {
        [Fact]
        public async Task MiniJuegoMatematica_GenerarPreguntaServicio_NotNull()
        {
            var repo = new Mock<IPreguntasRepository>();
            repo.Setup(f => f.AgregarPregunta(new Pregunta
            {
                Id = 1,
                tipo = "matematica",
                numeros = new int[] { 1, 2, 3 },
                respuesta = "6",
                fechaCreacion = DateTime.Now
            }));

            MiniJuegoMatematica mini = new MiniJuegoMatematica(repo.Object);


            PreguntaGeneralDTO pregunta = await mini.GenerarPreguntaServicio();

            Assert.NotNull(pregunta);
        }

        [Fact]
        public async Task MiniJuegoMatematica_GenerarPreguntaServicio_ReturnDtosCorrectos()
        {
            var repo = new Mock<IPreguntasRepository>();

            Pregunta? preguntaGuardada = null;

            repo.Setup(r => r.AgregarPregunta(It.IsAny<Pregunta>()))
                .Callback<Pregunta>(p =>
                {
                    p.Id = 1;
                    preguntaGuardada = p;
                })
                .Returns(Task.CompletedTask);

            var mini = new MiniJuegoMatematica(repo.Object);

            // Act
            PreguntaGeneralDTO dto = await mini.GenerarPreguntaServicio();

            // Assert
            Assert.NotNull(preguntaGuardada);

            Assert.Equal(1, dto.Id);
            Assert.Equal("matematica", dto.tipo);


            var dtoMat = Assert.IsType<PreguntaMatematicaDTO>(dto);


            Assert.Equal(preguntaGuardada!.numeros, dtoMat.numeros);

            repo.Verify(r => r.AgregarPregunta(
                It.Is<Pregunta>(p =>
                    p.tipo == "matematica" &&
                    p.numeros.Length == 3 &&
                 p.respuesta == (p.numeros[0] + p.numeros[1] + p.numeros[2]).ToString()
                )
            ), Times.Once);
        }

        [Fact]
        public async Task MiniJuegoMatematica_ValidarRespuesta_CuandoEsCorrectaDevuelveTrue()
        {
            // Arrange
            var repomock = new Mock<IPreguntasRepository>();
            var pregunta = new Pregunta
            {
                Id = 10,
                tipo = "matematica",
                numeros = new[] { 1, 2, 3 },
                respuesta = "6",
                fechaCreacion = DateTime.Now
            };

            repomock.Setup(r => r.TraerPreguntaPorId(10)).ReturnsAsync(pregunta);

            var minijuego = new MiniJuegoMatematica(repomock.Object);

            //act 
            ValidacionRespuestaDTO resultado =
                await minijuego.ValidarRespuesta(10, "6");

            // assert 
            Assert.True(resultado.esCorrecta);
            Assert.Equal("Respuesta correcta.", resultado.mensaje);
            Assert.Equal("6", resultado.respuestaCorrecta);
            Assert.Equal("matematica", resultado.tipoMiniJuego);

        }

        [Fact]
        public async Task MiniJuegoMatemaitca_ValidarRespuesta_CuandoEsIncorrectaDevuelveFalse()
        {
            // arrange 
            var repomock = new Mock<IPreguntasRepository>();
            var pregunta = new Pregunta
            {
                Id = 11,
                tipo = "matematica",
                numeros = new[] { 1, 2, 3 },
                respuesta = "6",
                fechaCreacion = DateTime.Now
            };

            repomock.Setup(r => r.TraerPreguntaPorId(11)).ReturnsAsync(pregunta);

            var minijuego = new MiniJuegoMatematica(repomock.Object);

            // act
            ValidacionRespuestaDTO resultado = await minijuego.ValidarRespuesta(11, "1111");

            //assert 

            Assert.False(resultado.esCorrecta);
            Assert.Equal("Respuesta incorrecta.", resultado.mensaje);
            Assert.Equal("6", resultado.respuestaCorrecta);
            Assert.Equal("matematica", resultado.tipoMiniJuego);
        }


    }
}