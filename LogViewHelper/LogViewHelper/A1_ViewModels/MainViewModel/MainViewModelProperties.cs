using System.Collections.Generic;
using System.ComponentModel;
using LogViewHelper.A0_Models;
using UseAbilities.IoC.Attributes;
using UseAbilities.IoC.Stores;

namespace LogViewHelper.A1_ViewModels.MainViewModel
{
    public partial class MainViewModel
    {
        [InjectedProperty]
        public IFileReadStore<IEnumerable<LogItem>> LogStore { get; set; }

        public virtual ICollectionView LogCollectionView { get; set;}
    }
}
