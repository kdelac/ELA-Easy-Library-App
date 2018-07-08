using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EasyLibraryApplication.WPF.ViewModel;

namespace EasyLibraryApplication.WPF.View
{
    /// <summary>
    /// Interaction logic for LoanBooksView.xaml
    /// </summary>
    public partial class LoanBooksView : Page
    {
        private LoanBookViewModel viewModel;
        public LoanBooksView()
        {
            InitializeComponent();
            viewModel = new LoanBookViewModel();
            DataContext = viewModel;
        }
    }
}
