using EasyLibraryApplication.WPF.Annotations;
using EasyLibraryApplication.WPF.Commands;
using EasyLibraryApplication.WPF.Model;
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

namespace EasyLibraryApplication.WPF.ViewModel
{
    public class ConfirmationOfReturningBookViewModel : INotifyPropertyChanged
    {
        private LibraryEntities ctx;

        #region InputNumber

        private int number;
        public int bookNumber
        {
            get => number;
            set
            {
                if (number != value)
                {
                    number = value;
                    OnPropertyChanged(nameof(bookNumber));

                }
            }
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region StaticProperties
        public static Administrator AdminUser;
        #endregion

        #region Collections

        public CollectionViewSource LoansCollection { get; set; }

        #endregion


        #region Commands

        public GetLoanBookCommand GetLoanBookEvent { get; set; }
        public DeleteLoanCommand DeleteLoanEvent { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Konstruktor klase ConfirmationOfReturningBookViewModel
        /// </summary>
        public ConfirmationOfReturningBookViewModel()
        {
            LoansCollection = new CollectionViewSource();
            GetLoanBookEvent = new GetLoanBookCommand(this);
            DeleteLoanEvent = new DeleteLoanCommand(this);
        }
        #endregion


        /// <summary>
        /// Dohvaćanje podataka iz baze podataka
        /// </summary>
        public void Refresh()
        {
            ctx = new LibraryEntities();
            ctx.Loans.Load();

            LoansCollection.Source = new ObservableCollection<Loan>(ctx.Loans.Where(x => x.CopyId == bookNumber).ToList());            
        }

        /// <summary>
        /// Brise knjigu iz posuđenih
        /// </summary>
        public void DeleteLoan()
        {
            ctx.Loans.Remove(LoansCollection.View.CurrentItem as Loan);
            ctx.SaveChanges();
            Refresh();
        }

    }
}
