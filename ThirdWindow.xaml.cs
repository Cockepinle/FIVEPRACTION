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
    /// Логика взаимодействия для ThirdWindow.xaml
    /// </summary>
    public partial class ThirdWindow : Window
    {
        private MainWindow mainWindow;
        public bool isButton = false;
        public ThirdWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new MyBuyPage();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new InformationAboutCheckPage();
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
