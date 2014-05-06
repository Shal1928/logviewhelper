using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Data;
using UseAbilities.Extensions.EnumerableExt;
using UseAbilities.Extensions.StringExt;
using UseAbilities.WPF.Converters.Base;

namespace LogViewHelper.B0_Converters
{
    [ValueConversion(typeof(string), typeof(TimeSpan))]
    public class StringToTimeSpanConverter : ConverterBase<StringToTimeSpanConverter>
    {
        public StringToTimeSpanConverter()
        {
            //
        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = value.ToString();
            if (stringValue.Length > 10) stringValue = stringValue.Remove(9, stringValue.Length - 10);

            return stringValue;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = value as string;
            TimeSpan result;
            if (string.IsNullOrWhiteSpace(stringValue)) throw new ArgumentException("Value is not string or empty or white spaces");
            stringValue = stringValue.Replace(",", ".");
            
            //00:00:00.000 (12)
            var validString = Regex.Match(stringValue, @"(([0-1][0-9])|([2][0-3]))[:]?([0-5][0-9]?\d)?[:]?([0-5][0-9]?\d)?[.]?(\d{0,3})").ToString();
            if (!validString.IsEmpty())
                return TimeSpan.Parse(validString.ClearEdges(null,":"));

            const int maxLength = 12;
            if (stringValue.Length > maxLength) stringValue = stringValue.Remove(stringValue.IndexOfLast() - maxLength, maxLength);

            if (stringValue.Contains(":"))
            {
                const char splitChar = ':';
                var stringArray = stringValue.Split(splitChar);

            }

            if (stringValue.Length < 5)
            {
                if (stringValue.Contains(":"))
                {
                    const char splitChar = ':';
                    var stringArray = stringValue.Split(splitChar);
                    var intArray = new int[3];
                    int validInt;

                    for (var i = 0; i < 3; i++)
                    {
                        if (stringArray.Length >= i) break;

                        if (int.TryParse(stringArray[i], out validInt))
                        {
                            intArray[i] = validInt;
                            continue;
                        }

                        if (stringArray[i].Contains("."))
                        {
                            var subArray = stringArray[i].Split('.');
                            if (int.TryParse(subArray[0], out validInt)) intArray[i] = validInt;
                            if (int.TryParse(subArray[1], out validInt)) intArray[i] = validInt;
                            break;
                        }
                    }
                        
                    
                    if (stringArray[0].Length < 2) stringArray[0] = "0" + stringArray[0];
                    if (stringArray[1].Length < 2) stringArray[1] = "0" + stringArray[1];

                    stringValue = stringArray[0] + splitChar + stringArray[1];
                }
                else
                {
                    var i = 2;
                    if (stringValue.Length == 1) stringValue = "0" + stringValue + "00";
                    if (1 < stringValue.Length && stringValue.Length <= 3) i = 1;

                    stringValue = stringValue.Insert(i, ":");
                }
            }

            if (!TimeSpan.TryParse(stringValue, out result)) return "00:00:00.000"; //throw new Exception("Can't parse TimeSpan from string");

            return result;
        }

        private static string GetValidString(string value)
        {
            return Regex.Match(value, @"(([0-1][0-9])|([2][0-3]))[:]?([0-5][0-9]?\d)?[:]?([0-5][0-9]?\d)?[.]?(\d{0,3})").ToString();
        }
    }
}
