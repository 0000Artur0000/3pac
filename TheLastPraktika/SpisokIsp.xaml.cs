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
    /// Логика взаимодействия для SpisokIsp.xaml
    /// </summary>
    public partial class SpisokIsp : UserControl
    {
        public SpisokIsp()
        {
            InitializeComponent();
        }
        private DataSet ds = new DataSet();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            MainWindow.frmid = 1;
            MainWindow.hasLog = true;
            mw.DropClose(0.4);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Gr.ItemsSource = null;
            ds.Tables.Clear();
            ds.Tables.Add(Connect.read("IspolDB"));
            ds.Tables.Add(Connect.read("MenejDB"));
            ds.Tables.Add(new DataTable());

            ds.Tables[2].Columns.Add(new DataColumn("ФИО исполнителя"));
            ds.Tables[2].Columns.Add(new DataColumn("Грейд"));
            ds.Tables[2].Columns.Add(new DataColumn("ФИО менеджера"));
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string s = "";
                foreach (DataRow drs in ds.Tables[1].Rows)
                {
                    if (drs[0].ToString() == dr[5].ToString())
                        s = drs[3].ToString();
                }
                object[] ob = { dr[4], dr[3], s };
                ds.Tables[2].Rows.Add(ob);
            }
            Gr.ItemsSource = ds.Tables[2].AsDataView();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
