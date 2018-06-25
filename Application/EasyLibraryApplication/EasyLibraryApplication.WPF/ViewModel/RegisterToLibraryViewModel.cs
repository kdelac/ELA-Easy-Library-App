using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using EasyLibraryApplication.WPF.Annotations;
using EasyLibraryApplication.WPF.Commands;
using EasyLibraryApplication.WPF.Model;

namespace EasyLibraryApplication.WPF.ViewModel
{
    /// <summary>
    /// Klasa koja služi za spajanje pogleda za registraciju knjiznice za određenog korisnika
    /// i modela, te za izradu poslovne logike
    /// </summary>
    class RegisterToLibraryViewModel : INotifyPropertyChanged
    {
        #region Atributes

        public static User User { get; set; }
        public CollectionViewSource RegistredLibrarCollection { get; private set; }
        public CollectionViewSource NotRegistredLibrarCollection { get; private set; }
        public RegisterToLibraryCommand RegisterToLibraryEvent { get; set; }

        #endregion
        
        #region Private Filds

        private LibraryEntities ctx;

        #endregion

        #region Constructor

        /// <summary>
        /// Konstruktor koji stvara tri objekta od kojih su dva liste koje prikazuju podatke na pogledima i jedan objekt
        /// za događaj prilikom pritiska gumba. Poziva se metoda za prikaz podataka
        /// </summary>
        public RegisterToLibraryViewModel()
        {
            RegistredLibrarCollection= new CollectionViewSource();
            NotRegistredLibrarCollection = new CollectionViewSource();
            RegisterToLibraryEvent = new RegisterToLibraryCommand(this);
            LoadData();
        }

        #endregion

        #region LoadData

        /// <summary>
        /// Stvaranje novog objekta baze, učitavanje svih knjižnica. Dodjeljivanje knjižnica u liste pomoću
        /// pohranjenih procedura.
        /// </summary>
        public void Refresh()
        {
            ctx = new LibraryEntities();
            ctx.Libraries.Load();
            RegistredLibrarCollection.Source = ctx.GetAllLibrarysForUser(User.Id);
            NotRegistredLibrarCollection.Source = ctx.GetAllLibrarysForUserNotRegistered(User.Id);
        }

        /// <summary>
        /// Pozivanje metode Refresh i dodjeljivanje SelectedItem-a
        /// </summary>
        private void LoadData()
        {
            Refresh();
            SelectedItem = NotRegistredLibrarCollection.View.CurrentItem as Library;
        }

        #endregion

        #region RegisterToLibrary

        /// <summary>
        /// Metoda koja upisuje korisnika u bazu i dodaje mu izabranu knjižnicu
        /// </summary>
        public void RegisterToLibrary()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"Želite se učlaniti u {SelectedItem.Name}?", "Pozor", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                LibraryMember libraryMember = new LibraryMember()
                {
                    LibraryId = SelectedItem.Id,
                    UserId = User.Id,
                    MembershipStartDay = DateTime.Now
                };
                ctx.LibraryMembers.Add(libraryMember);
                ctx.SaveChanges();
                MessageBox.Show($"Uspješno ste se registrirali u {SelectedItem.Name}");
                Refresh();
            }
        }

        #endregion

        #region Selected Item

        private Library selectedItem;

        public Library SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        #endregion

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
