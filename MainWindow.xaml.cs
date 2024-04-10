using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AccountsTableAdapter accounts = new AccountsTableAdapter();
        FirstWindow firstWindow = new FirstWindow();
        SecondWindow secondWindow = new SecondWindow();
        ThirdWindow thirdWindow = new ThirdWindow();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var allLogins = accounts.GetData().Rows;
            bool foundUser = false;
            for (int i = 0; i < allLogins.Count; i++)
            {
                if (allLogins[i][1].ToString() == LoginTbx.Text &&
                    allLogins[i][2].ToString() == PasswordTbx.Password)
                {
                    foundUser = true;
                    if (!IsInputValid(LoginTbx.Text, PasswordTbx.Password))
                    {
                        MessageBox.Show("Некорректный ввод. Пожалуйста, используйте только латинские буквы и цифры.");
                        return;
                    }
                    int roleId = (int)allLogins[i][3];
                    switch (roleId)
                    {
                        case 1:
                            firstWindow.Show();
                            this.Close();
                            break;
                        case 2:
                            secondWindow.Show();
                            this.Close();
                            break;
                        case 3:
                            thirdWindow.Show();
                            this.Close();
                            break;
                    }
                }
            }
            if (!foundUser)
            {
                MessageBox.Show("Пользователь с таким логином и паролем не существует.");
            }
        }
        private bool IsInputValid(string login, string password)
        {
            // Проверка на наличие только латинских букв и цифр
            string pattern = "^[a-zA-Z0-9]*$";
            if (!Regex.IsMatch(login, pattern) || !Regex.IsMatch(password, pattern))
            {
                return false;
            }
            return true;
        }
    }
}
