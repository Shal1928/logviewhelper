using System;
using System.Globalization;
using System.Windows.Data;
using LogViewHelper.A0_Models;
using UseAbilities.Extensions.Helpers;
using UseAbilities.Extensions.ObjectExt;
using UseAbilities.WPF.Converters.Base;

namespace LogViewHelper.B0_Converters
{
    [ValueConversion(typeof(EnumViewWrapper<TimeType>), typeof(TimeType))]
    public class TimeTypeWrapConverter : ConverterBase<TimeTypeWrapConverter>
    {
        public TimeTypeWrapConverter()
        {
            //
        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var wrappedEnum = value as EnumViewWrapper<TimeType>;
            if (wrappedEnum.IsNull()) throw new ArgumentException("value is not EnumViewWrapper<TimeType>!");

            return wrappedEnum.Value;
        }
    }
}
