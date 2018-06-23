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
using QrCodeGenerator;
using EmailGenerator;
using System.Drawing;
using System.Threading;
using System.Windows;
using EasyLibraryApplication.WPF.Commands;
using Newtonsoft.Json;

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

        
        public static User User { get; set; }

        private Email email;
        private QrCode qrCode;
        private Reservation reservation;

        #region Commands

        public ReservationCommand ReservationEvent{ get; set; }

        #endregion


        #region selectedBook

        public static Book Book { get; set; }
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


        #region SelectedLibrary

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

        /// <summary>
        /// KOnstruktor view model-a
        /// </summary>
        #endregion
        public ReservationViewModel()
        {
            LibraryCollectionViewSource = new CollectionViewSource();
            LoadData();
            ReservationEvent = new ReservationCommand(this);
     
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

        /// <summary>
        /// Dohvaćanje podataka iz baze podataka
        /// </summary>
        public void Refresh()
        {          
            ctx = new LibraryEntities();
            ctx.Libraries.Load();
            ctx.Books.Load();
            LibraryCollectionViewSource.Source = new ObservableCollection<Library>(ctx.GetAllLibrariesWhereIsBookFreeForUser("1234",20).ToList()); 
             
            LibraryCollectionViewSource.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));    
        }

        public void CreateReservation()
        {
            reservation = new Reservation();
            reservation.UserId = 20;
            reservation.BookId = 1;
            reservation.ReservationDate = DateTime.Now;
            reservation.EndReservationDate = DateTime.Now.Add(new TimeSpan(3, 0, 0, 0));
        
        }
        public Bitmap CreateQrPicture()
        {
            
            string json = JsonConvert.SerializeObject(reservation);
            qrCode = new QrCode(json);
            return qrCode.GetQrCodeBitmap();
        }
        public async void SendEmail()
        {
            CreateReservation();
            email = new Email("kristijan.mihaljinac@gmail.com", "kristijan.mihaljinac@gmail.com");
            await email.SendEmail(CreateQrPicture(), "ovo je jebenica");
            
        }

        public async void SaveReservationToDatabase()
        {
            CreateReservation();
            /*ctx.Books
                .Where(book => book.LibraryId == selectedLibrary.Id && book.ISBN == SelectedBook.ISBN)
                .Select(book => book.Id).FirstOrDefault();*/
            ctx.Reservations.Add(reservation);
            await ctx.SaveChangesAsync();
        }
    }
}
