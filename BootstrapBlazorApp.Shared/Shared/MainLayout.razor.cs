using BootstrapBlazor.Components;
using System.Collections.Generic;

namespace BootstrapBlazorApp.Shared.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class MainLayout
    {
        private bool UseTabSet { get; set; } = true;

        private string Theme { get; set; } = "";

        private bool IsOpen { get; set; }

        private bool IsFixedHeader { get; set; } = true;

        private bool IsFixedFooter { get; set; } = true;

        private bool IsFullSide { get; set; } = true;

        private bool ShowFooter { get; set; } = true;

        private List<MenuItem> Menus { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // TODO: 菜单获取可以通过数据库获取，此处为示例直接拼装的菜单集合
            Menus = GetIconSideMenuItems();
        }

        private static List<MenuItem> GetIconSideMenuItems()
        {
            var menus1 = new List<MenuItem>
            {
                new MenuItem() { Text = "1组件库", Icon = "fa fa-fw fa-home", Url = "https://www.blazor.zone/components" },
                new MenuItem() { Text = "11Index", Icon = "fa fa-fw fa-fa", Url = "" },
                new MenuItem() { Text = "1Counter", Icon = "fa fa-fw fa-check-square-o", Url = "counter" },
                new MenuItem() { Text = "1FetchData", Icon = "fa fa-fw fa-database", Url = "fetchdata" },
                new MenuItem() { Text = "1Table", Icon = "fa fa-fw fa-table", Url = "table" },
                new MenuItem() { Text = "1InCell编辑", Icon = "fa fa-fw fa-table", Url = "incell" },
                new MenuItem() { Text = "1项目地址", Icon = "fa fa-fw fa-table", Url = "src" }
            };
            var menus2 = new List<MenuItem>
            {
                new MenuItem() { Text = "222", Icon = "fa fa-fw fa-home", Url = "https://www.blazor.zone/components" },
                new MenuItem() { Text = "222222222Index", Icon = "fa fa-fw fa-fa", Url = "" },
                new MenuItem() { Text = "22Counter", Icon = "fa fa-fw fa-check-square-o", Url = "counter" },
                new MenuItem() { Text = "22FetchData", Icon = "fa fa-fw fa-database", Url = "fetchdata" },
                new MenuItem() { Text = "22Table", Icon = "fa fa-fw fa-table", Url = "table" },
                new MenuItem() { Text = "22InCell编辑", Icon = "fa fa-fw fa-table", Url = "incell" },
                new MenuItem() { Text = "22项目地址", Icon = "fa fa-fw fa-table", Url = "src" }
            };

            var menus = new List<MenuItem>
            {
                new MenuItem() { Text = "组件库", Icon = "fa fa-fw fa-home", Url = "https://www.blazor.zone/components" },
                new MenuItem() { Text = "Index", Icon = "fa fa-fw fa-fa", Url = "" },
                new MenuItem() { Text = "Counter", Icon = "fa fa-fw fa-check-square-o", Url = "counter" },
                new MenuItem() { Text = "FetchData", Icon = "fa fa-fw fa-database", Url = "fetchdata" },
                new MenuItem() { Text = "Table", Icon = "fa fa-fw fa-table", Url = "table" },
                new MenuItem() { Text = "InCell编辑", Icon = "fa fa-fw fa-table", Url = "incell", Items = menus2 },
                new MenuItem() { Text = "项目地址", Icon = "fa fa-fw fa-table", Url = "src"  , Items = menus1 }
            };

            return menus;
        }
    }
}
