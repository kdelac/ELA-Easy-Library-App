using System.Windows.Controls;
using EasyLibraryApplication.WPF.ViewModel;

namespace EasyLibraryApplication.WPF.View
{
    /// <summary>
    /// Interaction logic for RegiserToLibraryView.xaml
    /// </summary>
    public partial class RegiserToLibraryView : Page
    {
        private RegisterToLibraryViewModel registerToLibraryViewModel;
        public RegiserToLibraryView()
        {
            InitializeComponent();
            registerToLibraryViewModel = new RegisterToLibraryViewModel();
            this.DataContext = registerToLibraryViewModel;
        }
    }
}
