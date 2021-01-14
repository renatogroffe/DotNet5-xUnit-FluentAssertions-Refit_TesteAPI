using System.Threading.Tasks;
using Refit;
using APITemperatura.Testes.Models;

namespace APITemperatura.Testes.HttpClients
{
    public interface IConversorTemperaturasAPI
    {
        [Get("/ConversorTemperaturas/Fahrenheit/{vlFahrenheit}")]
        Task<ApiResponse<Temperatura>> GetAsync(double vlFahrenheit);
    }
}