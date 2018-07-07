using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EasyLibraryApplication.WPF.Model;
using EasyLibraryApplication.WPF.ViewModel;

namespace EasyLibraryApplication.WPF.Commands
{
    class AddBookCopyCommand : ICommand
    {
        private CRUDBookCopiesViewModel viewModel;

        /// <summary>
        /// Konstruktor klase
        /// </summary>
        /// <param name="vm"></param>
        public AddBookCopyCommand(CRUDBookCopiesViewModel vm)
        {
            this.viewModel = vm;
        }

         
        /// <summary>
        /// Metoda koja određuje može li se naredba izvršiti ili ne
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Metoda u kojoj se nalazi logika  koju izršava naredba
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            if (viewModel.ButtonAddContent == "Dodaj")
            {
                Copy addCopy= new Copy();
                viewModel.SelectedBookCopy = addCopy;
                viewModel.ButtonAddContent = "Otkaži";
            }
            else
            {
                viewModel.SelectedBookCopy = viewModel.CopiesCollection.View.CurrentItem as Copy;
                viewModel.ButtonAddContent = "Dodaj";
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
