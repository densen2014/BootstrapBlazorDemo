using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.BB.Table.Sqlite.Pages
{
    public partial class GenTableDynamicCheckboxList
    {

        private DataTableDynamicContext DataTableDynamicContext { get; set; }
        private DataTable UserData { get; } = new DataTable();

        [Inject]
        [NotNull]
        private DialogService? DialogService { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<SiteItem>? Localizer { get; set; }

        private IEnumerable<string> WorkplanValue { get; set; } = new string[] { "0", "1", "2", "3" };

        private SiteItem Model { get; set; } = new SiteItem()
        {
            url = "",
            keyword = "",
            workplan = "",
            state = "ready"
        };

        [NotNull]
        private List<SiteItem> Items { get; set; }

        protected override void OnInitialized()
        {
            GetData();
            InitDataTable();
        }

        private void GetData()
        {
            UserData.Rows.Clear();
            UserData.Columns.Add(nameof(SiteItem.workplan), typeof(string));
            UserData.Columns.Add(nameof(SiteItem.id), typeof(int));
            UserData.Columns.Add(nameof(SiteItem.url), typeof(string));
            UserData.Columns.Add(nameof(SiteItem.keyword), typeof(string));
            UserData.Columns.Add(nameof(SiteItem.direction), typeof(string));
            UserData.Columns.Add(nameof(SiteItem.state), typeof(string));

            for (int i = 0; i < 10; i++)
            {
                UserData.Rows.Add("2,3",i, "item.url", "item.keyword" + i,  "item.direction", "item.state");

            }
        }

        private void InitDataTable()
        {
            DataTableDynamicContext = new DataTableDynamicContext(UserData, (context, col) =>
            {
                //if (col.GetFieldName() == nameof(SiteItem.id))
                //{
                //    col.Editable = false;
                //    col.Width = 30;
                //}
                //if (col.GetFieldName() == nameof(SiteItem.url))
                //{
                //    col.Width = 150;
                //}
                //if (col.GetFieldName() == nameof(SiteItem.keyword))
                //{
                //    //col.Width = 150;
                //}
                if (col.GetFieldName() == nameof(SiteItem.workplan))
                {
                    col.ComponentType = typeof(CheckboxList<string>);
                    col.Items = SiteItem.timezone;
                    col.Width = 150;
                }
                //if (col.GetFieldName() == nameof(SiteItem.direction))
                //{
                //    col.ComponentType = typeof(Select<string>);
                //    col.Items = typeof(SiteItem.Direcitons).ToSelectList();
                //    col.Width = 100;
                //}
                //if (col.GetFieldName() == nameof(SiteItem.state))
                //{
                //    col.Editable = false;
                //    col.Width = 50;
                //}

            });

            //DataTableDynamicContext.OnChanged = async args =>
            //{
            //    if (args.ChangedType == DynamicItemChangedType.Add)
            //    {
            //        await ShowAddDia(args);
            //    }
            //};

            DataTableDynamicContext.OnAddAsync = async args =>
            {
                    await ShowAddDia(args.FirstOrDefault());
            };

            var method = DataTableDynamicContext.OnValueChanged;
            DataTableDynamicContext.OnValueChanged = async (model, col, val) =>
            {
                // 调用内部提供的方法 
                if (method != null)
                {
                    await method(model, col, val);
                    if (col.GetFieldName() != "id" && col.GetFieldName() != "state")
                    {
                        int id = (int)model.GetValue("id");
                        string filedname = col.GetFieldName();
                        string value = val.ToString();
                        //UpdateData(id, filedname, value);
                    }
                }
            };

            DataTableDynamicContext.OnDeleteAsync = items =>
            {
                foreach (var it in items)
                {
                    var id = it.GetValue("id");
                    //DeleteData((int)id);
                }
                UserData.AcceptChanges();
                return Task.FromResult(true);
            };
        }

        private async Task ShowAddDia(IDynamicObject args)
        {
            var items = Utility.GenerateEditorItems<SiteItem>();

            var item = items.First(i => i.GetFieldName() == nameof(SiteItem.url));
            item.Items = SiteItem.GenerateHobbys(Localizer);

            item = items.First(i => i.GetFieldName() == nameof(SiteItem.state));
            item.Editable = false;

            var option = new EditDialogOption<SiteItem>()
            {
                Title = "Add Site Dialog",
                Model = Model,
                Items = items,
                ItemsPerRow = 1,
                RowType = RowType.Inline,
                SaveButtonText = "AddSite",
                OnCloseAsync = () =>
                {
                    //Trace.Log("关闭按钮被点击");
                    return Task.CompletedTask;
                },
                OnEditAsync = context =>
                {
                    var id = IncertData(Model.url, Model.keyword, string.Join(",", WorkplanValue), Model.directions.ToString());
                    Model.id = id.id;
                    Model.direction = Model.directions.ToString();
                    Model.workplan = id.workplan;
                    var fitem = args;
                    fitem.SetValue(nameof(SiteItem.id), Model.id);
                    fitem.SetValue(nameof(SiteItem.url), Model.url);
                    fitem.SetValue(nameof(SiteItem.keyword), Model.keyword);
                    fitem.SetValue(nameof(SiteItem.workplan), Model.workplan);
                    fitem.SetValue(nameof(SiteItem.direction), Model.direction);
                    fitem.SetValue(nameof(SiteItem.state), Model.state);
                    UserData.AcceptChanges();
                    StateHasChanged();
                    return Task.FromResult(true);
                }
            };
            await DialogService.ShowEditDialog(option);
        }

        private SiteItem IncertData(string surl, string skeyword, string sworkplan, string sdirection)
        {
            return null;
        }

        public class SiteItem
        {
            [Display(Name = "主键")]
            [AutoGenerateColumn(Ignore = true)]
            public int id { get; set; }

            [Required(ErrorMessage = "{0}不能为空")]
            [AutoGenerateColumn(Order = 10, Filterable = true, Searchable = true)]
            [Display(Name = "url")]
            public string url { get; set; }

            [Required(ErrorMessage = "{0}不能为空")]
            [AutoGenerateColumn(Order = 20, Filterable = true, Searchable = true)]
            [Display(Name = "keyword")]
            public string keyword { get; set; }

            [Required(ErrorMessage = "{0}不能为空")]
            [AutoGenerateColumn(Order = 40, Filterable = true, Searchable = true)]
            [Display(Name = "state")]
            public string state { get; set; }

            [Required(ErrorMessage = "Please choose direction")]
            [AutoGenerateColumn(Order = 30, Filterable = true, Searchable = true)]
            //[AutoGenerateColumn(Ignore = true)]
            [Display(Name = "direction")]
            public string direction { get; set; }

            [Required(ErrorMessage = "Please choose workplan")]
            //[AutoGenerateColumn(Order = 30, Filterable = true, Searchable = true)]
            [AutoGenerateColumn(ComponentType = typeof(CheckboxList<string>), ComponentItems = typeof(Timezone) )]
            [Display(Name = "workplan")]
            public string workplan { get; set; }

            public Direcitons directions { get; set; }
            public static IEnumerable<SelectedItem> timezone = new List<SelectedItem>(new List<SelectedItem>
            {
                new SelectedItem { Text = "0:00H", Value = "0" },
                new SelectedItem { Text = "1:00H", Value = "1" },
                new SelectedItem { Text = "2:00H", Value = "2" },
                new SelectedItem { Text = "3:00H", Value = "3" },
            });
            public enum Timezone
            {
                /// <summary>
                /// 0:00H
                /// </summary>
                [Description("0:00H")]
                H0,
                /// <summary>
                /// 1:00H
                /// </summary>
                [Description("1:00H")]
                H1,
                /// <summary>
                /// 2:00H
                /// </summary>
                [Description("2:00H")]
                H2,
            }

            public static IEnumerable<SelectedItem> GenerateHobbys(IStringLocalizer<SiteItem> localizer) => localizer["Hobbys"].Value.Split(",").Select(i => new SelectedItem(i, i)).ToList();

            public enum Direcitons
            {
                [Display(Name = "Russion")]
                Russion,
                [Display(Name = "English")]
                English,
                [Display(Name = "Dari")]
                Dari,
                [Display(Name = "Turkish")]
                Turkish,
                [Display(Name = "Pashto")]
                Pashto
            }
        }







    }
}