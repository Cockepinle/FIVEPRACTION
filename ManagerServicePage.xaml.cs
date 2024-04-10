using Salon.BeautySalonDataSetTableAdapters;
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
    /// Логика взаимодействия для ManagerServicePage.xaml
    /// </summary>
    public partial class ManagerServicePage : Page
    {
        ServicessTableAdapter service = new ServicessTableAdapter();
        StoksTableAdapter stoks = new StoksTableAdapter();
        public ManagerServicePage()
        {
            InitializeComponent();
            SalonGrid.ItemsSource = service.GetData();
            Pro.ItemsSource = stoks.GetData();
            Pro.DisplayMemberPath = "Date_Stocks";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(Address.Text) || string.IsNullOrWhiteSpace(Quantity.Text))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

            if (!Regex.IsMatch(Address.Text, "^[А-Я][а-я]*$"))
            {
                MessageBox.Show("Название услуги должно начинаться с большой буквы и содержать только буквы русского алфавита.");
                return;
            }

            if (!int.TryParse(Quantity.Text, out int quantity) || quantity < 1000 || quantity > 500000)
            {
                MessageBox.Show("Цена должна быть целым числом от 1000 до 500000.");
                return;
            }
            foreach (var item in SalonGrid.Items)
            {
                if (item is DataRowView row)
                {
                    if (row.Row.ItemArray[1].ToString() == Address.Text)
                    {
                        MessageBox.Show("Процедура с таким названием уже существует.");
                        return;
                    }
                }
            }
            if (Pro.SelectedItem != null)
            {
                int rol = Convert.ToInt32((Pro.SelectedItem as DataRowView).Row[0]);
                service.InsertServices(Address.Text, Convert.ToInt32(Quantity.Text), rol);
                SalonGrid.ItemsSource = service.GetData();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SalonGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите элемент для выполнения операции.");
                return;
            }
            if (Pro.SelectedItem != null)
            {
                object id = (Pro.SelectedItem as DataRowView).Row[0];
                service.DeleteService(Convert.ToInt32(id));
                SalonGrid.ItemsSource = service.GetData();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (SalonGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите элемент для выполнения операции.");
                return;
            }
            if (!Regex.IsMatch(Address.Text, "^[А-Я][а-я]*$"))
            {
                MessageBox.Show("Название услуги должно начинаться с большой буквы и содержать только буквы русского алфавита.");
                return;
            }

            if (!int.TryParse(Quantity.Text, out int quantity) || quantity < 1000 || quantity > 500000)
            {
                MessageBox.Show("Цена должна быть целым числом от 1000 до 500000.");
                return;
            }
            foreach (DataRowView row in SalonGrid.Items)
            {
                if (row.Row.ItemArray[1].ToString() == Address.Text)
                {
                    MessageBox.Show("Процедура с таким названием уже существует.");
                    return;
                }
            }
            if (Pro.SelectedItem != null)
            {
                object id = (Pro.SelectedItem as DataRowView).Row[0];
                int rol = Convert.ToInt32((Pro.SelectedItem as DataRowView).Row[0]);
                service.UpdateService(Address.Text, Convert.ToInt32(Quantity.Text), rol, Convert.ToInt32(id));
                SalonGrid.ItemsSource = service.GetData();
            }
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            List<ModelClass> forImport = ClassConverter.DeserializeObject<List<ModelClass>>();
            foreach (var item in forImport)
            {
                service.InsertServices(item.servicesName, item.price, item.id);
            }

            SalonGrid.ItemsSource = null;
            SalonGrid.ItemsSource = service.GetData();
        }
    }
}
