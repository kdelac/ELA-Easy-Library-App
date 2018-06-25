using System.Windows;

namespace EasyLibraryApplication.WPF.View
{
    /// <summary>
    /// Interaction logic for AdminLayoutView.xaml
    /// </summary>
    public partial class AdminLayoutView : Window
    {
        public AdminLayoutView()
        {
            InitializeComponent();
        }

        private void UiActionLogOff_OnClick(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.Close();
            login.Show();
        }
    }
}
