using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EasyLibraryApplication.WPF.ViewModel;

namespace EasyLibraryApplication.WPF.Commands
{
    class RegistrationCommand : ICommand
    {
        private RegistrationViewModel viewModel;

        public RegistrationCommand(RegistrationViewModel vModel)
        {
            viewModel = vModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.UserRegistration();
        }

        public event EventHandler CanExecuteChanged;
    }
}
