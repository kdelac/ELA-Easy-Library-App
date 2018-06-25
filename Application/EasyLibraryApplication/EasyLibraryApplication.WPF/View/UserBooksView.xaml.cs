using System.Windows.Controls;
using EasyLibraryApplication.WPF.ViewModel;

namespace EasyLibraryApplication.WPF.View
{
    /// <summary>
    /// Interaction logic for UserBooksView.xaml
    /// </summary>
    public partial class UserBooksView : Page
    {
        public UserBooksView()
        {
            InitializeComponent();
            this.DataContext = new UserBooksViewModel();
        }
    }
}
