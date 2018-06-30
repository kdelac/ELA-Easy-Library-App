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
        public AddBookCommand(CRUDBooksViewModel viewModel)
        {
            this.booksViewModel = viewModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

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
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
