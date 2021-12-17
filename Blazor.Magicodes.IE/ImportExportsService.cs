using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;

namespace Blazor.Magicodes.IE
{
    /// <summary>
    /// 通用导入导出服务类
    /// </summary>
    public class ImportExportsService
    {
        public async Task<string> ExportToExcel<T>(string filePath, List<T> items = null) where T : class, new()
        {
            filePath = filePath ?? Path.Combine("D:\\", $"模板_{typeof(T).Name}.xlsx");

            IExporter exporter = new ExcelExporter();
            items = items ?? new List<T>();
            var result = await exporter.Export(filePath, items);
            return result.FileName;
        }
    }
}
