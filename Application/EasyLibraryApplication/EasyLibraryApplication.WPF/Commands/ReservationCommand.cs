using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            reservationViewModel.SendEmail();
            reservationViewModel.SaveReservationToDatabase();
        }

        public event EventHandler CanExecuteChanged;
    }
}
