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
    class AddBookCommand : ICommand
    {
        private CRUDBooksViewModel booksViewModel;

        /// <summary>
        /// Konstruktor klase
        /// </summary>
        /// <param name="viewModel"></param>
        public AddBookCommand(CRUDBooksViewModel viewModel)
        {
            this.booksViewModel = viewModel;
        }

        /// <summary>
        /// Metoda koja određuje može li se naredba dodavanja knjige izvršiti
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Metoda koja služi za dodavanje knjige
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            if (booksViewModel.ButtonAddContent == "Dodaj")
            {
                Book addBook = new Book();
                booksViewModel.SelectedBook = addBook;
                booksViewModel.ButtonAddContent = "Otkaži";
            }
            else
            {
                booksViewModel.SelectedBook = booksViewModel.BookCollection.View.CurrentItem as Book;
                booksViewModel.ButtonAddContent = "Dodaj";
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
