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
    using System.IO;

    public partial class ChangeCodeWindow : Window
    {
        string path = "code.bin";

        public ChangeCodeWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (newCode.Password == Code.Password && newCode.Password.Length == 4)
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
                {
                    writer.Write(newCode.Password);
                }

                MessageBox.Show("Код успешно изменён!", "Изменение кода",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                Close();
            }
            else
                MessageBox.Show("Несовпадает код или превышен длины кода.", "Изменение кода",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
