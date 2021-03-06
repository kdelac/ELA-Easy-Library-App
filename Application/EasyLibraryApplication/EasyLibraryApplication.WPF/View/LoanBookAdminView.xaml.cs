﻿using EasyLibraryApplication.WPF.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasyLibraryApplication.WPF.View
{
    /// <summary>
    /// Interaction logic for LoanBookAdminView.xaml
    /// </summary>
    /// 
    public partial class LoanBookAdminView : Page
    {
        private LoanBookAdminViewModel viewModel;
        public LoanBookAdminView()
        {
            InitializeComponent();
            viewModel = new LoanBookAdminViewModel();
            this.DataContext = viewModel;
        }
    }
}
