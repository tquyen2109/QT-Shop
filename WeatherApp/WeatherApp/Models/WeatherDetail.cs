namespace WeatherApp.Models
{
    public class WeatherDetail
    {
        public Request Request { get; set; }
        public Location Location { get; set; }
        public Current Current { get; set; }
    }
}