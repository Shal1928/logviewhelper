using System.Collections.Generic;
using System.Windows;
using LogViewHelper.A0_Models;
using LogViewHelper.A1_ViewModels.MainViewModel;
using LogViewHelper.A2_View;
using LogViewHelper.D0_Stores;
using UseAbilities.IoC.Core;
using UseAbilities.IoC.Helpers;
using UseAbilities.IoC.Stores;
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
            Loader(IoCManager.Container);
            AdvancedViewManager.Instance.RegisterRelation<MainViewModel, MainView>();

            AdvancedViewManager.Instance.ResolveAndShow<MainViewModel>();
        }

        private static void Loader(IoC ioc)
        {
            ioc.RegisterSingleton<IFileReadStore<IEnumerable<LogItem>>, LogStore>();
        }
    }
}
