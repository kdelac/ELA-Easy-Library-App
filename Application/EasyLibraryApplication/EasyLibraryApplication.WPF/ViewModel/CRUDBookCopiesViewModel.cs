using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using EasyLibraryApplication.WPF.Annotations;
using EasyLibraryApplication.WPF.Commands;
using EasyLibraryApplication.WPF.Model;

namespace EasyLibraryApplication.WPF.ViewModel
{
    class CRUDBookCopiesViewModel : INotifyPropertyChanged
    {
        private LibraryEntities ctx;

        #region Commands

        public AddBookCopyCommand AddBookCopyEvent { get; set; }

        public SaveBookCopyCommand SaveBookCopyEvent { get; set; }

        public DeleteBookCopyCommand DeleteBookCopyEvent { get; set; }

        public RefreshCopyCommand RefreshCopyEvent { get; set; }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Metoda koja implementira INotifyPropertyChanged interface
        /// </summary>
        /// <param name="propertyName"></param>

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Konstruktor klase
        /// </summary>
        public CRUDBookCopiesViewModel()
        {
            SaveBookCopyEvent = new SaveBookCopyCommand(this);
            AddBookCopyEvent = new AddBookCopyCommand(this);
            DeleteBookCopyEvent = new DeleteBookCopyCommand(this);
            RefreshCopyEvent = new RefreshCopyCommand(this);
            StatusesCollection = new CollectionViewSource();
            CopiesCollection = new CollectionViewSource();
            BookCollection = new CollectionViewSource();
            LoadData();
            this.PropertyChanged += OnPropertyChanged;
            ButtonAddContent = "Dodaj";
        }



        #endregion

        #region CopySourceChange

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == nameof(SelectedBook))
            {
                CopiesCollection.Source = new ObservableCollection<Copy>(ctx.Copies.Where(cp => cp.BookId == SelectedBook.Id));
            }
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
            CopiesCollection.Source = new ObservableCollection<Copy>(ctx.Copies.Where(cp => cp.BookId == SelectedBook.Id));
            
        }



        #endregion
    
        #region ButtonAddContent Full Property
        private string _buttonAddContent;
        public string ButtonAddContent
        {
            get
            {
                return _buttonAddContent;
            }

            set
            {
                _buttonAddContent = value;
                OnPropertyChanged(nameof(ButtonAddContent));
            }
        }
        #endregion

        #region DeleteMethod

        /// <summary>
        /// Metoda koja služi za brisanje odabrane knjige iz baze podataka
        /// </summary>
        public void DeleteSelectedBookCopy()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"Želite obrisati kopiju knjige {SelectedBook.Title}?", "Pozor", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                ctx.Copies.Remove(CopiesCollection.View.CurrentItem as Copy);
                ctx.SaveChanges();
                Refresh();
            }

        }
        #endregion

        #region SaveChangesMethod 
        /// <summary>
        /// Metoda koja služi za spremanje promjena u bazu podataka
        /// </summary>
        public void SaveChanges()
        {
            if (ButtonAddContent == "Otkaži")
            {
                Add();
            }

            ctx.SaveChanges();
            ButtonAddContent = "Dodaj";
        }
        #endregion

        #region AddMethod

        /// <summary>
        /// Metoda koja služi za dodavanje nove kopije knjige u bazu podataka
        /// </summary>
        private void Add()
        {
            SelectedBookCopy.BookId = SelectedBook.Id;
            SelectedBookCopy.StatusId = SelectedStatus.Id;
           
            ctx.Copies.Add(SelectedBookCopy);
            
        }

        #endregion
    }
}
