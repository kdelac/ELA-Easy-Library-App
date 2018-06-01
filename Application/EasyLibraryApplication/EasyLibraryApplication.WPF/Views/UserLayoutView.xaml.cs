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
        }

        private void UiActionOption1_OnClick(object sender, RoutedEventArgs e)
        {
            uiActionRec1.Fill = Brushes.Blue;
            uiActionRec2.Fill = Brushes.LightGray;
        }

        private void UiActionOption2_OnClick(object sender, RoutedEventArgs e)
        {
            uiActionRec1.Fill = Brushes.LightGray;
            uiActionRec2.Fill = Brushes.Blue;
        }
    }
}
