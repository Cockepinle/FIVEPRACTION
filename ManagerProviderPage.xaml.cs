using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для ManagerProviderPage.xaml
    /// </summary>
    public partial class ManagerProviderPage : Page
    {
        ProvidersTableAdapter providers = new ProvidersTableAdapter();
        public ManagerProviderPage()
        {
            InitializeComponent();
            SalonGrid.ItemsSource = providers.GetData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(Address.Text) || string.IsNullOrWhiteSpace(Quantity.Text))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

            if (!Regex.IsMatch(Address.Text, "^[A-ZА-Я][A-Za-zА-Яа-я]*$"))
            {
                MessageBox.Show("Фирма должна начинаться с большой буквы и содержать буквы русского или английского алфавита.");
                return;
            }

            if (!Regex.IsMatch(Quantity.Text, "^[7][0-9]{10}$"))
            {
                MessageBox.Show("Некорректный номер. Номер должен начинаться с 7 и состоять из 11 цифр.");
                return;
            }

            providers.InsertProviders(Address.Text, Quantity.Text);
            SalonGrid.ItemsSource = providers.GetData();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SalonGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите элемент для выполнения операции.");
                return;
            }

            object id = (SalonGrid.SelectedItem as DataRowView).Row[0];
            providers.DeleteProviders(Convert.ToInt32(id));
            SalonGrid.ItemsSource = providers.GetData();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (SalonGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите элемент для выполнения операции.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Address.Text) || string.IsNullOrWhiteSpace(Quantity.Text))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

            if (!Regex.IsMatch(Address.Text, "^[A-ZА-Я][A-Za-zА-Яа-я]*$"))
            {
                MessageBox.Show("Фирма должна начинаться с большой буквы и содержать буквы русского или английского алфавита.");
                return;
            }

            if (!Regex.IsMatch(Quantity.Text, "^[7][0-9]{10}$"))
            {
                MessageBox.Show("Некорректный номер. Номер должен начинаться с 7 и состоять из 11 цифр.");
                return;
            }

            object id = (SalonGrid.SelectedItem as DataRowView).Row[0];
            providers.UpdateProviders(Address.Text, Quantity.Text, Convert.ToInt32(id));
            SalonGrid.ItemsSource = providers.GetData();
        }
    }
}
