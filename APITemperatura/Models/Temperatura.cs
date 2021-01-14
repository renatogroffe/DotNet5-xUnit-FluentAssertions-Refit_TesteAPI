using System;

namespace APITemperatura.Models
{
    public class Temperatura
    {
        public DateTime Data { get; }
        public double Fahrenheit { get; }
        public double Celsius { get; }
        public double Kelvin { get; }

        public Temperatura(double temperaturaFahrenheit)
        {
            Data = DateTime.Now;

            if (temperaturaFahrenheit < -459.67)
            {
                throw new ArgumentException(
                    $"Temperatura invalida na escala Fahrenheit: {temperaturaFahrenheit}");
            }

            Fahrenheit = temperaturaFahrenheit;

            Celsius = (temperaturaFahrenheit - 32.0) / 1.8; // Simulação de falha
            Kelvin = Celsius + 273.15; // Simulação de falha

            //Celsius = Math.Round((temperaturaFahrenheit - 32.0) / 1.8, 2);
            //Kelvin = Math.Round(Celsius + 273.15, 2);
        }
    }
}