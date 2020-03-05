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

namespace TheLastPraktika
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
            T1.MaxLength = 25;
            T2.MaxLength = 20;
            T1.MaxLines = 1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(T1.Text) || String.IsNullOrEmpty(T2.Password))
                MessageBox.Show("Введите данные!");
            else
            {
                if (!(new Regex(@"[.?*+^$[\]\\(){}|\`\'\""\-]").IsMatch(T2.Password) ||
                    new Regex(@"[.?*+^$[\]\\(){}|\`\'\""\-]").IsMatch(T1.Text)))
                {
                    string jo = Connect.connect(T1.Text, T2.Password);
                    if (!String.IsNullOrEmpty(jo))
                    {

                        MainWindow mw = (MainWindow)Application.Current.MainWindow;
                        MainWindow.name = jo;
                        MainWindow.frmid = 1;
                        MainWindow.hasLog = true;
                        mw.DropClose(0.4);
                        // mw.DropOpen(1.2);
                    }
                    else
                        MessageBox.Show("Неправильный логин или пароль!");
                }
                else
                {
                    MessageBox.Show("Недопустимые символы!");
                }
            }
        }
    }
}
