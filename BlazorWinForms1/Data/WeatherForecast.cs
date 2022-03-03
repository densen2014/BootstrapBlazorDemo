using BootstrapBlazor.Components;
using FreeSql.DataAnnotations;
using System.ComponentModel;

namespace Demo.BB.Table.Sqlite.Data
{
    [AutoGenerateClass(Searchable = true, Filterable = true, Sortable = true)]
    public class WeatherForecast
    {
        [Column(IsIdentity = true)]
        [DisplayName("序号")]
        public int ID { get; set; }
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}