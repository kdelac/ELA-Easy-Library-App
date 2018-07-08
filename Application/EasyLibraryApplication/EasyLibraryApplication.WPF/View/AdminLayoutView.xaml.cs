using System;
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
            uiFrame.Source = new Uri("CRUDBooksView.xaml", UriKind.Relative);
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
    }
}
