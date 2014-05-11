using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;
using UseAbilities.Extensions.ObjectExt;

namespace LogViewHelper.B1_Behaviors
{
    public class TextHiglightPresenterBehavior : Behavior<ContentPresenter>
    {
        //MarkerStyle
        protected override void OnAttached()
        {
            //var textBox = AssociatedObject.Content as TextBox;
            //if(textBox.IsNull()) return;

            AssociatedObject.Initialized += delegate(object sender, EventArgs e)
            {
                var a = sender;
                var b = e;
                var c = true;
            };
        }
    }
}
