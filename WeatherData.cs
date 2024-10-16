using System.Globalization;

namespace WeatherPlugin
{
    public class WeatherData
    {
        public string? City { get; set; }
        public string? CountryShort { get; set; }
        public string? Country { get; set; }
        public int? Temperature { get; set; }
        public int? FeelTemperature { get; set; }
        public string? Weather { get; set; }
        public string? WeatherDescription { get; set; }

        public string? Message { get; set; }
        public int Status { get; set; }

        public static string GetCountryNameFromShort(string countryCode)
        {
            RegionInfo regionInfo = new RegionInfo(countryCode);

            if (regionInfo != null)
                return regionInfo.EnglishName;

            return string.Empty;
        }

        public static int KelvinToCelsius(float kelvin)
        {
            return (int)Math.Floor(kelvin - 273.15);
        }
    }
}
