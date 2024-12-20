using System.ComponentModel;

namespace DataGrid.Contracts.Models
{
    /// <summary>
    /// Перечисление представляющее направления туров.
    /// </summary>
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
