using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using MessageBox = System.Windows.MessageBox;

namespace EasyLibraryApplication.WPF.View
{
    /// <summary>
    /// Interaction logic for LayoutView.xaml
    /// </summary>
    public partial class LayoutView : Window
    {
      
        public LayoutView()
        {
            InitializeComponent();
            uiActionRec1.Fill = Brushes.LightSkyBlue;
            uiFrame.Source = new Uri("UserBooksView.xaml", UriKind.Relative);
            KeyDown += LayoutView_KeyDown;
        }

        private void LayoutView_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                Help.ShowHelp(null, "file://C:\\Faks\\Programsko inžinjerstvo\\Projekt\\Application\\EasyLibraryApplication\\EasyLibraryApplication.WPF\\HelperFiles\\KorisnickiPogled.chm");
            }
        }

        private void UiActionOption1_OnClick(object sender, RoutedEventArgs e)
        {
            uiActionRec1.Fill = Brushes.LightSkyBlue;
            uiActionRec2.Fill = Brushes.AliceBlue;
            uiActionRec3.Fill = Brushes.AliceBlue;
            uiFrame.Source = new Uri("UserBooksView.xaml", UriKind.Relative);
        }

        private void UiActionOption2_OnClick(object sender, RoutedEventArgs e)
        {
            uiActionRec1.Fill = Brushes.AliceBlue;
            uiActionRec3.Fill = Brushes.AliceBlue;
            uiActionRec2.Fill = Brushes.LightSkyBlue;
            //uiFrame.Source = new Uri("Page1.xaml", UriKind.Relative);
        }

        private void UiActionOption3_OnClick(object sender, RoutedEventArgs e)
        {
            uiActionRec1.Fill = Brushes.AliceBlue;
            uiActionRec2.Fill = Brushes.AliceBlue;
            uiActionRec3.Fill = Brushes.LightSkyBlue;
            uiFrame.Source = new Uri("RegiserToLibraryView.xaml", UriKind.Relative);
        }

        private void UiActionLogoOff_OnClick(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.Close();
            login.Show();
        }
    }
}
