using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Логика взаимодействия для InformationAboutCheckPage.xaml
    /// </summary>
    public partial class InformationAboutCheckPage : Page
    {
        OrdersTableAdapter orders = new OrdersTableAdapter();
        EmployeeTableAdapter employee = new EmployeeTableAdapter();
        StudioAddressTableAdapter studioAddress = new StudioAddressTableAdapter();  
        Certificate_UserTableAdapter certificate = new Certificate_UserTableAdapter();
        public InformationAboutCheckPage()
        {
            InitializeComponent();
            Pro.ItemsSource = orders.GetData();
            Pro.DisplayMemberPath = "DateTime_Orders";
            SavedCheckGrid.ItemsSource = orders.GetData();
        }
        private void Pro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)Pro.SelectedItem;
            if (selectedRow != null)
            {
                // Получение данных о выбранном чеке
                string orderId = selectedRow["DateTime_Orders"].ToString();
                string orderDate = selectedRow["Amount_Orders"].ToString();
                string orderTotal = selectedRow["Contribution"].ToString();
                string sdahaa = selectedRow["Change_Orders"].ToString();
                string cer = selectedRow["Certificate_User_ID"].ToString();
                string emp = selectedRow["Employee_ID"].ToString();
                string adr = selectedRow["StudioAddress_ID"].ToString();

                Data.Text = orderId;
                Summ.Text = orderDate;
                Vznos.Text = orderTotal.ToString();
                Sdaha.Text = sdahaa.ToString();
                Certificate.Text = cer;
                Employee.Text = emp;
                Address.Text = adr;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Получение выбранного чека из списка
            DataRowView selectedRow = (DataRowView)Pro.SelectedItem;
            if (selectedRow != null)
            {
                // Получение данных о выбранном чеке
                DateTime orderDateTime = Convert.ToDateTime(selectedRow["DateTime_Orders"]);
                string orderId = orderDateTime.ToString("yyyy-MM-dd HH:mm:ss"); // Use the desired date format
                string orderDate = selectedRow["Amount_Orders"].ToString();
                string orderTotal = selectedRow["Contribution"].ToString();
                string sdahaa = selectedRow["Change_Orders"].ToString();
                string cer = selectedRow["Certificate_User_ID"].ToString();
                string emp = selectedRow["Employee_ID"].ToString();
                string adr = selectedRow["StudioAddress_ID"].ToString();

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = System.IO.Path.Combine(desktopPath, "SavedCheck.txt");
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"Дата чека: {orderId}");
                    writer.WriteLine($"Сумма чека: {orderDate}");
                    writer.WriteLine($"Внесенная сумма: {orderTotal}");
                    writer.WriteLine($"Сдача: {sdahaa}");
                    writer.WriteLine($"Сертификат: {cer}");
                    writer.WriteLine($"Сотрудник: {emp}");
                    writer.WriteLine($"Адрес: {adr}");
                    // Добавьте другие данные чека, если необходимо
                }

                MessageBox.Show("Чек успешно выгружен в файл SavedCheck.txt");
            }
        }
    }
}
