using CounterStrikeSharp.API.Core;
using System.Text.Json.Serialization;

namespace WeatherPlugin.Models;

public class APIConfig : BasePluginConfig
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string WeatherUrl { get; set; } = string.Empty;
}
