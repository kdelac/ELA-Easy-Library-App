using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using EasyLibraryApplication.WPF.Annotations;
using EasyLibraryApplication.WPF.Commands;
using EasyLibraryApplication.WPF.Model;
using EasyLibraryApplication.WPF.View;
using PasswordHash;

namespace EasyLibraryApplication.WPF.ViewModel
{
    class RegistrationViewModel : INotifyPropertyChanged
    {
        public CollectionViewSource Collection { get; private set; }

        #region Private Fields

        private LibraryEntities ctx;
        private RegistrationView registrationView;


        #endregion

        #region Constructor

        public RegistrationViewModel(RegistrationView regView)
        {
            Collection = new CollectionViewSource();
            LoadData();
            registrationView = regView;
            RegistrationEvent = new RegistrationCommand(this);
            SelectedUser = new User();
        }

        #endregion

        #region Commands

        public RegistrationCommand RegistrationEvent { get; set; }

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

        #region Selected User

        private User selectedUser;

        public User SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        #endregion
        
        #region LoadData

        public void Refresh()
        {
            ctx = new LibraryEntities();
            ctx.Libraries.Load();
            Collection.Source = ctx.Libraries.Local;
        }

        private void LoadData()
        {
            Refresh();
            SelectedItem = Collection.View.CurrentItem as Library;
        }

        #endregion
        
        #region SaveDataToDatabase

        public void SaveChanges()
        {
            if (SelectedUser.Name != null && SelectedUser.Surname != null && SelectedUser.Username != null && SelectedUser.PasswordHash != null && SelectedUser.Email != null && SelectedUser.City != null)
            {
                Password passwoord = new Password();
                string hash = passwoord.GetHashString(SelectedUser.PasswordHash);
                SelectedUser.PasswordHash = hash;
                User user = ctx.Users.FirstOrDefault(s => s.Username == SelectedUser.Username);
                bool result = IsValidEmailAddress(SelectedUser.Email);
                if (user == null)
                {
                    if (result)
                    {
                        try
                        {
                            ctx.Users.Add(SelectedUser);
                            ctx.SaveChanges();
                            LibraryMember libraryMember = new LibraryMember()
                            {
                                LibraryId = SelectedItem.Id,
                                MembershipStartDay = DateTime.Now,
                                UserId = selectedUser.Id
                            };
                            ctx.LibraryMembers.Add(libraryMember);
                            ctx.SaveChanges();
                            MessageBox.Show("Uspješno ste se registrirali!", "Registriran!", MessageBoxButton.OK,
                                MessageBoxImage.Information);
                            registrationView.Close();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Korisničko ime može imati najviše 10 znakova", "Pozor", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Morate unjeti ispravan email");

                    }
                }
                else
                {
                    MessageBox.Show("Korisničko ime je zauzeto", "Pozor!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
            else
            {
                MessageBox.Show("Potrebno je unjeti sve podatke!", "Pogreška!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        #endregion

        #region CheckEmail

        public static bool IsValidEmailAddress(string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
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
