using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using EasyLibraryApplication.WPF.Annotations;
using EasyLibraryApplication.WPF.Commands;
using EasyLibraryApplication.WPF.Model;

namespace EasyLibraryApplication.WPF.ViewModel
{
    /// <summary>
    /// Klasa koja služi za spajanje pogleda pregleda knjiga i modela, te za izradu poslovne logike
    /// </summary>
    class UserBooksViewModel : INotifyPropertyChanged
    {
        public static User User { get; set; }
        public CollectionViewSource BooksCollectionViewSource { get; set; }
        

        #region PrivateFields

        private LibraryEntities ctx;

        #endregion

        #region Constructor

        /// <summary>
        /// Konstruktor koji stvara dva nova objekta jedan za listu knjiga, a drugi za događaj pritiska na gumb. Poziva se metoda LoadData().
        /// </summary>
        public UserBooksViewModel()
        {
            BooksCollectionViewSource = new CollectionViewSource();
            ChooseBookEvent = new ChooseBookCommand(this);
            SearchEvent = new SearchCommand(this);
            LoadData();
        }

        #endregion
        
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

        #region Command

        public SearchCommand SearchEvent { get; set; }
        public ChooseBookCommand ChooseBookEvent { get; set; }

        #endregion

        #region LoadData

        public void Refresh()
        {
            ctx = new LibraryEntities();
            ctx.Books.Load();
            ctx.Authors.Load();
            BooksCollectionViewSource.Source = ctx.FindBookForUser(User.Id);
        }

        private void LoadData()
        {
            Refresh();
            SelectedItem = BooksCollectionViewSource.View.CurrentItem as Book;
        }

        #endregion

        #region InputName

        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));

                }
            }
        }

        #endregion


        /// <summary>
        /// Metoda za pretragu knjige po naslovu knjige
        /// </summary>
        public void Search()
        {
            BooksCollectionViewSource.Source = ctx.FindBookForUserName(User.Id, Name);
        }

        /// <summary>
        /// Metoda koja se aktivira prilikom pritiska na gumb i otvara se prozor za rezervaciju
        /// </summary>
        public void CooseBook()
        {

        }

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Metoda zadužena za implementaciju INotifyPropertyChanged sučelja
        /// </summary>
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
