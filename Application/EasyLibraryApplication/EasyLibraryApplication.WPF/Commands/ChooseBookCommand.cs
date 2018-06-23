using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EasyLibraryApplication.WPF.ViewModel;

namespace EasyLibraryApplication.WPF.Commands
{
    class ChooseBookCommand : ICommand
    {
        private UserBooksViewModel viewModel;

        public ChooseBookCommand(UserBooksViewModel vModel)
        {
            viewModel = vModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.CooseBook();
        }

        public event EventHandler CanExecuteChanged;
    }
}
