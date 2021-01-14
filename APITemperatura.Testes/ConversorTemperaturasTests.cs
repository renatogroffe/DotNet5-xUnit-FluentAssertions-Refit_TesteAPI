using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Xunit;
using FluentAssertions;
using Refit;
using APITemperatura.Testes.HttpClients;

namespace APITemperatura.Testes
{
    public class ConversorTemperaturasTests
    {
        private readonly IConversorTemperaturasAPI _apiConvTemperaturas;

        public ConversorTemperaturasTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json");
            var configuration = builder.Build();

            _apiConvTemperaturas = RestService.For<IConversorTemperaturasAPI>(
                configuration["UrlWebApp"]);
        }

        [Theory]
        [InlineData(32, 0, 273.15)]
        [InlineData(86, 30, 303.15)]
        [InlineData(47, 8.33, 281.48)]
        [InlineData(90.5, 32.5, 305.65)]
        [InlineData(120.18, 48.99, 322.14)]
        [InlineData(212, 100, 373.15)]
        [InlineData(-459.67, -273.15, 0)]
        public async Task TestarConversoesValidas(
            double vlFahrenheit,
            double vlCelsius,
            double vlKelvin)
        {
            var response = await _apiConvTemperaturas.GetAsync(vlFahrenheit);
            response.StatusCode.Should().Be(HttpStatusCode.OK,
                $"* Ocorreu uma falha: Status Code esperado (200, OK) diferente do resultado gerado *");

            var resultado = response.Content;
            resultado.Celsius.Should().Be(vlCelsius,
                "* Ocorreu uma falha: os valores na escala Celsius nao correspondem *");
            resultado.Kelvin.Should().Be(vlKelvin,
                "* Ocorreu uma falha: os valores na escala Kelvin nao correspondem *");
        }

        [Theory]
        [InlineData(-459.68)]
        [InlineData(-500)]
        [InlineData(-1000.99)]
        public async Task TestarFalhas(double vlFahrenheit)
        {
            var response = await _apiConvTemperaturas.GetAsync(vlFahrenheit);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest,
                "* Ocorreu uma falha: Status Code esperado para a temperatura de" +
               $"{vlFahrenheit} graus Fahrenheit: 400 (Bad Request) *");
        }
    }
}