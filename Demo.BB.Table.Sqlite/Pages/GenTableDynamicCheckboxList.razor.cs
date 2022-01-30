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


        private SiteItem Model { get; set; } = new SiteItem()
        {
            workplan = "",
        };


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

            for (int i = 0; i < 10; i++)
            {
                UserData.Rows.Add("H2,H3",i);

            }
        }

        private void InitDataTable()
        {
            DataTableDynamicContext = new DataTableDynamicContext(UserData, (context, col) =>
            {
                //if (col.GetFieldName() == nameof(SiteItem.workplan))
                //{
                //    col.ComponentType = typeof(CheckboxList<string>);
                //    col.ComponentItems = typeof(SiteItem.Timezone);
                //    //col.Items =typeof( SiteItem.Timezone).ToSelectList();
                //    col.Width = 150;
                //}

            });

            DataTableDynamicContext.OnChanged = async args =>
            {
                if (args.ChangedType == DynamicItemChangedType.Add)
                {
                    await ShowAddDia(args);
                }
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

        private async Task ShowAddDia(DynamicObjectContextArgs args)
        {
            var items = Utility.GenerateEditorItems<SiteItem>();

            //var col = items.First(i => i.GetFieldName() == nameof(SiteItem.workplan));//查找到workplan列
            //col.ComponentType = typeof(CheckboxList<IEnumerable<string>>); //设定组件类型
            //col.Items = SiteItem.timezone;

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
                    var id = 9999;
                    Model.id = id;
                    var fitem = args.Items.FirstOrDefault();
                    fitem.SetValue(nameof(SiteItem.id), Model.id);
                    fitem.SetValue(nameof(SiteItem.workplan), Model.workplan);
                    UserData.AcceptChanges();
                    StateHasChanged();
                    return Task.FromResult(true);
                }
            };
            await DialogService.ShowEditDialog(option);
        }

      

        public class SiteItem
        {
            [Display(Name = "主键")]
            [AutoGenerateColumn(Ignore = true)]
            public int id { get; set; }

            [Required(ErrorMessage = "Please choose workplan")]
            //[AutoGenerateColumn(Order = 30, Filterable = true, Searchable = true)]
            [AutoGenerateColumn(ComponentType = typeof(CheckboxList<string>), ComponentItems = typeof(Timezone) )]
            [Display(Name = "workplan")]
            public string workplan { get; set; }

            //public static IEnumerable<SelectedItem> timezone = new List<SelectedItem>(new List<SelectedItem>
            //{
            //    new SelectedItem { Text = "0:00H", Value = "0" },
            //    new SelectedItem { Text = "1:00H", Value = "1" },
            //    new SelectedItem { Text = "2:00H", Value = "2" },
            //    new SelectedItem { Text = "3:00H", Value = "3" },
            //});
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
                /// <summary>
                /// 3:00H
                /// </summary>
                [Description("3:00H")]
                H3,
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