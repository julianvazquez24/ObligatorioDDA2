using Xunit;
using Moq;
using ObligatorioDDA2.MinijuegosAPI.Services;
using ObligatorioDDA2.MinijuegosAPI.Models;
using ObligatorioDDA2.MinijuegosAPI.Models.DTOs;
using ObligatorioDDA2.MinijuegosAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Obligatorio2.Tests
{
    public class MiniJuegoContrllerTests
    {
        [Fact]
        public async Task MiniJuegoController_GenerarPregunta_SiTipoEsInvalido_TiraBadRequest()
        {
            var repomock = new Mock<IPreguntasRepository>();

            var minijuegoMatematica = new MiniJuegoMatematica(repomock.Object);
            var minijuegoLogica = new MiniJuegoLogica(repomock.Object);
            var minijuegoMemoria = new MiniJuegoMemoria(repomock.Object);

            var controller = new MinijuegosController(
                repomock.Object,
                minijuegoMatematica,
                minijuegoLogica,
                minijuegoMemoria);

            var resultado = await controller.GenerarPregunta("INVALIDO");

            var badrequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.Equal("Tipo de minijuego no válido.", badrequest.Value);
        }

        [Fact]
        public async Task MiniJuegoController_GenerarPregunta_CuandoTipoEsMatematica_DevuelveOkConPreguntaMatematica()
        {
            var repomock = new Mock<IPreguntasRepository>();

            var minijuegoMatematica = new MiniJuegoMatematica(repomock.Object);
            var minijuegoLogica = new MiniJuegoLogica(repomock.Object);
            var minijuegoMemoria = new MiniJuegoMemoria(repomock.Object);

            var controller = new MinijuegosController(
                repomock.Object,
                minijuegoMatematica,
                minijuegoLogica,
                minijuegoMemoria);

            var resultado = await controller.GenerarPregunta("MATEMATICA");

            var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
            var dto = Assert.IsType<PreguntaMatematicaDTO>(resultadoOk.Value);

            Assert.Equal("matematica", dto.tipo);
            Assert.Equal(3, dto.numeros.Length);
        }

        [Fact]
        public async Task MiniJuegoController_GenerarPregunta_CuandoTipoEsLogica_DevuelveOkConPreguntaLogica()
        {
            var repomock = new Mock<IPreguntasRepository>();

            var minijuegoMatematica = new MiniJuegoMatematica(repomock.Object);
            var minijuegoLogica = new MiniJuegoLogica(repomock.Object);
            var minijuegoMemoria = new MiniJuegoMemoria(repomock.Object);

            var controller = new MinijuegosController(
                repomock.Object,
                minijuegoMatematica,
                minijuegoLogica,
                minijuegoMemoria);

            var resultado = await controller.GenerarPregunta("LOGICA");

            var resultadoOk = Assert.IsType<OkObjectResult>(resultado);
            var dto = Assert.IsType<PreguntaLogicaDTO>(resultadoOk.Value);

            Assert.Equal("logica", dto.tipo);
            Assert.Equal(3, dto.numeros.Length);
        }

        [Fact]
        public async Task MinijuegoController_ValidarRespuesta_CuandoPreguntaNoExiste_TiraNotFound()
        {
            var repomock = new Mock<IPreguntasRepository>();

            repomock.Setup(r => r.TraerPreguntaPorId(1))
                .ReturnsAsync((Pregunta)null);

            var controller = new MinijuegosController(
                repomock.Object,
                new MiniJuegoMatematica(repomock.Object),
                new MiniJuegoLogica(repomock.Object),
                new MiniJuegoMemoria(repomock.Object));

            var resultado = await controller.ValidarRespuesta(1, "9999");

            var devuelveNotFound = Assert.IsType<NotFoundObjectResult>(resultado);
            Assert.Equal("Pregunta no encontrada.", devuelveNotFound.Value);
        }

        [Fact]
        public async Task MiniJuegoController_ValidarRespuesta_CuandoPreguntaEsTipoMatematica_DevuelveOkYEsCorrecta()
        {
            var repomock = new Mock<IPreguntasRepository>();

            var pregunta = new Pregunta
            {
                Id = 10,
                tipo = "matematica",
                numeros = new[] { 2, 3, 5 },
                respuesta = "10"
            };

            repomock.Setup(r => r.TraerPreguntaPorId(10))
                .ReturnsAsync(pregunta);

            var controller = new MinijuegosController(
                repomock.Object,
                new MiniJuegoMatematica(repomock.Object),
                new MiniJuegoLogica(repomock.Object),
                new MiniJuegoMemoria(repomock.Object));

            var resultado = await controller.ValidarRespuesta(10, "10");

            var ok = Assert.IsType<OkObjectResult>(resultado);
            var dto = Assert.IsType<ValidacionRespuestaDTO>(ok.Value);

            Assert.True(dto.esCorrecta);
            Assert.Equal("matematica", dto.tipoMiniJuego);
        }

        [Fact]
        public async Task MiniJuegoController_ValidarRespuesta_CuandoTipoDePreguntaEsDesconocido_DevuelveBadRequest()
        {
            var repomock = new Mock<IPreguntasRepository>();

            var pregunta = new Pregunta
            {
                Id = 20,
                tipo = "otro"
            };

            repomock.Setup(r => r.TraerPreguntaPorId(20))
                .ReturnsAsync(pregunta);

            var controller = new MinijuegosController(
                repomock.Object,
                new MiniJuegoMatematica(repomock.Object),
                new MiniJuegoLogica(repomock.Object),
                new MiniJuegoMemoria(repomock.Object));

            var resultado = await controller.ValidarRespuesta(20, "algo");

            var bad = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.Equal("Tipo de minijuego no válido.", bad.Value);
        }
    }
}
