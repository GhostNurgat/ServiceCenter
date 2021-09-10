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

    public partial class TypeTechnologyPage : Page
    {
        TypeTechnologyViewModel model = new TypeTechnologyViewModel();

        public TypeTechnologyPage()
        {
            InitializeComponent();
            this.DataContext = model;
        }

        private void UpdateButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TypeTechnologyPage());
        }
    }
}
