using Salon.BeautySalonDataSetTableAdapters;
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
using Salon.BeautySalonDataSetTableAdapters;
using System.Data;
using System.Net;
using System.Text.RegularExpressions;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для ManagerProductPage.xaml
    /// </summary>
    public partial class ManagerProductPage : Page
    {
        ConsumablesTableAdapter consumables = new ConsumablesTableAdapter();
        ProvidersTableAdapter providers = new ProvidersTableAdapter();
        public ManagerProductPage()
        {
            InitializeComponent();
            SalonGrid.ItemsSource = consumables.GetData();
            Pro.ItemsSource = providers.GetData();
            Pro.DisplayMemberPath = "Company_Name";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Address.Text) || string.IsNullOrWhiteSpace(Quantity.Text) || Pro.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            string patternCommas = "^[а-яА-Я,]+$";
            string patternDigits = "^[0-9]+$";

            if (!Regex.IsMatch(Address.Text, patternCommas))
            {
                MessageBox.Show("Название продукта должно содержать запятые вместо пробелов.");
                return;
            }

            if (!Regex.IsMatch(Quantity.Text, patternDigits))
            {
                MessageBox.Show("Пожалуйста, введите только цифры в поле общего количества.");
                return;
            }

            int rol = Convert.ToInt32((Pro.SelectedItem as DataRowView).Row[0]);
            consumables.InsertProduct(Address.Text, Convert.ToInt32(Quantity.Text), rol);
            SalonGrid.ItemsSource = consumables.GetData();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SalonGrid.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите продукт для удаления.");
                return;
            }

            object id = (SalonGrid.SelectedItem as DataRowView).Row[0];
            consumables.DeleteProduct(Convert.ToInt32(id));
            SalonGrid.ItemsSource = consumables.GetData();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(Address.Text) || string.IsNullOrWhiteSpace(Quantity.Text) || Pro.SelectedItem == null || SalonGrid.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля и выберите продукт для обновления.");
                return;
            }

            string patternCommas = "^[а-яА-Я,]+$";
            string patternDigits = "^[0-9]+$";

            if (!Regex.IsMatch(Address.Text, patternCommas))
            {
                MessageBox.Show("Название продукта должно содержать запятые вместо пробелов.");
                return;
            }

            if (!Regex.IsMatch(Quantity.Text, patternDigits))
            {
                MessageBox.Show("Пожалуйста, введите только цифры в поле общего количества.");
                return;
            }

            object id = (SalonGrid.SelectedItem as DataRowView).Row[0];
            int rol = Convert.ToInt32((Pro.SelectedItem as DataRowView).Row[0]);
            consumables.UpdateProduct(Address.Text, Convert.ToInt32(Quantity.Text), rol, Convert.ToInt32(id));
            SalonGrid.ItemsSource = consumables.GetData();
        }
    }
}
