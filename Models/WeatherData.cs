namespace WeatherPlugin.Models;

public record WeatherData(
    string City,
    string CountryShort,
    string Country,
    int Temperature,
    int FeelTemperature,
    string Weather,
    string WeatherDescription);

