using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EasyLibraryApplication.WPF.Annotations;
using EasyLibraryApplication.WPF.Commands;
using EasyLibraryApplication.WPF.Model;
using EasyLibraryApplication.WPF.View;

namespace EasyLibraryApplication.WPF.ViewModel
{
    class RegistrationViewModel : INotifyPropertyChanged
    {
        #region Private Fields

        private LibraryEntities ctx;
        private RegistrationView registrationView;

        #endregion
        
        #region Constructor

        public RegistrationViewModel(RegistrationView regView)
        {
            registrationView = regView;
            RegistrationEvent = new RegistrationCommand(this);
        }

        #endregion

        #region Commands

        public RegistrationCommand RegistrationEvent { get; set; }

        #endregion

        #region Selected Item

        private User selectedItem;

        public User SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        #endregion

        public void UserRegistration()
        {
            registrationView.Close();
        }

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
