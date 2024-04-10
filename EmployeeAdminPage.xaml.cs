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
using static MaterialDesignThemes.Wpf.Theme;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для EmployeeAdminPage.xaml
    /// </summary>
    public partial class EmployeeAdminPage : Page
    {
        SpecializationTableAdapter specialization = new SpecializationTableAdapter();
        EmployeeTableAdapter employee = new EmployeeTableAdapter();
        PositionTableAdapter position = new PositionTableAdapter();
        public EmployeeAdminPage()
        {
            InitializeComponent();
            Spesilization.ItemsSource = specialization.GetData();
            Spesilization.DisplayMemberPath = "Specialization";
            SalonGrid.ItemsSource = employee.GetData();
            Role.ItemsSource = position.GetData();
            Role.DisplayMemberPath = "Position";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Surname.Text) || string.IsNullOrEmpty(Name.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля Фамилия и Имя.");
                return;
            }

            if (Spesilization.SelectedItem == null || Role.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите элементы из выпадающего списка для Специализации и Должности.");
                return;
            }

            if (ValidateNameInput(Surname.Text) && ValidateNameInput(Name.Text))
            {
                if (Spesilization.SelectedItem != null && Role.SelectedItem != null)
                {
                    int spe = Convert.ToInt32((Spesilization.SelectedItem as DataRowView).Row[0]);
                    int rol = Convert.ToInt32((Role.SelectedItem as DataRowView).Row[0]);
                    employee.InsertEmployee(Surname.Text, Name.Text, MiddleName.Text, rol, spe);
                    SalonGrid.ItemsSource = employee.GetData();
                }
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SalonGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите элемент для удаления.");
                return;
            }

            if (ValidateNameInput(Surname.Text) && ValidateNameInput(Name.Text) && ValidateNameInput(MiddleName.Text))
            {
                object id = (SalonGrid.SelectedItem as DataRowView).Row[0];
                employee.DeleteEmployee(Convert.ToInt32(id));
                SalonGrid.ItemsSource = employee.GetData();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (SalonGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите элемент для изменения.");
                return;
            }

            if (ValidateNameInput(Surname.Text) && ValidateNameInput(Name.Text) && ValidateNameInput(MiddleName.Text))
            {
                if (Spesilization.SelectedItem != null && Role.SelectedItem != null)
                {
                    object id = (SalonGrid.SelectedItem as DataRowView).Row[0];
                    int spe = Convert.ToInt32((Spesilization.SelectedItem as DataRowView).Row[0]);
                    int rol = Convert.ToInt32((Role.SelectedItem as DataRowView).Row[0]);
                    employee.UpdateEmployee(Surname.Text, Name.Text, MiddleName.Text, rol, spe, Convert.ToInt32(id));
                    SalonGrid.ItemsSource = employee.GetData();
                }
            }

        }
        private bool ValidateNameInput(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return true; // Поле может быть пустым
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(name, @"^[А-ЯЁ][а-яё]*$"))
            {
                MessageBox.Show("Фамилия, имя и отчество должны содержать только русские буквы и начинаться с заглавной буквы.");
                return false;
            }
            return true;
        }
    }
}
