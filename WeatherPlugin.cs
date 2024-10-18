using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using WeatherPlugin.Models;
using WeatherPlugin.Services.IServices;

namespace WeatherPlugin;

public class WeatherPlugin(
    IWeatherService weatherClient,
    IServiceProvider serviceProvider
    ) : BasePlugin, IPluginConfig<APIConfig>
{
    private readonly IWeatherService _weatherClient = weatherClient;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public override string ModuleName => "Weather Plugin via openweathermap.org";
    public override string ModuleDescription => "Get Weather status base city name with HttpsRequest to api.openweathermap.org";
    public override string ModuleVersion => "1.0.0.1";
    public override string ModuleAuthor => "Oylsister, kurumi";

    public required APIConfig Config { get; set; }

    public void OnConfigParsed(APIConfig config)
    {
        Config = config;

        var apiConfig = _serviceProvider.GetRequiredService<APIConfig>();
        apiConfig.Token = Config.Token;
        apiConfig.WeatherUrl = Config.WeatherUrl;
    }

    [CommandHelper(1, "css_weather <city name>")]
    [ConsoleCommand("css_weather")]
    public async Task WeatherCommandHandler(CCSPlayerController? playerController, CommandInfo info)
    {
        var weatherResult = await _weatherClient.GetWeatherCity(info.GetArg(1));
        if (weatherResult.IsFailed)
        {
            info.ReplyToCommand($"{ChatColors.DarkRed}####### WEATHER ERROR #######");
            info.ReplyToCommand($"Reasons: {JsonConvert.SerializeObject(weatherResult.Errors)}");
            return;
        }

        var weather = weatherResult.Value;

        info.ReplyToCommand($"{ChatColors.Green}####### WEATHER #######");
        info.ReplyToCommand($"{ChatColors.Lime}Location: {ChatColors.Default}{weather.City}, {weather.Country} {weather.CountryShort}");
        info.ReplyToCommand($"{ChatColors.Lime}Temperature: {ChatColors.Default}{weather.Temperature}°C | Feels like: {weather.FeelTemperature}°C");
        info.ReplyToCommand($"{ChatColors.Lime}Weather: {ChatColors.Default}{weather.Weather}, {weather.WeatherDescription}");
    }
}
