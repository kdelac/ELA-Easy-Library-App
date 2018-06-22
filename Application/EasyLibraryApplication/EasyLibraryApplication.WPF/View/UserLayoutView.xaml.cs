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
using System.Windows.Shapes;
using EasyLibraryApplication.WPF.Model;
using EasyLibraryApplication.WPF.ViewModel;

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
    }
}
