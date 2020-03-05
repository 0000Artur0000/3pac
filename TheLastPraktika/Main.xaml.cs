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

namespace TheLastPraktika
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : UserControl
    {
        public Main()
        {
            InitializeComponent();
              
        }
        
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            L1.Content = "Добро пожаловать,\n" +
                MainWindow.name.Replace("  "," ");
            if(MainWindow.id == 0)
            {
                Btn1.Visibility = Visibility.Hidden;
            }
        }
        private void join(int i)
        {
            MainWindow mn = (MainWindow)Application.Current.MainWindow;
            MainWindow.frmid = i;
            MainWindow.hasLog = true;
            mn.DropClose(0.4);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            join(2);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            join(4);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            join(3);
        }
    }
}
