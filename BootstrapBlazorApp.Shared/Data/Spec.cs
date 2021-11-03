using BootstrapBlazor.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 public class Spec
{
    [AutoGenerateColumn(Editable = false)]
    [DisplayName("艾迪")]
    public int id { get; set; }

    [DisplayName("名字")]
    public string Name { get; set; }
}

