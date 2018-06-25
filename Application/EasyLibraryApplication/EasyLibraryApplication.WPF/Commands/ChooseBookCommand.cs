using System;
using System.Windows.Input;
using EasyLibraryApplication.WPF.ViewModel;

namespace EasyLibraryApplication.WPF.Commands
{
    class ChooseBookCommand : ICommand
    {
        private UserBooksViewModel viewModel;

        /// <summary>
        /// Konstruktor u kojemu se predaje viewModel kako bi se mogle izvršit metode iz zadanog viewModela
        /// </summary>
        /// <param name="vModel"></param>
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
