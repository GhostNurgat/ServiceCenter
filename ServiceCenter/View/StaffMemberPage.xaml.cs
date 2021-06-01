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

namespace ServiceCenter.View
{
    using ServiceCenter.ViewModel;
    using ServiceCenter.View;

    public partial class StaffMemberPage : Page
    {
        public StaffMemberPage()
        {
            InitializeComponent();
            this.DataContext = new StaffViewModel();
        }

        private void UpdateButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StaffMemberPage());
        }
    }
}