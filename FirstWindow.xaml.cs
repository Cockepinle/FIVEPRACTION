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
using System.Windows.Shapes;
using static MaterialDesignThemes.Wpf.Theme;

namespace Salon
{
    /// <summary>
    /// Логика взаимодействия для FirstWindow.xaml
    /// </summary>
    public partial class FirstWindow : Window
    {
        private MainWindow mainWindow; 
        public bool isButton = false;
        public FirstWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new RoleAdminPage();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new EmployeeAdminPage();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new DateAdminPage();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            isButton = true;
            if (isButton)
            {
                mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }

        }
    }
}
