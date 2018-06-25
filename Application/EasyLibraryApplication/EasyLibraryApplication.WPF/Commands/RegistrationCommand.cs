using System;
using System.Windows.Input;
using EasyLibraryApplication.WPF.ViewModel;

namespace EasyLibraryApplication.WPF.Commands
{
    class RegistrationCommand : ICommand
    {
        private RegistrationViewModel viewModel;

        /// <summary>
        /// Konstruktor u kojemu se predaje viewModel kako bi se mogle izvršit metode iz zadanog viewModela
        /// </summary>
        /// <param name="vModel"></param>
        public RegistrationCommand(RegistrationViewModel vModel)
        {
            viewModel = vModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Metoda koja se izvršava prilikom pritiska na gumb, te se izvršava metoda iz viewModela
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            viewModel.SaveChanges();
        }

        public event EventHandler CanExecuteChanged;
    }
}
