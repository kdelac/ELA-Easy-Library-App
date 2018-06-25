using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using EasyLibraryApplication.WPF.ViewModel;

namespace EasyLibraryApplication.WPF.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel(this);
            KeyDown += Login_KeyDown;
        }

        private void Login_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                Help.ShowHelp(null, "file://C:\\Faks\\Programsko inžinjerstvo\\Projekt\\Application\\EasyLibraryApplication\\EasyLibraryApplication.WPF\\HelperFiles\\Prijava.chm");
            }
        }
    }
}
