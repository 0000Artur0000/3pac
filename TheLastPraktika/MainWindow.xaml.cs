using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TheLastPraktika
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DropOpen(0.7);
            jojo.Children.Add(us[frmid]);

        }
        public static List<string> log = new List<string>();
        public static string name = "";
        public static int idd, id, frmid = 0;
        public static bool hasLog;
        List<UserControl> us = new List<UserControl>()
        {
            new Login(), new Main(), new UprKoef(), new SpisokIsp(), new SpZad(), new Editor()
        };
        public void DropClose(double j)
        {
            List<ThicknessAnimation> an = new List<ThicknessAnimation>()
            {
                new ThicknessAnimation(new Thickness(90,30,1186,30),new Duration(TimeSpan.FromSeconds(0.5))),
                new ThicknessAnimation(new Thickness(0,20,1070,20),new Duration(TimeSpan.FromSeconds(0.65))),
                new ThicknessAnimation(new Thickness(16,0,16,0),new Duration(TimeSpan.FromSeconds(0.35))),

            };
            DoubleAnimation anD = new DoubleAnimation(0.0, new Duration(TimeSpan.FromSeconds(0.2)));
            anD.BeginTime = TimeSpan.FromSeconds(j - 0.2);

            for (int i = 0; i < an.Count; i++)
                an[i].BeginTime = TimeSpan.FromSeconds(j);
            an[0].Completed += MainWindow_RemoveRequested;
            Scroll.BeginAnimation(MarginProperty, an[0]);
            brd.BeginAnimation(MarginProperty, an[1]);
            img.BeginAnimation(MarginProperty, an[2]);
            anD.Completed += AnD_Completed;
            jojo.BeginAnimation(OpacityProperty, anD);


        }

        private void MainWindow_RemoveRequested(object sender, EventArgs e)
        {
            DropOpen(0.3);
        }

        private void AnD_Completed(object sender, EventArgs e)
        {
            jojo.Children.Clear();
            jojo.Children.Add(us[frmid]);
            //MessageBox.Show("Проверка");
        }

        public void DropOpen(double j)
        {
            List<ThicknessAnimation> an = new List<ThicknessAnimation>()
            {
                new ThicknessAnimation(new Thickness(90,30,10,30),
                new Duration(TimeSpan.FromSeconds(0.65))),
                new ThicknessAnimation(new Thickness(20, 20, 1090, 20),
                new Duration(TimeSpan.FromSeconds(0.65))),
                new ThicknessAnimation(new Thickness(9,-10,197,-10),
                new Duration(TimeSpan.FromSeconds(0.45))),
                new ThicknessAnimation(new Thickness(90,30,788,30),
                new Duration(TimeSpan.FromSeconds(0.35))),
                new ThicknessAnimation(new Thickness(10, 20, 1080, 20),
                new Duration(TimeSpan.FromSeconds(0.35))),

            };
            DoubleAnimation anD = new DoubleAnimation(1.0,
                new Duration(TimeSpan.FromSeconds(0.2)));
            anD.BeginTime = TimeSpan.FromSeconds(j + 0.7);

            for (int i = 0; i < an.Count; i++)
                an[i].BeginTime = TimeSpan.FromSeconds(j);

            if (hasLog)
            {
                Scroll.BeginAnimation(MarginProperty, an[0]);
                brd.BeginAnimation(MarginProperty, an[1]);
            }
            else
            {
                Scroll.BeginAnimation(MarginProperty, an[3]);
                brd.BeginAnimation(MarginProperty, an[4]);
            }
            img.BeginAnimation(MarginProperty, an[2]);
            jojo.BeginAnimation(OpacityProperty, anD);
        }



        private void Border_MouseEnter_1(object sender, MouseEventArgs e)
        {
            BtnEx.Background = Brushes.DarkRed;
        }

        private void BtnEx_MouseLeave(object sender, MouseEventArgs e)
        {
            BtnEx.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0, 145, 234));
        }

        private void BtnEx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Brd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.Cursor = Cursors.ScrollAll;
                this.DragMove();

            }
        }

        private void Brd_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }


    }
}
