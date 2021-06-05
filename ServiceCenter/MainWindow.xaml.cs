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
    using ServiceCenter.Login;

    public partial class MainWindow : Window
    {
        int CountUser = 0;

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

        private void LoginWindow(object sender, RoutedEventArgs e)
        {
            var change = new ChangeCodeWindow();
            change.ShowDialog();
        }

        private void Login(object sender, MouseButtonEventArgs e)
        {
            if (CountUser == 0)
            {
                var login = new LoginWindow();
                if (login.ShowDialog() == true)
                {
                    MessageBox.Show("Вход выполнен успешно!", "Авторизация",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    CountUser = 1;

                    staffPage.Visibility = Visibility.Visible;
                    clientPage.Visibility = Visibility.Visible;
                    orderPage.Visibility = Visibility.Visible;
                    changeButton.Visibility = Visibility.Visible;
                    loginLabel.Content = "Выход";
                }
                else
                    MessageBox.Show("Вы ввели неверный код!", "Авторизация",
                        MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (CountUser == 1)
            {
                var result = MessageBox.Show("Вы уверены, что хотите выйти из системы?", "Выход",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    MainFrame.Navigate(null);

                    staffPage.Visibility = Visibility.Hidden;
                    clientPage.Visibility = Visibility.Hidden;
                    orderPage.Visibility = Visibility.Hidden;
                    changeButton.Visibility = Visibility.Hidden;
                    loginLabel.Content = "Вход";
                }
            }
        }
    }
}
