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

    public partial class EditStaffWindow : Window
    {
        public StaffMember StaffMember { get; private set; }

        public EditStaffWindow(StaffMember staff)
        {
            InitializeComponent();
            StaffMember = staff;
            this.DataContext = new StaffValidationViewModel(StaffMember);
        }

        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
