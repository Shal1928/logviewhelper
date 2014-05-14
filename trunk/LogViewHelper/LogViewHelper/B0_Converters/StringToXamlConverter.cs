using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using UseAbilities.Extensions.EnumerableExt;
using UseAbilities.Extensions.ObjectExt;
using UseAbilities.WPF.Converters.Base;

namespace LogViewHelper.B0_Converters
{
    public class StringToXamlConverter : ConverterBase<StringToXamlConverter>
    {
        public StringToXamlConverter()
        {
            //
        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var textBlock = value as TextBlock;
            if (textBlock == null) return value;

            var escapedXml = SecurityElement.Escape(textBlock.Text);
            if (escapedXml.IsNull()) return null;

            while (escapedXml.IndexOf("SELECT") != -1)
            {
                textBlock.Inlines.Add(new Run("RUN") { FontWeight = FontWeights.Bold, Background = Brushes.Yellow });
                break;
            }

            //if (escapedXml.Length > 0)
            //    textBlock.Inlines.Add(new Run(escapedXml));
            
            return textBlock;
        }
    }
}
