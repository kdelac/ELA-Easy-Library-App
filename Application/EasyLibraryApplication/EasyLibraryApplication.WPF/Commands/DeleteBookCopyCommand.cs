using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EasyLibraryApplication.WPF.ViewModel;

namespace EasyLibraryApplication.WPF.Commands
{
    class DeleteBookCopyCommand : ICommand
    {
        private CRUDBookCopiesViewModel viewModel;

        /// <summary>
        /// Konstruktor klase
        /// </summary>
        /// <param name="vm"></param>
        public DeleteBookCopyCommand(CRUDBookCopiesViewModel vm)
        {
            viewModel = vm;
        }

        /// <summary>
        /// Metoda koja određuje može li se naredba brisanja kopije knjige izvršiti
        /// </summary>
        /// <param name="parameter"></param>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Metoda kojom se izvršava naredba brisanja kopije knjige
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            viewModel.DeleteSelectedBookCopy();
        }

        public event EventHandler CanExecuteChanged;
    }
}
