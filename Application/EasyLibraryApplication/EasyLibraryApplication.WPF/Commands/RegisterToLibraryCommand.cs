using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EasyLibraryApplication.WPF.ViewModel;

namespace EasyLibraryApplication.WPF.Commands
{
    class RegisterToLibraryCommand : ICommand
    {
        private RegisterToLibraryViewModel viewModel;

        public RegisterToLibraryCommand(RegisterToLibraryViewModel vModel)
        {
            viewModel = vModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.RegisterToLibrary();
        }

        public event EventHandler CanExecuteChanged;
    }
}
