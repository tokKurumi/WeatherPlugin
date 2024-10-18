using FluentResults;
using WeatherPlugin.Models;

namespace WeatherPlugin.Services.IServices;

public interface IWeatherService
{
    public Task<Result<WeatherData>> GetWeatherCity(string city);
}
