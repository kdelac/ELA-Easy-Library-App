using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using EasyLibraryApplication.WPF.View;
using EasyLibraryApplication.WPF.ViewModel;

namespace EasyLibraryApplication.WPF.Commands
{
    class LoginCommand : ICommand
    {
        private LoginViewModel viewModel;

        public LoginCommand(LoginViewModel vM)
        {
            viewModel = vM;

        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.CheckUser();
        }

        public event EventHandler CanExecuteChanged;
    }
}
