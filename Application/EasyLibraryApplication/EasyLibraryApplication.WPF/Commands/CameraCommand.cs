//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Windows.Input;
//using System.Threading.Tasks;
//using EasyLibraryApplication.WPF.ViewModel;

//namespace EasyLibraryApplication.WPF.Commands
//{
//    public class CameraCommand : ICommand
//    {
//        private AffirmatingReservationViewModel viewModel;

//        public CameraCommand(AffirmatingReservationViewModel vModel)
//        {
//            viewModel = vModel;
//        }
//        public bool CanExecute(object parameter)
//        {
//            return true;
//        }

//        /// <summary>
//        /// Metoda koja se izvršava prilikom pritiska na gumb, te se izvršava metoda iz viewModela
//        /// </summary>
//        /// <param name="parameter"></param>
//        public void Execute(object parameter)
//        {
//            viewModel.OpenCamera();
//        }

//        public event EventHandler CanExecuteChanged;
//    }
//}
