using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.DependencyInjection;
using WeatherPlugin.Models;
using WeatherPlugin.Services.IServices;

namespace WeatherPlugin;

public class WeatherRequestInjection : IPluginServiceCollection<WeatherPlugin>
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<APIConfig>();

        services.AddHttpClient<IWeatherService, WeatherHttpClient>()
            .ConfigureHttpClient((serviceProvider, client) =>
            {
                var config = serviceProvider.GetRequiredService<APIConfig>();
                client.BaseAddress = new Uri(config.WeatherUrl);
            });
    }
}
