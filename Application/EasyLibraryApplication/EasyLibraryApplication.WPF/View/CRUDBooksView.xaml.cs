﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using EasyLibraryApplication.WPF.ViewModel;

namespace EasyLibraryApplication.WPF.View
{
    /// <summary>
    /// Interaction logic for CRUDBooksView.xaml
    /// </summary>
    public partial class CRUDBooksView : Page
    {
        private CRUDBooksViewModel viewModel;
        public CRUDBooksView()
        {
            InitializeComponent();
            viewModel = new CRUDBooksViewModel();
            DataContext = viewModel;
        }
    }
}
