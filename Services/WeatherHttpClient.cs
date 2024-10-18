using FluentResults;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using WeatherPlugin.Models;
using WeatherPlugin.Services.IServices;

namespace WeatherPlugin.Services;

public class WeatherHttpClient(
    HttpClient httpClient,
    ILogger<WeatherHttpClient> logger,
    APIConfig configs
    ) : IWeatherService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ILogger<WeatherHttpClient> _logger = logger;
    private readonly APIConfig _apiConfigs = configs;

    public async Task<Result<WeatherData>> GetWeatherCity(string city)
    {
        _logger.LogInformation("Getting weather data for {City}", city);
        using var weatherResponse = await _httpClient.GetAsync($"weather?q={city}&appid={_apiConfigs?.Token}");
        if (!weatherResponse.IsSuccessStatusCode)
        {
            _logger.LogError("Failed to get weather data for {City}", city);
            return Result.Fail<WeatherData>($"Failed to get weather data for {city}");
        }

        var weatherData = await weatherResponse.Content.ReadFromJsonAsync<WeatherData>();
        if (weatherData is null)
        {
            _logger.LogError("Failed to parse weather data");
            return Result.Fail<WeatherData>("Failed to parse weather data");
        }

        return Result.Ok(weatherData);
    }
}
