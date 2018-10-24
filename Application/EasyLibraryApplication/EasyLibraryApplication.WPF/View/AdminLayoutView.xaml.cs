using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

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
            uiFrame.Source = new Uri("CRUDBooksView.xaml", UriKind.Relative);
            KeyDown += OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Key == Key.F1)
            {
                Help.ShowHelp(null, "file://C:\\Users\\krist\\Documents\\Podaci\\Fakultet\\PI\\Projekt\\Application\\EasyLibraryApplication\\EasyLibraryApplication.WPF\\HelperFiles\\AdminPogled.chm");
            }
        }

        private void UiActionLogOff_OnClick(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.Close();
            login.Show();
        }

        private void UiActionOption1_OnClick(object sender, RoutedEventArgs e)
        {
            uiFrame.Source = new Uri("CRUDBooksView.xaml", UriKind.Relative);
        }

        private void UiActionOption2_OnClick(object sender, RoutedEventArgs e)
        {
            uiFrame.Source = new Uri("CRUDBookCopiesView.xaml", UriKind.Relative);
        }

        private void uiActionOption3_Click(object sender, RoutedEventArgs e)
        {
            uiFrame.Source = new Uri("LoanBookAdminView.xaml", UriKind.Relative);
        }

        private void uiActionOption4_Click(object sender, RoutedEventArgs e)
        {
            uiFrame.Source = new Uri("ConfirmationOfReturningBookView.xaml", UriKind.Relative);
        }

        private void uiActionOption5_Click(object sender, RoutedEventArgs e)
        {
            uiFrame.Source = new Uri("AffirmatingReservationView.xaml", UriKind.Relative);
        }
    }
}
