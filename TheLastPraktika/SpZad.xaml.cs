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
    /// Логика взаимодействия для SpZad.xaml
    /// </summary>
    public partial class SpZad : UserControl
    {
        public SpZad()
        {
            InitializeComponent();
        }
        private DataSet ds = new DataSet();
        private void preload()
        {
            C1.Items.Clear();
            C2.Items.Clear();
            C1.Items.Add("Все");
            C1.Items.Add("plan");
            C1.Items.Add("ready");
            C2.Items.Add("Все");
            C1.SelectedIndex = 0;
            C2.SelectedIndex = 0;
            if (MainWindow.id == 0)
            {
                L2.Visibility = Visibility.Hidden;
                C2.Visibility = Visibility.Hidden;
            }
        }
        public void load()
        {

            Gr.ItemsSource = null;
            ds.Tables.Clear();
            ds.Tables.Add(Connect.read("tasks_for_executors"));
            ds.Tables.Add(Connect.read("IspolDB"));
            ds.Tables.Add(Connect.read("MenejDB"));
            ds.Tables.Add(new DataTable());
            preload();
            if (MainWindow.id == 1)
            {
                MainWindow.log.Clear();
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    if (MainWindow.idd.ToString() == ds.Tables[1].Rows[i][5].ToString())
                        MainWindow.log.Add(ds.Tables[1].Rows[i][1].ToString());
            }
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                C2.Items.Add(ds.Tables[1].Rows[i][4].ToString());
            postL();
            Gr.ItemsSource = ds.Tables[3].AsDataView();
        }
        private void postL()
        {
            DataTable dt = ds.Tables[3];
            dt.Columns.Clear();
            dt.Rows.Clear();
            dt.Columns.Add(new DataColumn("№ задачи"));
            dt.Columns.Add(new DataColumn("Заголовок задачи"));
            dt.Columns.Add(new DataColumn("Статус задачи"));
            dt.Columns.Add(new DataColumn("ФИО исполнителя"));
            dt.Columns.Add(new DataColumn("ФИО менеджера"));
            for (int k = 0; k < MainWindow.log.Count; k++)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    if (ds.Tables[0].Rows[i][1].ToString() == MainWindow.log[k])
                    {
                        string[] s = new string[2];
                        for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                            if (ds.Tables[1].Rows[j][1].ToString() == ds.Tables[0].Rows[i][1].ToString())
                            {
                                s[0] = ds.Tables[1].Rows[j][4].ToString();
                                for (int h = 0; h < ds.Tables[2].Rows.Count; h++)
                                    if (ds.Tables[2].Rows[h][0].ToString() == ds.Tables[1].Rows[j][5].ToString())
                                        s[1] = ds.Tables[2].Rows[h][3].ToString();
                            }
                        if (C1.SelectedIndex < 1)
                        {
                            if (C2.SelectedIndex < 1)
                            {
                                object[] dr = { ds.Tables[0].Rows[i][0], ds.Tables[0].Rows[i][2], ds.Tables[0].Rows[i][4], s[0], s[1] };
                                dt.Rows.Add(dr);
                            }
                            else
                            {
                                for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                                    if (C2.SelectedValue.ToString() == ds.Tables[1].Rows[j][4].ToString() && ds.Tables[1].Rows[j][1].ToString() == ds.Tables[0].Rows[i][1].ToString())
                                    {

                                        object[] dr = { ds.Tables[0].Rows[i][0], ds.Tables[0].Rows[i][2], ds.Tables[0].Rows[i][4], s[0], s[1] };
                                        dt.Rows.Add(dr);
                                    }
                            }
                        }
                        else
                        {
                            if (C1.SelectedValue.ToString() == ds.Tables[0].Rows[i][4].ToString())
                            {
                                if (C2.SelectedIndex < 1)
                                {
                                    object[] dr = { ds.Tables[0].Rows[i][0], ds.Tables[0].Rows[i][2], ds.Tables[0].Rows[i][4], s[0], s[1] };
                                    dt.Rows.Add(dr);
                                }
                                else
                                {
                                    for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                                        if (C2.SelectedValue.ToString() == ds.Tables[1].Rows[j][4].ToString() && ds.Tables[1].Rows[j][1].ToString() == ds.Tables[0].Rows[i][1].ToString())
                                        {

                                            object[] dr = { ds.Tables[0].Rows[i][0], ds.Tables[0].Rows[i][2], ds.Tables[0].Rows[i][4], s[0], s[1] };
                                            dt.Rows.Add(dr);
                                        }
                                }
                            }
                        }

                    }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            MainWindow.frmid = 1;
            MainWindow.hasLog = true;
            mw.DropClose(0.4);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            load();
        }

        private void C2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            postL();
        }

        private void C1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            postL();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            MainWindow.frmid = 5;
            MainWindow.hasLog = true;
            mw.DropClose(0.4);
        }
    }
}
