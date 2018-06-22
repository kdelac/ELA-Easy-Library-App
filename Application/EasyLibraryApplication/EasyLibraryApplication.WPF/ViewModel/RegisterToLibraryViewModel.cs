using System;
using System.Collections.Generic;
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
    class RegisterToLibraryViewModel : INotifyPropertyChanged
    {
        public CollectionViewSource Collection { get; private set; }

        #region Private Filds

        private LibraryEntities ctx;

        #endregion

        #region Constructor

        public RegisterToLibraryViewModel()
        {
            Collection = new CollectionViewSource();
            LoadData();
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
