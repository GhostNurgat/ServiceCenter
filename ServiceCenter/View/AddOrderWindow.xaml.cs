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

namespace ServiceCenter.View
{
    using ServiceCenter.Models;
    using ServiceCenter.ViewModel;

    public partial class AddOrderWindow : Window
    {
        public Order Order { get; private set; }

        public AddOrderWindow(Order order)
        {
            InitializeComponent();
            Order = order;
            this.DataContext = new OrderValidationViewModel(Order);
        }

        private void AcceptButton(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
