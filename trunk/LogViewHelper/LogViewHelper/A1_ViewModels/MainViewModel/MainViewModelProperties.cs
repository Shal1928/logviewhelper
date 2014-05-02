using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogViewHelper.A1_ViewModels.MainViewModel
{
    public partial class MainViewModel
    {
        public virtual ICollectionView LogCollectionView { get; set;}
        public virtual string FileName {get; set;}
    }
}
