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
using Microsoft.Xaml.Behaviors.Layout;
using Salon.BeautySalonDataSetTableAdapters;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для DateAdminPage.xaml
    /// </summary>
    public partial class DateAdminPage : Page
    {
        AccountsTableAdapter accounts = new AccountsTableAdapter();
        EmployeeTableAdapter employee = new EmployeeTableAdapter();
        public DateAdminPage()
        {
            InitializeComponent();
            SalonGrid.ItemsSource = accounts.GetData();
            EmployeeBox.ItemsSource = employee.GetData();
            EmployeeBox.DisplayMemberPath = "Surname_Employee";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите сотрудника.");
                return;
            }

            string login = Login.Text;
            string password = PasswordTbx.Password;

            if (!Regex.IsMatch(login, "^(?=.*[A-Z])(?=.*\\d)[a-zA-Z0-9]{1,10}$") ||
                !Regex.IsMatch(password, "^(?=.*[A-Z])(?=.*\\d)[a-zA-Z0-9]{1,10}$"))
            {
                MessageBox.Show("Логин должен содержать хотя бы одну заглавную букву, пароль должен содержать хотя бы одну заглавную букву и одну цифру, и оба не должны превышать 10 символов.");
                return;
            }

            if (CheckIfLoginExists(login))
            {
                MessageBox.Show("Логин уже существует. Пожалуйста, выберите другой логин.");
                return;
            }

            int emp = (int)(EmployeeBox.SelectedItem as DataRowView).Row[0];
            accounts.InsertAccounts(login, password, emp);
            SalonGrid.ItemsSource = accounts.GetData();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SalonGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите элемент для удаления.");
                return;
            }

            object id = (SalonGrid.SelectedItem as DataRowView).Row[0];
            accounts.DeleteAccounts(Convert.ToInt32(id));
            SalonGrid.ItemsSource = accounts.GetData();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (SalonGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите элемент для изменения.");
                return;
            }

            string login = Login.Text;
            string password = PasswordTbx.Password;

            if (!Regex.IsMatch(login, "^(?=.*[A-Z])(?=.*\\d)[a-zA-Z0-9]{1,10}$") ||
                !Regex.IsMatch(password, "^(?=.*[A-Z])(?=.*\\d)[a-zA-Z0-9]{1,10}$"))
            {
                MessageBox.Show("Логин должен содержать хотя бы одну заглавную букву, пароль должен содержать хотя бы одну заглавную букву и одну цифру, и оба не должны превышать 10 символов.");
                return;
            }

            if (CheckIfLoginExists(login) && login != (SalonGrid.SelectedItem as DataRowView).Row["Login_Accounts"].ToString())
            {
                MessageBox.Show("Логин уже существует. Пожалуйста, выберите другой логин.");
                return;
            }

            object id = (SalonGrid.SelectedItem as DataRowView).Row[0];
            int emp = (int)(EmployeeBox.SelectedItem as DataRowView).Row[0];
            accounts.UpdateAccounts(login, password, emp, Convert.ToInt32(id));
            SalonGrid.ItemsSource = accounts.GetData();
        }
        private bool CheckIfLoginExists(string login)
        {
            foreach (var item in SalonGrid.Items)
            {
                if (item is DataRowView row)
                {
                    string existingLogin = row["Login_Accounts"].ToString();
                    if (existingLogin == login)
                    {
                        return true; 
                    }
                }
            }
            return false;
        }
    }
}
