using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherApiService : IWeatherApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public WeatherApiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<WeatherDetail> GetWeatherDetails(string query)
        {
            var res = await _httpClient.GetFromJsonAsync<WeatherDetail>
                (_config["WeatherAPI"] + "current?access_key=" + _config["WeatherAPIKey"] + "&query=" + query );
            return res;
        }
    }
}