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
            RegisterEven = new RegisterCommand();
            loginView = login;
            SelectedItem = new User();
        }

        #region Commands

        public LoginCommand LoginEvent { get; set; }
        public RegisterCommand RegisterEven { get; set; }

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

        public void CheckUser()
        {
            User username = ctx.Users.FirstOrDefault(s => s.Username == SelectedItem.Username);
            User password = ctx.Users.FirstOrDefault(s => s.PasswordHash == "kdelac123");
            if (username != null && password != null)
            {
                MessageBox.Show($"Bok, {username.Username}");
                loginView.Close();
            }
            else
            {
                MessageBox.Show("Ne postoji");
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
