using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EasyLibraryApplication.WPF.ViewModel;

namespace EasyLibraryApplication.WPF.Commands
{
    class DeleteBookCommand : ICommand
    {
        private CRUDBooksViewModel viewModel;
        public DeleteBookCommand(CRUDBooksViewModel vm)
        {
            this.viewModel = vm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
           viewModel.DeleteSelectedBook();
        }

        public event EventHandler CanExecuteChanged;
    }
}
