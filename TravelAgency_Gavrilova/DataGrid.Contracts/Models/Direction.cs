using System;
using System.ComponentModel;
using System.Linq;

namespace DataGrid.Contracts.Models
{
    public enum Direction
    {
        [Description("Турция")]
        Turckey = 1,

        [Description("Испания")]
        Spain = 2,

        [Description("Италия")]
        Italy = 3,

        [Description("Франция")]
        France = 4,

        [Description("Шушары")]
        Shushary = 5,
    }
}
