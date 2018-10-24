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

namespace EasyLibraryApplication.WPF.ViewModel
{
    class LoanBookAdminViewModel : INotifyPropertyChanged
    {
        private LibraryEntities ctx;

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Collections

        public CollectionViewSource LoanBooksAdminCollection { get; set; }
        #endregion

        #region SelectedProperties
        private Loan selectedLoan;

        public Loan SelectedLoan
        {
            get { return selectedLoan; }
            set
            {
                selectedLoan = value;
                OnPropertyChanged(nameof(SelectedLoan));
            }
        }

        public static Administrator AdminUser { get; set; }


        #endregion


        #region Constructor

        public LoanBookAdminViewModel()
        {
            LoanBooksAdminCollection = new CollectionViewSource();
            LoadData();
        }


        #endregion


        #region LoadData

        /// <summary>
        /// Učitavanje podataka 
        /// </summary>
        private void LoadData()
        {
            Refresh();
            SelectedLoan = LoanBooksAdminCollection.View.CurrentItem as Loan;

        }

        /// <summary>
        /// Dohvaćanje podataka iz baze podataka
        /// </summary>
        public void Refresh()
        {

            ctx = new LibraryEntities();
            ctx.Loans.Load();

            LoanBooksAdminCollection.Source =
                new ObservableCollection<Loan>(ctx.Loans.Where(x => x.Copy.Book.LibraryId == AdminUser.LibraryId).ToList());

        }

        #endregion


    }
}
