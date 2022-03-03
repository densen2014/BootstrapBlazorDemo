// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using AME.CommonUtils;
using AME.Services;
using AmeBlazor.Components;
using Demo.BB.Table.Sqlite.Data;
using Microsoft.AspNetCore.Components;

namespace BlazorWpf1.Pages
{
    public partial class Index
    {
        // 由于使用了FreeSql ORM 数据服务,可以直接取对象 
        [Inject] IFreeSql fsql { get; set; }

        //用演示服务的数据初始化数据库
        [Inject] WeatherForecastService ForecastService { get; set; }

        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (firstRender)
        //    {
        //        //懒的人,直接初始化一些数据用用
        //        var res = fsql.Select<WeatherForecast>().Count();
        //        if (res == 0)
        //        {
        //            var forecasts = (await ForecastService.GetForecastAsync(DateTime.Now)).ToList();
        //            fsql.Insert<WeatherForecast>().AppendData(forecasts).ExecuteAffrows();
        //        }
        //    }
        //}

    }
}


