using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using EasyLibraryApplication.WPF.Annotations;
using EasyLibraryApplication.WPF.Commands;
using EasyLibraryApplication.WPF.Model;

namespace EasyLibraryApplication.WPF.ViewModel
{
    class UserBooksViewModel : INotifyPropertyChanged
    {
        public static User User { get; set; }
        public CollectionViewSource BooksCollectionViewSource { get; set; }

        #region PrivateFields

        private LibraryEntities ctx;

        #endregion


        public UserBooksViewModel()
        {
            BooksCollectionViewSource = new CollectionViewSource();
            SearchEvent = new SearchCommand(this);
            LoadData();
        }

        public SearchCommand SearchEvent { get; set; }

        #region Selected Item

        private Book selectedItem;

        public Book SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        #endregion


        public void Refresh()
        {
            ctx = new LibraryEntities();
            ctx.Books.Load();
            ctx.Authors.Load();
            BooksCollectionViewSource.Source = ctx.Books.Local;
        }

        private void LoadData()
        {
            Refresh();
            SelectedItem = BooksCollectionViewSource.View.CurrentItem as Book;
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name= value;
                    OnPropertyChanged(nameof(Name));

                }
            }
        }

        public void Search()
        {
            BooksCollectionViewSource.Source = ctx.Books.Local.Select(s => s.Title.Equals("Vlak u snijegu"));
            SelectedItem = BooksCollectionViewSource.View.CurrentItem as Book;
        }

        public void CooseBook()
        {

        }

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
