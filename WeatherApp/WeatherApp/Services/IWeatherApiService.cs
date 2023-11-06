using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IWeatherApiService
    {
        public Task<WeatherDetail> GetWeatherDetails(string query);
    }
}