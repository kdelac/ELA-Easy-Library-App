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
    class CRUDBooksViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region StaticProperties
        public static User AdminUser;
        #endregion

        #region Collections
      
        public CollectionViewSource BookCollection { get;  set; }
        public CollectionViewSource LanguageCollection { get; set; }
        public CollectionViewSource PublisherCollection { get; set; }

        #endregion



        private LibraryEntities ctx;


        #region SelectedProperties
        #region SelectedBookProperty

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

        #region SelectedLibraryProperty

        private Library selectedLibrary;

        public Library SelectedLibrary
        {
            get { return selectedLibrary; }
            set
            {
                selectedLibrary = value;
                OnPropertyChanged(nameof(SelectedLibrary));
            }
        }

        #endregion

        #region SelectedLanguageProperty
        private Language selectedLanguage;

        public Language SelectedLanguage
        {
            get { return selectedLanguage; }
            set
            {
                selectedLanguage = value;
                OnPropertyChanged(nameof(SelectedLanguage));
            }
        }


        #endregion

        #region SelectedPublisherProperty

        private Publisher selectedPublisher;

        public Publisher SelectedPublisher
        {
            get { return selectedPublisher; }
            set
            {
                selectedPublisher = value;
                OnPropertyChanged(nameof(SelectedPublisher));
            }
        }

        #endregion


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

        #region Constructor

        public CRUDBooksViewModel()
        {
            BookCollection = new CollectionViewSource();
            LibraryCollection = new CollectionViewSource();
            LoadData();
            ButtonAddContent = "Dodaj";

        }

        #endregion


        #region LoadData

        private void LoadData()
        {
            Refresh();
            SelectedBook = BookCollection.View.CurrentItem as Book;
            SelectedLanguage = LanguageCollection.View.CurrentItem as Language;
            SelectedPublisher = PublisherCollection.View.CurrentItem as Publisher;         

        }

      

        private void Refresh()
        {
            ctx = new LibraryEntities();
            ctx.Books.Load();
            ctx.Publishers.Load();
            ctx.Languages.Load();
           
            BookCollection.Source = new ObservableCollection<Book>(ctx.Books
                .Where(book => book.LibraryId == AdminUser.Administrators.Where(ad => ad.EndOfWork == null)
                                   .Select(ad => ad.LibraryId).First()));
            LanguageCollection.Source = ctx.Languages.Local;
            PublisherCollection.Source = ctx.Publishers.Local;
        }

        #endregion


        public void SaveChanges()
        {
            if (ButtonAddContent == "Otkaži")
            {
                Add();
            }

            ctx.SaveChanges();
            ButtonAddContent = "Dodaj";
        }

        private void Add()
        {
            ctx.Books.Add(SelectedBook);
        }
    }
}
