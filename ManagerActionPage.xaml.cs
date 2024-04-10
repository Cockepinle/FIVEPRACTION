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
    /// Логика взаимодействия для ManagerActionPage.xaml
    /// </summary>
    public partial class ManagerActionPage : Page
    {
        StoksTableAdapter stoks = new StoksTableAdapter();
        public ManagerActionPage()
        {
            InitializeComponent();
            SalonGrid.ItemsSource = stoks.GetData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Address.Text))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }
            stoks.InsertStoks(Address.Text, Convert.ToInt32(Quantity.Text));
            SalonGrid.ItemsSource = stoks.GetData();
            if (!IsDigitsOnly(Address.Text) || Address.Text.Length < 13 || Address.Text.Length > 20 || Address.Text.Count(c => c == '-') > 1)
            {
                MessageBox.Show("Дата должна содержать только цифры, быть не короче 13 символов и не длиннее 20 символов, а также содержать только один символ '-'.");
                return;
            }
            if (!int.TryParse(Quantity.Text, out int quantity) || quantity < 5 || quantity > 50)
            {
                MessageBox.Show("В поле проценты должно быть введено число не менее 5 и не более 50.");
                return;
            }

            stoks.InsertStoks(Address.Text, quantity);
            SalonGrid.ItemsSource = stoks.GetData();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SalonGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите элемент перед выполнением операции.");
                return;
            }
            object id = (SalonGrid.SelectedItem as DataRowView).Row[0];
            stoks.DeleteStoks(Convert.ToInt32(id));
            SalonGrid.ItemsSource = stoks.GetData();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (SalonGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите элемент перед выполнением операции.");
                return;
            }
            if (string.IsNullOrWhiteSpace(Address.Text))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }
            if(!IsDigitsOnly(Address.Text) || Address.Text.Length < 13 || Address.Text.Length > 13 || Address.Text.Count(c => c == '-') > 1)
    {
                MessageBox.Show("Дата должна содержать только цифры, быть не короче 13 символов и не длиннее 13 символов, а также содержать только один символ '-'.");
                return;
            }
            if (!int.TryParse(Quantity.Text, out int quantity) || quantity < 5 || quantity > 50)
            {
                MessageBox.Show("В поле проценты должно быть введено число не менее 5 и не более 50.");
                return;
            }

            object id = (SalonGrid.SelectedItem as DataRowView).Row[0];
            stoks.UpdateStoks(Address.Text, quantity, Convert.ToInt32(id));
            SalonGrid.ItemsSource = stoks.GetData();
        }

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
    }
}
