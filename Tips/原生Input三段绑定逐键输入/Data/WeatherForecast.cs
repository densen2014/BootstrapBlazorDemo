namespace BlazorApp2.Data
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
        public TemperatureCs? temperatureCs { get; set; }
    } 
    
    public class TemperatureCs
    {

        public string TemperatureC1 { get; set; } = "1";
        public string TemperatureC2 { get; set; }="2";
        public string TemperatureC3 { get; set; } = "3";
 
    }
}