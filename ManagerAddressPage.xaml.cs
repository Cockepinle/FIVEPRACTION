using Salon.BeautySalonDataSetTableAdapters;
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
using System.Text.RegularExpressions;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для ManagerAddressPage.xaml
    /// </summary>
    public partial class ManagerAddressPage : Page
    {
        StudioAddressTableAdapter address = new StudioAddressTableAdapter();
        public ManagerAddressPage()
        {
            InitializeComponent();
            SalonGrid.ItemsSource = address.GetData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string role = Address.Text;

            // Проверка длины адреса и наличия только русских букв и пробелов
            string pattern = "^(?=.*[0-9])[а-яА-Я0-9 ]{10,}$";
            if (!Regex.IsMatch(role, pattern))
            {
                MessageBox.Show("Адрес должен содержать только русские буквы, цифры и пробелы, и быть не менее 10 символов с хотя бы одной цифрой.");
                return;
            }

            if (CheckIfRoleExists(role))
            {
                MessageBox.Show("Адрес уже существует. Нельзя добавить существующий адрес.");
                return;
            }

            address.InsertAddress(Address.Text);
            SalonGrid.ItemsSource = address.GetData();
        }
        private bool CheckIfRoleExists(string role)
        {
            foreach (var item in SalonGrid.Items)
            {
                if (item is DataRowView)
                {
                    DataRowView row = item as DataRowView;
                    string existingRole = row["StudioAddress"].ToString();
                    if (existingRole == role)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SalonGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите элемент перед выполнением операции.");
                return;
            }
            object id = (SalonGrid.SelectedItem as DataRowView).Row[0];
            address.DeleteAddress(Convert.ToInt32(id));
            SalonGrid.ItemsSource = address.GetData();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (SalonGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите элемент перед выполнением операции.");
                return;
            }
            object id = (SalonGrid.SelectedItem as DataRowView).Row[0];
            address.UpdateAddress(Address.Text, Convert.ToInt32(id));
            SalonGrid.ItemsSource = address.GetData();
        }
    }
}
