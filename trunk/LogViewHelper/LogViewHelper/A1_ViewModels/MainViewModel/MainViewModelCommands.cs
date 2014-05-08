using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using LogViewHelper.A0_Models;
using LogViewHelper.C0_Helpers;
using UseAbilities.Extensions.EnumerableExt;
using UseAbilities.Extensions.ObjectExt;
using UseAbilities.MVVM.Command;

namespace LogViewHelper.A1_ViewModels.MainViewModel
{
    public partial class MainViewModel
    {
        #region LoadNewCommand
        private ICommand _loadNewCommand;
        public ICommand LoadNewCommand
        {
            get
            {
                return _loadNewCommand ?? (_loadNewCommand = new RelayCommand<string>(OnLoadNewCommand, null));
            }
        }

        private void OnLoadNewCommand(string filename)
        {
            LoadLog(filename);
        }
        #endregion

        #region AddNewCommand
        private ICommand _addNewCommand;
        public ICommand AddNewCommand
        {
            get
            {
                return _addNewCommand ?? (_addNewCommand = new RelayCommand<string>(OnAddNewCommand, null));
            }
        }

        private void OnAddNewCommand(string filename)
        {
            //FileName = filename;
        }
        #endregion

        #region ClearFilterCommand
        private ICommand _clearFilterCommand;
        public ICommand ClearFilterCommand
        {
            get
            {
                return _clearFilterCommand ?? (_clearFilterCommand = new RelayCommand(m => OnClearFilterCommand(), p => IsCanClearFilterCommand()));
            }
        }

        private void OnClearFilterCommand()
        {
            Id = new List<string>();
            GUID = new List<string>();
            Pattern = string.Empty;
            TimeFilterSwitch = false;

            if (LogCollectionView.IsNull() || LogCollectionView.SourceCollection.IsNullOrEmpty()) return;
            LogCollectionView.Filter = null;
            LogCollectionView.Refresh();
        }

        private bool IsCanClearFilterCommand()
        {
            IsClearFilterEnabled = !Id.IsNullOrEmpty() || !GUID.IsNullOrEmpty() || !Pattern.IsNullOrEmpty() || TimeFilterSwitch;
            return IsClearFilterEnabled;
        }
        #endregion


        #region SearchCommand
        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new RelayCommand(m => OnSearchCommand(), p => IsCanSearchCommand()));
            }
        }

        private void OnSearchCommand()
        {
            if(LogCollectionView.IsNullOrEmpty()) return;

            var lowerEdge = new DateTime();
            var upperEdge = new DateTime();

            if (TimeFilterSwitch)
            {
                lowerEdge = SelectedDate.AddHours(-1 * Overtime.Hour).AddMinutes(-1 * Overtime.Minute).AddSeconds(-1 * Overtime.Second);
                upperEdge = SelectedDate.AddHours(Overtime.Hour).AddMinutes(Overtime.Minute).AddSeconds(Overtime.Second);
            }

            LogCollectionView.Filter = delegate(object item)
            {
                var logItem = item as LogItem;
                if (logItem == null) return false;

                if (!Id.IsNullOrEmpty() && !Id.Contains(logItem.Id)) return false;
                if (!GUID.IsNullOrEmpty() && !GUID.Contains(logItem.GUID)) return false;
                if (TimeFilterSwitch && (logItem.DateTime < lowerEdge || logItem.DateTime > upperEdge)) return false;
                if (!Pattern.IsNullOrEmpty() && !logItem.Message.Contains(Pattern)) return false;

                return true;
            };

            LogCollectionView.Refresh();
        }

        private bool IsCanSearchCommand()
        {
            IsSearchEnabled = !(LogCollectionView.IsNull() || LogCollectionView.SourceCollection.IsNullOrEmpty());
            return IsSearchEnabled;
        }
        #endregion
    }
}
