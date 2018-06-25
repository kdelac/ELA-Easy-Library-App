using System.Windows.Controls;
using EasyLibraryApplication.WPF.ViewModel;

namespace EasyLibraryApplication.WPF.View
{
    /// <summary>
    /// Interaction logic for RegiserToLibraryView.xaml
    /// </summary>
    public partial class RegiserToLibraryView : Page
    {
        public RegiserToLibraryView()
        {
            InitializeComponent();
            this.DataContext = new RegisterToLibraryViewModel();
        }
    }
}
