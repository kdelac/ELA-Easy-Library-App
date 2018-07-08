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
    class LoanBookViewModel : INotifyPropertyChanged
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

        public CollectionViewSource LoanBooksCollection { get; set; }
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


        #endregion

        #region StaticFields

        public static User User;

        #endregion

        #region Constructor

        public LoanBookViewModel()
        {
            LoanBooksCollection = new CollectionViewSource();
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
            SelectedLoan = LoanBooksCollection.View.CurrentItem as Loan;

        }

        /// <summary>
        /// Dohvaćanje podataka iz baze podataka
        /// </summary>
        public void Refresh()
        {
           
            ctx = new LibraryEntities();
            ctx.Loans.Load();

            LoanBooksCollection.Source =
                new ObservableCollection<Loan>(ctx.Loans.Where(ln => ln.UserId == User.Id).ToList());
            
        }

        #endregion


    }
}
