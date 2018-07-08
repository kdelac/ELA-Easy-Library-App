using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EasyLibraryApplication.WPF.ViewModel;

namespace EasyLibraryApplication.WPF.Commands
{
    class RefreshCopyCommand : ICommand
    {
        private CRUDBookCopiesViewModel viewModel;
        /// <summary>
        /// Konstruktor klase
        /// </summary>
        /// <param name="vm"></param>
        public RefreshCopyCommand(CRUDBookCopiesViewModel vm)
        {
            viewModel = vm;
        }

        /// <summary>
        /// Metoda koja određuje može li se osvježavanje podataka iz baze podataka izvšiti
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

       
        /// <summary>
        /// Metoda kojom je izvršava osvježavanje podataka
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            viewModel.Refresh();
        }

        public event EventHandler CanExecuteChanged;
    }
}
