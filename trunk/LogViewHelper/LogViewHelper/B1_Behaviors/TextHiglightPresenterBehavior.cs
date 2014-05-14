using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Interactivity;
using System.Windows.Media;
using UseAbilities.Extensions.ObjectExt;
using Color = System.Windows.Media.Color;
//using ExtRichTextBox = Xceed.Wpf.Toolkit.RichTextBox;

namespace LogViewHelper.B1_Behaviors
{
    public class TextHiglightPresenterBehavior : Behavior<ContentPresenter>
    {
        //MarkerStyle
        protected override void OnAttached()
        {
            AssociatedObject.Initialized += delegate(object sender, EventArgs e)
            {
                var presenter = sender as ContentPresenter;
                if (presenter.IsNull()) return;

                var textBlock = presenter.Content as TextBlock;
                if (textBlock.IsNull()) return;

                //foreach (var marker in Markers)
                //{
                //    if (textBlock.Text.Contains(marker))
                //        textBlock.Inlines.Add(new Bold{Foreground = new SolidColorBrush(Colors.OrangeRed)});
                //    else
                //    {
                //        textBlock.Inlines.Add(textBlock.Text);
                //    }
                //}

                
            };
        }

        public static List<string> Markers = new List<string>
        {
            "SELECT",
            "Задача"
        }; 

    //    public static void ChangeTextcolor(string textToMark, Color color, TextBox richTextBox, int startIndex)
    //    {
    //        if (startIndex < 0 || startIndex > textToMark.Length - 1) startIndex = 0;

    //        var newFont = new Font("Verdana", 10f, FontStyle.Bold, GraphicsUnit.Point, 178, false);
    //        try
    //        {
    //            var texts = richTextBox.Text.Split('\n');
    //            foreach (string line in richTextBox.Lines)
    //            {
    //                if (line.Contains(textToMark))
    //                {
    //                    richTextBox.Select(startIndex, line.Length);
    //                    richTextBox.SelectionBackColor = color;
    //                }
    //                startIndex += line.Length + 1;
    //            }
    //        }
    //        catch
    //        { }
    //    }
    }
}
