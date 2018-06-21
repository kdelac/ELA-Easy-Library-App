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
    class ReservationViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public CollectionViewSource LibraryCollectionViewSource { get; private set; }
        private LibraryEntities ctx;

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

        #region SelectedLibray

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
        public ReservationViewModel(/*Book book*/)
        {
            LibraryCollectionViewSource = new CollectionViewSource();
            LoadData();
     
        }

        /// <summary>
        /// Ucitavanje podataka 
        /// </summary>
        private void LoadData()
        {
            Refresh();
            SelectedBook = ctx.Books.FirstOrDefault(s => s.Id == 1);
            SelectedLibrary = LibraryCollectionViewSource.View.CurrentItem as Library;
        }

        public void Refresh()
        {
            
            ctx = new LibraryEntities();
            ctx.Libraries.Load();
            ctx.Books.Load();

            List<Library> librariesOfFreeBook = (from library in ctx.Libraries
                join book in ctx.Books on library.Id equals book.LibraryId into lybGroup
                from pr2 in lybGroup
                where pr2.ISBN == "1234"
                join bookCopy in ctx.Copies on pr2.Id equals bookCopy.BookId into bookGroup
                from pr3 in bookGroup
                where bookGroup.Any() && pr3.StatusId == 1
                select library).ToList();

            LibraryCollectionViewSource.Source = new ObservableCollection<Library>(from lib in librariesOfFreeBook
              where lib.LibraryMembers.Select(lb => lb.UserId == 11).Count() > 1 select lib); 
             
            LibraryCollectionViewSource.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));    
        }
    }
}
