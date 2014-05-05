using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using LogViewHelper.A0_Models;
using LogViewHelper.A1_ViewModels.Base;
using UseAbilities.MVVM.Base;
using UseAbilities.MVVM.Command;

namespace LogViewHelper.A1_ViewModels.MainViewModel
{
    public partial class MainViewModel : ApplicationViewModel
    {
        public MainViewModel()
        {
            //var testDataLogCollection = new List<LogItem>
            //{
            //    new LogItem
            //    {
            //        DateTime = new DateTime(2014, 5, 1, 12, 0, 0, 1),
            //        Level = LogItemType.Debug,
            //        Id = 43,
            //        Message = "M1",
            //        MSeconds = 2
            //    },
            //    new LogItem
            //    {
            //        DateTime = new DateTime(2014, 5, 2, 13, 30, 11, 2),
            //        Level = LogItemType.Info,
            //        Id = 43,
            //        Message = "M2",
            //        MSeconds = 3
            //    },
            //    new LogItem
            //    {
            //        DateTime = new DateTime(2014, 5, 1, 12, 0, 0, 3),
            //        Level = LogItemType.All,
            //        Id = 42,
            //        Message = "M3",
            //        MSeconds = 4
            //    }
            //};

            //UpdateLogCollectionView(testDataLogCollection);
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
