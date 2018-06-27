using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using EasyLibraryApplication.WPF.ViewModel;

namespace EasyLibraryApplication.WPF.Commands
{
    class ReservationCommand : ICommand
    {
        private ReservationViewModel reservationViewModel;

        public ReservationCommand(ReservationViewModel vm)
        {
            reservationViewModel = vm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (reservationViewModel.CreateReservation())
            {
                Thread.Sleep(1);
                reservationViewModel.SendEmail();
                reservationViewModel.SaveReservationToDatabase();
                MessageBox.Show($"Uspješno ste rezervirali knjigu {ReservationViewModel.Book.Title} u knjižnici {reservationViewModel.SelectedLibrary.Name}, \npodaci o rezervaciji su poslani na Vaš e-mail.");
            }
            
          
        }

        public event EventHandler CanExecuteChanged;
    }
}
