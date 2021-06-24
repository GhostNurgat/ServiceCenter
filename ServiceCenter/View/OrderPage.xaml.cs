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

namespace ServiceCenter.View
{
    using ServiceCenter.ViewModel;

    public partial class OrderPage : Page
    {
        OrderViewModel model = new OrderViewModel();

        public OrderPage()
        {
            InitializeComponent();
            this.DataContext = model;
        }

        private void UpdateButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrderPage());
        }

        private void SearchBySurname(object sender, TextChangedEventArgs e)
        {
            model.SearchBySurname();
        }

        private void SortDateOrder(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            model.SortDateOrder(comboBox.SelectedIndex);
        }
    }
}
