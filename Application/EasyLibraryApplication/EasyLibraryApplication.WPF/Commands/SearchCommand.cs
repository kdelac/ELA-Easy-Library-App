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
    class SearchCommand : ICommand
    {
        private UserBooksViewModel viewModel;

        public SearchCommand(UserBooksViewModel vModel)
        {
            viewModel = vModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.Search();
        }

        public event EventHandler CanExecuteChanged;
    }
}
