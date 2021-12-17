using Magicodes.ExporterAndImporter.Excel;
using OfficeOpenXml.Table;
using System.ComponentModel;

namespace Blazor.Magicodes.IE.Data
{
    [ExcelImporter(IsLabelingError = true)]
    [ExcelExporter(Name = "导入商品中间表", TableStyle = TableStyles.Light10, AutoFitAllColumn = true)]
    public class WeatherForecast
    {
        [DisplayName("日期")]
        public DateTime Date { get; set; }

        [DisplayName("气温")]
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}