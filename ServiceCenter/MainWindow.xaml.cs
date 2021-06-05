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

namespace ServiceCenter
{
    using ServiceCenter.View;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GoToStaffPage(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new StaffMemberPage());
        }

        private void GoToClientPage(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new ClientPage());
        }

        private void GoToOrderPage(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new OrderPage());
        }
    }
}
