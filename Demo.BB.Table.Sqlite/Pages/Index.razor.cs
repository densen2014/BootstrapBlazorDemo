// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using AME.CommonUtils;
using AME.Services;
using AmeBlazor.Components;
using BootstrapBlazor.Components;
using Demo.BB.Table.Sqlite.Data;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Demo.BB.Table.Sqlite.Pages
{
    public partial class Index
    {
        // 由于使用了FreeSql ORM 数据服务,可以直接取对象 
        [Inject] IFreeSql fsql { get; set; }

        //用演示服务的数据初始化数据库
        [Inject] WeatherForecastService ForecastService { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                //懒的人,直接初始化一些数据用用
                var res = fsql.Select<WeatherForecast>().Count();
                if (res == 0)
                {
                    var forecasts = (await ForecastService.GetForecastAsync(DateTime.Now)).ToList();
                    fsql.Insert<WeatherForecast>().AppendData(forecasts).ExecuteAffrows();
                }
            }
        }


        /// <summary>
        ///
        /// </summary>
        public class Foo
        {
            // 列头信息支持 Display DisplayName 两种标签

            /// <summary>
            ///
            /// </summary>
            [Key]
            [Display(Name = "主键")]
            [AutoGenerateColumn(Ignore = true)]
            public int Id { get; set; }

            /// <summary>
            ///
            /// </summary>
            [Required(ErrorMessage = "{0}不能为空")]
            [AutoGenerateColumn(Order = 10, Filterable = true, Searchable = true)]
            [Display(Name = "姓名")]
            public string? Name { get; set; }
            /// <summary>
            ///
            /// </summary>
            [Required(ErrorMessage = "请选择学历")]
            [Display(Name = "学历")]
            [AutoGenerateColumn(Order = 60)]
            public EnumEducation? Education { get; set; }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public static List<Foo> GenerateFoo(int count = 10) => Enumerable.Range(1, count).Select(i => new Foo()
            {
                Id = i,
                Name = "Foo" + i.ToString(),
                Education = random.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middel
            }).ToList();
            private static readonly Random random = new();

        }

        /// <summary>
        ///
        /// </summary>
        public enum EnumEducation
        {
            /// <summary>
            ///
            /// </summary>
            [Display(Name = "小学")]
            Primary,

            /// <summary>
            ///
            /// </summary>
            [Display(Name = "中学")]
            Middel
        }
        private class DetailRow
        {
            /// <summary>
            /// 
            /// </summary>
            [DisplayName("主键")]
            public int Id { get; set; }

            /// <summary>
            /// 
            /// </summary>
            [DisplayName("培训课程")]
            [AutoGenerateColumn(Order = 10)]
            public string Name { get; set; } = "";

            /// <summary>
            /// 
            /// </summary>
            [DisplayName("日期")]
            [AutoGenerateColumn(Order = 20, Width = 180)]
            public DateTime DateTime { get; set; }


            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public static List<DetailRow> GenerateDetailRow(int count = 3) => Enumerable.Range(1, count).Select(i => new DetailRow()
            {
                Id = i,
                Name = "DetailRow" + i.ToString(),
                DateTime = DateTime.Now.AddDays(-count)
            }).ToList();

        }

        private List<Foo>? Items { get; set; } = Foo.GenerateFoo();
        private List<DetailRow>? DetailRows(int i) => DetailRow.GenerateDetailRow(i);

        public RenderFragment TablesDetailRow() => builder =>
        {
            builder.OpenComponent(0, typeof(Table<Foo>));
            builder.AddAttribute(1, nameof(Table<Foo>.Items), Items);
            builder.AddAttribute(2, nameof(Table<Foo>.IsDetails), _ = true);
            builder.AddAttribute(3, nameof(Table<Foo>.OnAfterRenderCallback), OnAfterRenderCallback());
            builder.AddAttribute(4, nameof(Table<Foo>.DetailRowTemplate), GenerateDetailRow());
            builder.AddAttribute(5, nameof(Table<Foo>.AutoGenerateColumns), true);
            builder.CloseComponent();
        }; 

    Func<Table<Foo>, Task>? OnAfterRenderCallback()
        {
            return _ => Task.CompletedTask;
        }


        private RenderFragment<Foo> GenerateDetailRow()=>item => builder =>
            {
                builder.OpenComponent(0, typeof(Table<DetailRow>));
                builder.AddAttribute(1, nameof(Table<DetailRow>.Items), DetailRows(item.Id));
                builder.CloseComponent();
            };

    }
}


