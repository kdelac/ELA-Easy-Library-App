using System;
using System.Collections.Generic;
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
    class RegisterToLibraryViewModel : INotifyPropertyChanged
    {
        public static int UserID { get; set; }
        public CollectionViewSource RegistredLibrarCollection { get; private set; }
        public CollectionViewSource NotRegistredLibrarCollection { get; private set; }
        public RegisterToLibraryCommand RegisterToLibraryEvent { get; set; }

        #region Private Filds

        private LibraryEntities ctx;

        #endregion

        #region Constructor

        public RegisterToLibraryViewModel()
        {
            RegistredLibrarCollection= new CollectionViewSource();
            NotRegistredLibrarCollection = new CollectionViewSource();
            RegisterToLibraryEvent = new RegisterToLibraryCommand(this);
            LoadData();
        }

        #endregion

        #region LoadData

        public void Refresh()
        {
            ctx = new LibraryEntities();
            ctx.Libraries.Load();
            RegistredLibrarCollection.Source = ctx.GetAllLibrarysForUser(UserID);
            NotRegistredLibrarCollection.Source = ctx.GetAllLibrarysForUserNotRegistered(UserID);
        }

        private void LoadData()
        {
            Refresh();
            SelectedItem = NotRegistredLibrarCollection.View.CurrentItem as Library;
        }

        #endregion

        #region RegisterToLibrary

        public void RegisterToLibrary()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show($"Želite se učlaniti u {SelectedItem.Name}?", "Pozor", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                LibraryMember libraryMember = new LibraryMember()
                {
                    LibraryId = SelectedItem.Id,
                    UserId = UserID,
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

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
