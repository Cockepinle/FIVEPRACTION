using System;
using System.Collections.Generic;
using System.Data;
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
using Salon.BeautySalonDataSetTableAdapters;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для ManagerSertivicatePage.xaml
    /// </summary>
    public partial class ManagerSertivicatePage : Page
    {
        Certificate_UserTableAdapter certificate = new Certificate_UserTableAdapter();
        public ManagerSertivicatePage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Address.Text))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }
            int amount = Convert.ToInt32(Address.Text);
            if (amount < 1000 || amount > 500000)
            {
                MessageBox.Show("Сумма должна быть больше 1000 и меньше 500000.");
                return;
            }

            certificate.InsertCertificate(amount);
            SalonGrid.ItemsSource = certificate.GetData();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            object id = (SalonGrid.SelectedItem as DataRowView).Row[0];
            certificate.DeleteCertificate(Convert.ToInt32(id));
            SalonGrid.ItemsSource = certificate.GetData();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Address.Text) || SalonGrid.SelectedItem == null)
            {
                MessageBox.Show("Заполните все поля и выберите элемент для выполнения операции.");
                return;
            }
            int amount = Convert.ToInt32(Address.Text);
            if (amount < 1000 || amount > 500000)
            {
                MessageBox.Show("Сумма должна быть больше 1000 и меньше 500000.");
                return;
            }

            object id = (SalonGrid.SelectedItem as DataRowView).Row[0];
            certificate.UpdateCertificate(amount, Convert.ToInt32(id));
            SalonGrid.ItemsSource = certificate.GetData();
        }
    }
}

