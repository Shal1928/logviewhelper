using System.Windows.Input;
using LogViewHelper.A0_Models;
using UseAbilities.Extensions.EnumerableExt;
using UseAbilities.Extensions.StringExt;
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

        #region SearchCommand
        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new RelayCommand(m => OnSearchCommand(), null));
            }
        }

        private void OnSearchCommand()
        {
            var lowerEdge = SelectedDate.AddHours(-1 * Overtime.Hour).AddMinutes(-1 * Overtime.Minute).AddSeconds(-1*Overtime.Second);
            var upperEdge = SelectedDate.AddHours(Overtime.Hour).AddMinutes(Overtime.Minute).AddSeconds(Overtime.Second);

            LogCollectionView.Filter = delegate(object item)
            {
                var logItem = item as LogItem;
                if (logItem == null) return false;

                if (!Id.IsNullOrEmptyOrSpaces() && logItem.Id != Id) return false;
                if (logItem.DateTime < lowerEdge || logItem.DateTime > upperEdge) return false;
                if (!Pattern.IsNullOrEmpty() && !logItem.Message.Contains(Pattern)) return false;

                return true;
            };

            LogCollectionView.Refresh();
        }
        #endregion
    }
}
