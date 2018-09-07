using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using EasyLibraryApplication.WPF.Annotations;
using EasyLibraryApplication.WPF.Commands;
using EasyLibraryApplication.WPF.Model;
using EasyLibraryApplication.WPF.View;
using PasswordHash;

namespace EasyLibraryApplication.WPF.ViewModel
{
    /// <summary>
    /// Klasa koja služi za spajanje pogleda za prijavu i modela, te za izradu poslovne logike
    /// </summary>
    class LoginViewModel : INotifyPropertyChanged
    {
        #region Private Fields

        private LibraryEntities ctx;
        private Login loginView;

        #endregion

        #region Constructor

        /// <summary>
        /// Konstruktor koji stvara četiri objekta od kojih su dva objekt za događaj prilikom pritiska gumba.
        /// Stvaranje context objekta. Stvaranje novog korisnika. Dodjeljivanje pogleda.
        /// </summary>
        public LoginViewModel(Login login)
        {
            ctx = new LibraryEntities();
            LoginEvent = new LoginCommand(this);
            RegisterEvent = new RegisterCommand(this);
            loginView = login;
            SelectedItem = new User();

        }

        #endregion
        
        #region Views

        public LayoutView UserLayoutView { get; set; }
        public RegistrationView RegistrationView { get; set; }
        public AdminLayoutView AdminLayoutView { get; set; }

        #endregion
        
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

        #region ChangeViews

        /// <summary>
        /// Metoda koja prebacuje korisnika s pogleda za prijavu na korisnikov pogled.
        /// </summary>
        public void ChangeToUserView()
        {
            UserLayoutView = new LayoutView();
            loginView.Close();
            UserLayoutView.Show();
        }

        /// <summary>
        /// Metoda koja prebacuje korisnika s pogleda za prijavu na administratorov pogled.
        /// </summary>
        public void ChangeToAdminView()
        {
            AdminLayoutView = new AdminLayoutView();
            loginView.Close();
            AdminLayoutView.Show();
        }

        /// <summary>
        /// Metoda koja prebacuje korisnika s pogleda za prijavu na pogled za registraciju.
        /// </summary>
        public void ShowRegistrationView()
        {
            RegistrationView = new RegistrationView();
            RegistrationView.ShowDialog();
        }
         
        #endregion
        
        #region CheckUsers

        /// <summary>
        ///  Metoda koja provjeraca dali korisnik postoji u bazi
        /// </summary>
        public void CheckUser()
        { 
            Password passwordHash = new Password();
            User user = ctx.Users.FirstOrDefault(s => s.Username == SelectedItem.Username);

            if (user != null)
            {
                int check = 0;
                if (SelectedItem.Username != null && SelectedItem.PasswordHash != null)
                {
                    check = passwordHash.VerifyHashString(user.PasswordHash, SelectedItem.PasswordHash);
                }

                LibraryMember libMember = ctx.LibraryMembers.FirstOrDefault(s => s.UserId == user.Id);
                Administrator admin = ctx.Administrators.FirstOrDefault(s => s.UserId == user.Id);

                if (admin != null && check == 1)
                {
                    ChangeToAdminView();
                }
                else if (libMember != null && check == 1)
                {
                    ChangeToUserView();
                }
                else if (check == 0)
                {
                    MessageBox.Show("Pogresna lozinka");
                }

                RegisterToLibraryViewModel.User = user;
                UserBooksViewModel.User = user;
                LoanBookViewModel.User = user;
                CRUDBooksViewModel.AdminUser = admin;
                LoanBookAdminViewModel.AdminUser = admin;
                CRUDBookCopiesViewModel.AdminUser = admin;
            }
            else
            {
                MessageBox.Show("Provjerite korisničko ime ili lozinku!\nAko nemate račun registrirajte se!");
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
