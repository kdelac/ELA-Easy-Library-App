using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EasyLibraryApplication.WPF.ViewModel;

namespace EasyLibraryApplication.WPF.Commands
{
    class SaveBookCopyCommand : ICommand
    {
        private CRUDBookCopiesViewModel viewModel;

        /// <summary>
        /// Konstruktor klase
        /// </summary>
        /// <param name="vm"></param>
        public SaveBookCopyCommand(CRUDBookCopiesViewModel vm)
        {
            viewModel = vm;
        }
        /// <summary>
        ///  Metoda koja određuje može li se spremanje knjige u bazu izvršiti
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>Vrća istinu ili laž ovisno može li se izvšiti naredba ili ne</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Metoda koja izvršava naredbu spremanja kopije knjige
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            viewModel.SaveChanges();
        }

        public event EventHandler CanExecuteChanged;
    }
}
