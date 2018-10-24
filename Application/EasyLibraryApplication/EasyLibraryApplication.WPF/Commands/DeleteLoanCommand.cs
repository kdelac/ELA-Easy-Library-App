using EasyLibraryApplication.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasyLibraryApplication.WPF.Commands
{
    public class DeleteLoanCommand : ICommand
    {

        private ConfirmationOfReturningBookViewModel viewModel;

        public DeleteLoanCommand(ConfirmationOfReturningBookViewModel vModel)
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
            viewModel.DeleteLoan();
        }

        public event EventHandler CanExecuteChanged;
    }
}
