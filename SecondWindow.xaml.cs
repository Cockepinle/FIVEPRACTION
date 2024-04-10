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
    /// Логика взаимодействия для SecondWindow.xaml
    /// </summary>
    public partial class SecondWindow : Window
    {
        private MainWindow mainWindow;
        public bool isButton = false;
        public SecondWindow()
        {
            InitializeComponent();
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new ManagerAddressPage();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new ManagerProviderPage();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new ManagerProductPage();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new ManagerServicePage();
        }
        private void button4_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new ManagerActionPage();
        }
    }
}
