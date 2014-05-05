using System.Reflection;
using UseAbilities.Extensions.StringExt;
using UseAbilities.MVVM.Base;

namespace LogViewHelper.A1_ViewModels.Base
{
    public class ApplicationViewModel : ViewModelBase
    {
        public string Title { get { return "Помошник логапредставления {0}".Paste(Assembly.GetExecutingAssembly().GetName().Version); } }
    }
}
