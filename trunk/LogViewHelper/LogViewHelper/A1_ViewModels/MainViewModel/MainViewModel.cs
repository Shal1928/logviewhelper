using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using LogViewHelper.A0_Models;
using LogViewHelper.A1_ViewModels.Base;

namespace LogViewHelper.A1_ViewModels.MainViewModel
{
    public partial class MainViewModel : ApplicationViewModel
    {
        public MainViewModel()
        {
            SelectedDate = DateTime.Now;
            Overtime = new DateTime(1, 1, 1,0,1,0);
            Id = new List<string>();
            GUID = new List<string>();
        }


        private void UpdateLogCollectionView(IEnumerable<LogItem> dataCollection)
        {
            var view = CollectionViewSource.GetDefaultView(dataCollection);
            if (view == null) return;

            view.Filter = FilterPredicate;
            LogCollectionView = view;
            LogCollectionView.Refresh();
        }

        private static bool FilterPredicate(object item)
        {
            var logItem = item as LogItem;
            if (logItem == null) return false;

            return true;
            //var date = new DateTime(SelectedYear, SelectedMonth, 1);
            //var weeks = date.GetWeeksAndDaysOfMonth();
            //_isImporting = false;
            //return weeks.Select(week => WrapCalendarDays(week, SelectedMonth)).Any(logItem.Equals);
        }

        private void LoadLog(string fileName)
        {
            UpdateLogCollectionView(LogStore.Load(fileName));
        }
    }
}
