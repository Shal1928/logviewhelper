using System;
using System.Collections.Generic;
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
        
        public virtual string Id { get; set; }
        public virtual string GUID { get; set; }
        public virtual TimeSpan Time { get; set; }
        public virtual TimeSpan Overtime { get; set; }
        public IEnumerable<EnumViewWrapper<TimeType>> TimeTypes{ get {return EnumViewWrapper<TimeType>.GetWrappedCollection();} }
        public TimeType SelectedTimeType { get; set; } 
        public virtual string Pattern { get; set; }
    }
}
