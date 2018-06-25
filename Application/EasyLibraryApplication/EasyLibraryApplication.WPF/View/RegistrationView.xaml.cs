using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using EasyLibraryApplication.WPF.ViewModel;

namespace EasyLibraryApplication.WPF.View
{
    /// <summary>
    /// Interaction logic for RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : Window
    {
        private RegistrationViewModel registrationViewModel;
        public RegistrationView()
        {
            InitializeComponent();
            registrationViewModel = new RegistrationViewModel(this);
            this.DataContext = registrationViewModel;
            KeyDown += RegistrationView_KeyDown;
        }

        private void RegistrationView_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                Help.ShowHelp(null, "file://C:\\Faks\\Programsko inžinjerstvo\\Projekt\\Application\\EasyLibraryApplication\\EasyLibraryApplication.WPF\\HelperFiles\\Registracija.chm");
            }
        }
    }
}
