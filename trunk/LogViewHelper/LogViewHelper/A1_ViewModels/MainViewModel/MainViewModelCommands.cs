using System.Windows.Input;
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
    }
}
