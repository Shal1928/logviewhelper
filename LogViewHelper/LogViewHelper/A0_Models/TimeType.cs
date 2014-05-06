using System.ComponentModel;

namespace LogViewHelper.A0_Models
{
    public enum TimeType
    {
        [Description("мсек")]
        Ms = 0,
        [Description("сек")]
        Sec,
        [Description("мин")]
        Min,
        [Description("ч")]
        Hour
    }
}
