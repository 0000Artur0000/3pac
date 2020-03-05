using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для UprKoef.xaml
    /// </summary>
    public partial class UprKoef : UserControl
    {
        public UprKoef()
        {
            InitializeComponent();
        }
        private DataSet ds = new DataSet();
        private List<TextBox> tb = new List<TextBox>();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            MainWindow.frmid = 1;
            MainWindow.hasLog = true;
            mw.DropClose(0.4);
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            DataRow dr = ds.Tables[0].NewRow();
            bool doDoHasDoHasMesh = true;
            try
            {
                if (Double.Parse(T4.Text) < 0.0 || Double.Parse(T4.Text) > 1.0 )
                {
                    MessageBox.Show("'Анализ и проектирование' может быть только от 0 до 1",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    doDoHasDoHasMesh = false;
                }
                if (Double.Parse(T5.Text) < 0.0 || Double.Parse(T5.Text) > 1.0)
                {
                    MessageBox.Show("'Установка оборудования' может быть только от 0 до 1",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    doDoHasDoHasMesh = false;
                }
                if (Double.Parse(T6.Text) < 0.0 || Double.Parse(T6.Text) > 1.0)
                {
                    MessageBox.Show("'Техническое обслуживание и сопровождение' может быть" +
                        " только от 0 до 1", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    doDoHasDoHasMesh = false;
                }

                dr[0] = MainWindow.idd;
                for (int i = 0; i < tb.Count; i++)
                {
                    dr[i + 1] = Double.Parse(tb[i].Text.Replace(',', '.'));
                }

            }
            catch (Exception)
            {
                doDoHasDoHasMesh = false;
                MessageBox.Show("Проверьте правильность данных", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);

            }
            //MessageBox.Show(dr[0] + ", " + dr[1] + ", " + dr[2] + ", " + dr[3] + ", " + dr[4] + ", " + dr[5] + ", " + dr[6] + ", " + dr[7] + ", " + dr[8] + ", " + dr[9]);
            if (doDoHasDoHasMesh)
            {
                Connect.load(dr);
                loaddb();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            tb.Clear();
            tb.Add(T1); tb.Add(T2); tb.Add(T3);
            tb.Add(T4); tb.Add(T5); tb.Add(T6);
            tb.Add(T7); tb.Add(T8); tb.Add(T9);
            for (int i = 0; i < tb.Count; i++)
            {
                tb[i].MaxLength = 10;
                tb[i].MaxLines = 1;
            }
            loaddb();
        }
        private void loaddb()
        {


            ds.Tables.Clear();
            ds.Tables.Add(Connect.read("RabDB"));
            TextUpdate();
        }
        private void TextUpdate()
        {

            System.Threading.Thread.CurrentThread.CurrentCulture =
                new System.Globalization.CultureInfo("en-US");

            for (int i = 0; i < tb.Count; i++)
            {
                tb[i].Text = Math.Round(Double.Parse(ds.Tables[0].Rows[MainWindow.idd - 1][i + 1].ToString()), 2).ToString();
            }

        }
    }
}
