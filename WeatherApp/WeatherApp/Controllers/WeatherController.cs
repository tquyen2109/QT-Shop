using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherApiService _weatherApiService;

        public WeatherController(IWeatherApiService weatherApiService)
        {
            _weatherApiService = weatherApiService;
        }

        [HttpGet]
        public async Task<WeatherDetail> GetWeatherDetails(string query)
        {
            return await _weatherApiService.GetWeatherDetails(query);
        }
    }
}