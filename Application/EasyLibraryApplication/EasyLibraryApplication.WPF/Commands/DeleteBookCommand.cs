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

        /// <summary>
        /// Konstruktor klase
        /// </summary>
        /// <param name="vm"></param>
        public DeleteBookCommand(CRUDBooksViewModel vm)
        {
            this.viewModel = vm;
        }

        /// <summary>
        /// Metoda koja određuje može li se naredba brisanja knjige izvršiti
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }
        /// <summary>
        /// Metoda kojom se izvršava naredba brisanja knjige
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
           viewModel.DeleteSelectedBook();
        }

        public event EventHandler CanExecuteChanged;
    }
}
