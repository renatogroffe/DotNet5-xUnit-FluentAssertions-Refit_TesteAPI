using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using APITemperatura.Models;

namespace APITemperatura.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConversorTemperaturasController : ControllerBase
    {
        private readonly ILogger<ConversorTemperaturasController> _logger;

        public ConversorTemperaturasController(
            ILogger<ConversorTemperaturasController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Fahrenheit/{temperatura}")]
        [ProducesResponseType(typeof(Temperatura), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(FalhaCalculo), (int)HttpStatusCode.BadRequest)]
        public ActionResult<Temperatura> GetConversaoFahrenheit(double temperatura)
        {
            _logger.LogInformation(
                $"Recebida temperatura para conversão: {temperatura}");

            // Comentário para simulação de falha
            /*if (temperatura < -459.67)
            {
                var mensagemErro =
                    $"Valor de temperatura em Fahrenheit invalido: {temperatura}";
                _logger.LogError(mensagemErro);
                return new BadRequestObjectResult(
                    new FalhaCalculo()
                    {
                        Mensagem = mensagemErro
                    });
            }*/

            var resultado = new Temperatura(temperatura);
            _logger.LogInformation(
                $"{resultado.Fahrenheit} graus Fahrenheit = " +
                $"{resultado.Celsius} graus Celsius = " +
                $"{resultado.Kelvin} graus Kelvin");
            return resultado;
        }
    }
}