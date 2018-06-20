using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EasyLibraryApplication.WPF.Annotations;
using EasyLibraryApplication.WPF.Commands;
using EasyLibraryApplication.WPF.Model;
using EasyLibraryApplication.WPF.View;
using PasswordHash;

namespace EasyLibraryApplication.WPF.ViewModel
{
    class LoginViewModel : INotifyPropertyChanged
    {
        private LibraryEntities ctx;
        private Login loginView;

        /// <summary>
        /// Konstruktor klase 
        /// </summary>
        public LoginViewModel(Login login)
        {
            ctx = new LibraryEntities();
            LoginEvent = new LoginCommand(this);
            RegisterEvent = new RegisterCommand(this);
            loginView = login;
            SelectedItem = new User();
            
        }

        public LayoutView UserLayoutView { get; set; }
        public RegistrationView RegistrationView { get; set; }
        public AdminLayoutView AdminLayoutView { get; set; }

        #region Commands

        public LoginCommand LoginEvent { get; set; }
        public RegisterCommand RegisterEvent { get; set; }

        #endregion

        #region Selected Item

        private User selectedItem;

        public User SelectedItem {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        #endregion

        /// <summary>
        /// Metoda koja prebacuje korisnika s ekrana za prijavu na korisnikov ekran.
        /// </summary>
        public void ChangeToUserView()
        {
            UserLayoutView = new LayoutView();
            loginView.Close();
            UserLayoutView.Show();
        }

        /// <summary>
        /// Metoda koja prebacuje korisnika s ekrana za prijavu na administratorov ekran.
        /// </summary>
        public void ChangeToAdminView()
        {
            AdminLayoutView = new AdminLayoutView();
            loginView.Close();
            AdminLayoutView.Show();
        }

        public void ShowRegistrationView()
        {
            RegistrationView = new RegistrationView();
            RegistrationView.ShowDialog();
        }

        public void CheckUser()
        {
            User username = ctx.Users.FirstOrDefault(s => s.Username == SelectedItem.Username);
            User password = ctx.Users.FirstOrDefault(s => s.PasswordHash == "kdelac123");
            if (username != null && password != null)
            {
                MessageBox.Show($"Bok, {username.Username}");
                ChangeToUserView();
            }
            else
            {
                ChangeToAdminView();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
