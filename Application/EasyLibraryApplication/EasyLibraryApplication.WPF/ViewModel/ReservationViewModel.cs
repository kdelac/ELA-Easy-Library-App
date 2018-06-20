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
            

            LibraryCollectionViewSource.Source = new ObservableCollection<Library>(ctx.Libraries.ToList());
            LibraryCollectionViewSource.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));    
        }
    }
}
