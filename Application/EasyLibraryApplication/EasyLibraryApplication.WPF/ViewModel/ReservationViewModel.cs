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
        

        public CollectionViewSource LibraryCollectionViewSource { get; private set; }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region StaticProperties
        public static User User { get; set; }
        public static Book Book { get; set; }
        #endregion

        #region PrivateFields
        private LibraryEntities ctx;
        private Email email;
        private QrCode qrCode;
        private Reservation reservation;
        #endregion

        #region Commands

        public ReservationCommand ReservationEvent{ get; set; }

        #endregion

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
        #endregion

        #region Constructor
        /// <summary>
        /// KOnstruktor view model-a
        /// </summary>
        public ReservationViewModel()
        {
            LibraryCollectionViewSource = new CollectionViewSource();
            LoadData();
            ReservationEvent = new ReservationCommand(this);
        }
        #endregion

        #region LoadData       
        /// <summary>
        /// Ucitavanje podataka 
        /// </summary>
        private void LoadData()
        {
            Refresh();
            SelectedBook = Book;
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
            LibraryCollectionViewSource.Source = new ObservableCollection<Library>(ctx.GetAllLibrariesWhereIsBookFreeForUser(Book.ISBN,User.Id).ToList());             
            LibraryCollectionViewSource.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));    
        }
        #endregion

        #region ReservationQRAndMail
        /// <summary>
        /// Metoda kojom se kreira objekt rezervacije te popunjava podacima 
        /// (odabrana knjiga, odabrana knjižnica, korisnik, datum rezervacije...)
        /// </summary>
        /// <returns>
        /// Vraća true ukolike je uspješno stvaranje objekta rezervacije te false ako je neuspješno
        /// </returns>
        public bool CreateReservation()
        {
            //Točno određena knjiga u odabranoj knjižnici
            int selectedBookId = ctx.Books
                .Where(book => book.LibraryId == selectedLibrary.Id && book.ISBN == Book.ISBN)
                .Select(book => book.Id).FirstOrDefault();

            bool bookAlredyReserved =
                ctx.Reservations.FirstOrDefault(r => r.UserId == User.Id && r.BookId == selectedBookId) != null;

            if (bookAlredyReserved)
            {
                MessageBox.Show($"Već ste rezervirali knjigu {Book.Title} u odabranoj knjižnici.");
                reservation = null;
                return false;
            }
            else
            {
                reservation = new Reservation();
                reservation.UserId = User.Id;
                reservation.BookId = selectedBookId;
                reservation.ReservationDate = DateTime.Now;
                reservation.EndReservationDate = DateTime.Now.Add(new TimeSpan(3,0,0,0));
                return true;
            }

        }
        /// <summary>
        /// Metoda koja stvara QR kod
        /// </summary>
        /// <returns>
        /// Varaća sliku QR koda tipa Bitmap
        /// </returns>
        public Bitmap CreateQrPicture()
        {
            try
            {
                string json = JsonConvert.SerializeObject(reservation);
                qrCode = new QrCode(json);
                return qrCode.GetQrCodeBitmap();
            }
            catch (Exception e)
            {
                MessageBox.Show("Nesupješno stvaranje QR koda");
                throw;
            }           
            
        }

        /// <summary>
        /// Metoda kojom se šalju podaci rezervacije i QR kod prijavljenom korisniku
        /// </summary>
        public async void SendEmail()
        {     
            string emailText = $"Poštovani {User.Name} {User.Surname},\n " +
                               $"rezervirali ste knjigu {Book.Title}.\n" +
                               $"Datum rezervacije: {reservation.ReservationDate.ToShortDateString()}. \n" +
                               $"Datum isteka rezervacije: {reservation.EndReservationDate.ToShortDateString()}. \n" +
                               "Lijep podrav, \n" +
                               $"{SelectedLibrary.Name}";               
            email = new Email(SelectedLibrary.Email, SelectedLibrary.PasswordHash, User.Email);
            try
            {
                await email.SendEmail(CreateQrPicture(), emailText);
            }
            catch (Exception e)
            {
                MessageBox.Show("Neuspješno slanje e-maila s QR kodom rezervacije.");
            }
                 
        }

        /// <summary>
        /// Metoda kojom se podaci rezervacije spremaju u podaci podataka
        /// </summary>
        public async void SaveReservationToDatabase()
        {
            try
            {
                ctx.Reservations.Add(reservation);
                await ctx.SaveChangesAsync();
            }
            catch (Exception e)
            {
                MessageBox.Show("Neuspjela rezervacija, pokušajte ponovno!!!");
            }
           
        }
        #endregion
    }
}
