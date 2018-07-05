using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using EasyLibraryApplication.WPF.Annotations;
using EasyLibraryApplication.WPF.Model;

namespace EasyLibraryApplication.WPF.ViewModel
{
    class CRUDBookCopiesViewModel : INotifyPropertyChanged
    {
        private LibraryEntities ctx;

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Constructor

        public CRUDBookCopiesViewModel()
        {
            StatusesCollection = new CollectionViewSource();
            CopiesCollection = new CollectionViewSource();
            BookCollection = new CollectionViewSource();
            LoadData();
        }

        #endregion

        #region StaticFields

        public static Administrator AdminUser;

        #endregion

        #region Collections

        public CollectionViewSource StatusesCollection { get; set; }
        public CollectionViewSource CopiesCollection { get; set; }
        public CollectionViewSource BookCollection { get; set; }

        #endregion

        #region SelectedProperties

        #region SelectedBook

        private Book selectedBook;

        public Book SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged(nameof(SelectedBook));
            }
        }

        #endregion

        #region SelectedCopy

        private Copy selectedbookCopy;

        public Copy SelectedBookCopy
        {
            get { return selectedbookCopy; }
            set
            {
                selectedbookCopy = value; 
                OnPropertyChanged(nameof(SelectedBookCopy));
            }
        }

        #endregion

        #region SelectedStatus

        private Status selectedStatus;

        public Status SelectedStatus
        {
            get { return selectedStatus; }
            set
            {
                selectedStatus = value; 
                OnPropertyChanged(nameof(SelectedStatus));
            }
        }


        #endregion
        #endregion


        #region LoadData

        /// <summary>
        /// Učitavanje podataka 
        /// </summary>
        private void LoadData()
        {
            Refresh();
            SelectedBook = BookCollection.View.CurrentItem as Book;
            SelectedStatus = StatusesCollection.View.CurrentItem as Status;
           
            SelectedBookCopy = CopiesCollection.View.CurrentItem as Copy;
        }

        /// <summary>
        /// Dohvaćanje podataka iz baze podataka
        /// </summary>
        public void Refresh()
        {
            int libraryId = AdminUser.LibraryId;
            ctx = new LibraryEntities();
            ctx.Books.Load();
            ctx.Statuses.Load();
            ctx.Copies.Load();
            
            BookCollection.Source = new ObservableCollection<Book>(ctx.Books
                .Where(book => book.LibraryId == libraryId));
            SelectedBook = BookCollection.View.CurrentItem as Book;
            StatusesCollection.Source = ctx.Statuses.Local;
            CopiesCollection.Source = new ObservableCollection<Copy>();
            
        }
         

        #endregion
    }
}
