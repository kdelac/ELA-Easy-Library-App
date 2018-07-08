using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EasyLibraryApplication.WPF.ViewModel;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace EasyLibraryApplication.WPF.View
{
    /// <summary>
    /// Interaction logic for ReservationView.xaml
    /// </summary>
    public partial class ReservationView : Window
    {
        private ReservationViewModel viewModel;
        public ReservationView()
        {
            InitializeComponent();
            viewModel = new ReservationViewModel();
            this.DataContext = viewModel;
            KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Key == Key.F1)
            {
                Help.ShowHelp(null, "file://C:\\Users\\krist\\Documents\\Podaci\\Fakultet\\PI\\Projekt\\Application\\EasyLibraryApplication\\EasyLibraryApplication.WPF\\HelperFiles\\RezervacijaKnjige.chm");
            }
        }
    }
}
