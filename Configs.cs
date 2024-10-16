using CounterStrikeSharp.API.Core;
using System.Text.Json.Serialization;

namespace WeatherPlugin
{
    public class APIConfigs : BasePluginConfig
    {
        [JsonPropertyName("token")]
        private string _token { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        private string _weatherurl { get; set; } = "http://api.openweathermap.org/data/2.5/";

        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }

        public string WeatherUrl
        {
            get { return _weatherurl; }
            set { _weatherurl = value; }
        }
    }
}
