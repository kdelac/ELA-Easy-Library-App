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
    class CRUDBooksViewModel : INotifyPropertyChanged
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

        #region StaticProperties
        public static Administrator AdminUser;
        #endregion

        #region Collections
      
        public CollectionViewSource BookCollection { get;  set; }
        public CollectionViewSource LanguageCollection { get; set; }
        public CollectionViewSource PublisherCollection { get; set; }
        public CollectionViewSource CategoryCollection { get; set; }

        #endregion

        #region Commands

        public AddBookCommand AddBookEvent { get; set; }
        public SaveBookCommand SaveBookEvent { get; set; }
        public RefreshCommand RefreshEvent { get; set; }
        public DeleteBookCommand DeleteBookEvent { get; set; } 


        #endregion

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

        #region SelectedCategoryProperty

        private Category selectedCategory;

        public Category SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
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
        /// <summary>
        /// Konstruktor klase CRUDBooksViewModel
        /// </summary>
        public CRUDBooksViewModel()
        {
            SaveBookEvent = new SaveBookCommand(this);
            AddBookEvent = new AddBookCommand(this);
            RefreshEvent = new RefreshCommand(this);
            DeleteBookEvent = new DeleteBookCommand(this);
            BookCollection = new CollectionViewSource();
            LanguageCollection = new CollectionViewSource();
            PublisherCollection = new CollectionViewSource();
            CategoryCollection = new CollectionViewSource();
            LoadData();
            ButtonAddContent = "Dodaj";
        }

        #endregion

        #region LoadData

        /// <summary>
        /// Učitavanje podataka 
        /// </summary>
        private void LoadData()
        {
            Refresh();
            SelectedBook = BookCollection.View.CurrentItem as Book;
            SelectedLanguage = LanguageCollection.View.CurrentItem as Language;
            SelectedPublisher = PublisherCollection.View.CurrentItem as Publisher;
            SelectedCategory = CategoryCollection.View.CurrentItem as Category;

        }

        /// <summary>
        /// Dohvaćanje podataka iz baze podataka
        /// </summary>
        public void Refresh()
        {
            int libraryId = AdminUser.LibraryId;
            ctx = new LibraryEntities();
            ctx.Books.Load();
            ctx.Publishers.Load();
            ctx.Languages.Load();
            ctx.Categories.Load();

       
            BookCollection.Source = new ObservableCollection<Book>(ctx.Books
               .Where(book => book.LibraryId == libraryId));
            LanguageCollection.Source = ctx.Languages.Local;
            PublisherCollection.Source = ctx.Publishers.Local;
            CategoryCollection.Source = new ObservableCollection<Category>(ctx.Categories.Where(ctg => ctg.Section.LibraryId == libraryId));
        }

        #endregion

        #region DeleteMethod

        /// <summary>
        /// Metoda koja služi za brisanje odabrane knjige iz baze podataka
        /// </summary>
        public void DeleteSelectedBook()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"Želite obrisati knjigu {SelectedBook.Title}?", "Pozor", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                ctx.Books.Remove(BookCollection.View.CurrentItem as Book);
                ctx.SaveChanges();
                Refresh();
            }
            
        }
        #endregion

        #region SaveChangesMethon 
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
        /// Metoda koja služi za dodavanje nove knjige u bazu podataka
        /// </summary>
        private void Add()
        {
            SelectedBook.CategoryId = SelectedCategory.Id;
            SelectedBook.LanguageId = SelectedLanguage.Id;
            SelectedBook.PublisherId = SelectedPublisher.Id;
            SelectedBook.LibraryId = 2;
            try
            {
                
                if (int.Parse(SelectedBook.Pages.ToString()) < 0)
                {
                    MessageBox.Show("Broj stranica mora biti veći od 0.");
                }
                else
                {
                    ctx.Books.Add(SelectedBook);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Broj stranica mora biti cijeli broj.");
            }
        }

        #endregion
    }
}
