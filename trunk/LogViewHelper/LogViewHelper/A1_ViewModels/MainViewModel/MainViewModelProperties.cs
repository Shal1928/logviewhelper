using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using LogViewHelper.A0_Models;
using UseAbilities.Extensions.Helpers;
using UseAbilities.IoC.Attributes;
using UseAbilities.IoC.Stores;

namespace LogViewHelper.A1_ViewModels.MainViewModel
{
    public partial class MainViewModel
    {
        [InjectedProperty]
        public IFileReadStore<IEnumerable<LogItem>> LogStore { get; set; }
        public virtual ICollectionView LogCollectionView { get; set;}

        public virtual List<string> Id { get; set; }
        public virtual List<string> GUID { get; set; }
        public virtual DateTime SelectedDate { get; set; }
        public virtual DateTime Overtime { get; set; }
        //public IEnumerable<EnumViewWrapper<TimeType>> TimeTypes{ get {return EnumViewWrapper<TimeType>.GetWrappedCollection();} }
        public virtual TimeType SelectedTimeType { get; set; }
        public virtual string Pattern { get; set; }
    }
}
