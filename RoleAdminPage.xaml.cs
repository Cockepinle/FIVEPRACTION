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
using static MaterialDesignThemes.Wpf.Theme;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для RoleAdminPage.xaml
    /// </summary>
    public partial class RoleAdminPage : Page
    {
        PositionTableAdapter position = new PositionTableAdapter();
        public RoleAdminPage()
        {
            InitializeComponent();
            SalonGrid.ItemsSource = position.GetData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            string role = Role.Text;

            if (CheckIfRoleExists(role))
            {
                MessageBox.Show("Роль уже существует. Нельзя добавить существующую роль.");
                return;
            }

            position.InsertPosition(role, Convert.ToInt32(Salary.Text));
            SalonGrid.ItemsSource = position.GetData();
        }
        private bool CheckIfRoleExists(string role)
        {
            foreach (var item in SalonGrid.Items)
            {
                if (item is DataRowView)
                {
                    DataRowView row = item as DataRowView;
                    string existingRole = row["Position"].ToString();
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
            object id = (SalonGrid.SelectedItem as DataRowView).Row[0];
            position.DeletePosition(Convert.ToInt32(id));
            SalonGrid.ItemsSource = position.GetData();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            object id = (SalonGrid.SelectedItem as DataRowView).Row[0];
            position.UpdatePosition(Role.Text, Convert.ToInt32(Salary.Text), Convert.ToInt32(id));
            SalonGrid.ItemsSource = position.GetData();
        }

        private bool ValidateInput()
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(Role.Text, @"^[A-Za-zА-Яа-я]+$"))
            {
                MessageBox.Show("Можно вводить только буквы в поле роли");
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(Salary.Text, @"^(4\d{4,6}|[5-9]\d{4,6}|1\d{5,6})$"))
            {
                MessageBox.Show("Можно вводить только число от 40000 до 1000000 в поле зарплаты");
                return false;
            }

            return true;
        }
    }
}
