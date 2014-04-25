using System.Windows;
using LogViewHelper.A1_ViewModels;
using LogViewHelper.A2_View;
using UseAbilities.MVVM.Managers;

namespace LogViewHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            AdvancedViewManager.Instance.RegisterRelation<MainViewModel, MainView>();
            ViewModelManager.ActiveViewModels.CollectionChanged += AdvancedViewManager.Instance.OnViewModelsCoolectionChanged;

            AdvancedViewManager.Instance.ResolveAndShow<MainViewModel>();
        }
    }
}
