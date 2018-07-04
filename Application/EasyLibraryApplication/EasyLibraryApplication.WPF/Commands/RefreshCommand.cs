using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EasyLibraryApplication.WPF.View;
using EasyLibraryApplication.WPF.ViewModel;

namespace EasyLibraryApplication.WPF.Commands
{
    class RefreshCommand : ICommand
    {
        private CRUDBooksViewModel viewModel;

        public RefreshCommand(CRUDBooksViewModel vm)
        {
            this.viewModel = vm;
        }

        public void Execute(object parameter)
        {
            viewModel.Refresh();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
