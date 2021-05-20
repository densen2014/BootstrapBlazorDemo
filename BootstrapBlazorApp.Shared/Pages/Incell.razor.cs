using BootstrapBlazorApp.Shared.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BootstrapBlazorApp.Shared.Pages
{
    public partial class InCell
    {

        /// <summary>
        ///
        /// </summary>
        protected static readonly Random random = new();
        protected Foo editingitem = new Foo();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        internal static List<Foo> GenerateItems() => Enumerable.Range(1, 20).Select(i => new Foo()
        {
            Id = i,
            Name = $"张{ConvertToChinese(i)}",
            DateTime = DateTime.Now.AddDays(i - 1),
            Address = $"上海市普陀区金沙江路 {random.Next(1000, 2000)} 弄",
            Count = random.Next(1, 100),
            Complete = random.Next(1, 100) > 50,
            Education = random.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middel
        }).ToList();

        /// <summary>
        ///
        /// </summary>
        protected static List<Foo> Items { get; } = GenerateItems();

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        Task Hi(Foo item)
        {
            editingitem = item;
            toastService.Success($"你上次编辑的是 ID: {editingitem.Id}  Name: {editingitem.Name}  Address: {editingitem.Address}");
            return Task.CompletedTask;
        }
        public static string ConvertToChinese(decimal number)
        {
            var s = number.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            var d = Regex.Replace(s, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            var r = Regex.Replace(d, ".", m => "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万亿兆京垓秭穰"[m.Value[0] - '-'].ToString());
            return r.Replace("元", "");
        }

    }
}
