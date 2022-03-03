using Demo.BB.Table.Sqlite.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlazorWpf1
{
    public static class Startup
    {
        public static IServiceProvider? Services { get; private set; }

        public static void Init()
        {
            var host = Host.CreateDefaultBuilder()
                           .ConfigureServices(WireupServices)
                           .Build();
            Services = host.Services;
        }

        private static void WireupServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddBlazorWebView();
            services.AddSingleton<WeatherForecastService>();
            // 增加 FreeSql ORM 数据服务操作类
            // 需要时打开下面代码
            // 需要引入 FreeSql 对 SQLite 的扩展包 FreeSql.Provider.Sqlite
            services.AddFreeSql(option =>
            {
                option.UseConnectionString(FreeSql.DataType.Sqlite, "Data Source=test.db;")
#if DEBUG
                     //开发环境:自动同步实体
                     .UseAutoSyncStructure(true)
                     .UseNoneCommandParameter(true)
                     //调试sql语句输出
                     .UseMonitorCommand(cmd => System.Console.WriteLine(cmd.CommandText))
#endif
                    ;
            });
            services.AddDensenExtensions();
        }
    }
}