﻿using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class Editor : UserControl
    {
        public Editor()
        {
            InitializeComponent();
        }
        private DataSet ds = new DataSet();
        List<int> che = new List<int>();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            MainWindow.frmid = 4;
            MainWindow.hasLog = true;
            mw.DropClose(0.4);
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            load();
        }
        private void check()
        {
            if (C1.HasItems)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                    if (che[C1.SelectedIndex - 1].ToString() == dr[0].ToString())
                    {
                        T1.Text = dr[2].ToString();
                        T2.Text = dr[3].ToString();
                        int id = -1;
                        foreach (string ss in MainWindow.log)
                            if (ss == dr[1].ToString())
                                id = MainWindow.log.IndexOf(dr[1].ToString());
                        C2.SelectedIndex = id;
                        C3.SelectedIndex = dr[4].ToString() == "plan" ? 0 :
                            dr[4].ToString() == "use" ? 1 : dr[4].ToString() == "ready" ? 2 : 3;
                        C4.SelectedIndex = dr[5].ToString() == "analysis" ? 0 :
                            dr[5].ToString() == "deployment" ? 1 : 2;
                        D2.SelectedDate = DateTime.Parse(dr[7].ToString());
                        D1.SelectedDate = DateTime.Parse(dr[8].ToString());
                        T4.Text = dr[6].ToString();
                        T3.Text = dr[9].ToString();
                    }
            }
        }
        public void load()
        {
            che.Clear();
            C1.Items.Clear();
            C1.Items.Add("Новая задача");
            C2.Items.Clear();
            C3.Items.Clear();
            C3.Items.Add("Запланирована");
            C3.Items.Add("Исполняется");
            C3.Items.Add("Выполнена");
            C3.Items.Add("Отменена");
            C4.Items.Clear();
            C4.Items.Add("Анализ и проектирование");
            C4.Items.Add("Установка оборудования");
            C4.Items.Add("Техническое обслуживание и сопровождение");
            C1.SelectedIndex = 0;
            ds.Tables.Clear();
            ds.Tables.Add(Connect.read("tasks_for_executors"));
            ds.Tables.Add(Connect.read("IspolDB"));
            ds.Tables.Add(Connect.read("MenejDB"));
            ds.Tables.Add(new DataTable());

            if (MainWindow.id == 1)
            {
                MainWindow.log.Clear();
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    if (MainWindow.idd.ToString() == ds.Tables[1].Rows[i][5].ToString())
                        MainWindow.log.Add(ds.Tables[1].Rows[i][1].ToString());
            }
            postL();
            foreach (DataRow dr in ds.Tables[1].Rows)
                if(MainWindow.log.Contains(dr[1].ToString()))
                if (!(C2.Items.Contains(dr[3].ToString())))
                {

                    C2.Items.Add(dr[4].ToString());
                }
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
                        object[] dr = { ds.Tables[0].Rows[i][0], ds.Tables[0].Rows[i][2],
                            ds.Tables[0].Rows[i][4], s[0], s[1] };
                        dt.Rows.Add(dr);
                        C1.Items.Add("Задача №" + ds.Tables[0].Rows[i][0]);
                        che.Add(int.Parse(ds.Tables[0].Rows[i][0].ToString()));
                    }
            }

        }
        private void C1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (C1.SelectedIndex == 0)
            {
                Btn1_Copy1.Visibility = Visibility.Hidden;
                Gb1.Header = "Добавление записи";
                T1.Text = "";
                T2.Text = "";
                C2.SelectedIndex = -1;
                C3.SelectedIndex = -1;
                C4.SelectedIndex = -1;
                D2.SelectedDate = null;
                D1.SelectedDate = null;
                T4.Text = "";
                T3.Text = "";
            }
            else
            {
                Btn1_Copy1.Visibility = Visibility.Visible;
                Gb1.Header = "Изменение записи";
                check();
            }
        }

        private void Button_Click_save(object sender, RoutedEventArgs e)
        {
            if (C1.SelectedIndex == 0)
            {
                bool g = true;

                DataRow dr = ds.Tables[0].NewRow();
                try
                {
                    if (int.Parse(T2.Text) > 50 || int.Parse(T2.Text) < 1)
                    {
                        MessageBox.Show("Сложность может быть только от 1 до 50",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        g = false;
                    }
                    Regex rgx = new Regex(@"[?*+^$[\]\\(){}|\`\'\-]");
                    if (rgx.IsMatch(T1.Text) || rgx.IsMatch(T2.Text) ||
                        rgx.IsMatch(T3.Text) || rgx.IsMatch(T4.Text))
                    {
                        MessageBox.Show("Недопустимые символы",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        g = false;
                    }
                    if (C2.SelectedIndex == -1 || C3.SelectedIndex == -1 ||
                        C4.SelectedIndex == -1)
                    {
                        MessageBox.Show("Проверьте правильность " +
                            "заполненных данных",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        g = false;
                    }
                    foreach (DataRow drr in ds.Tables[0].Rows)
                        dr[0] = int.Parse(drr[0].ToString()) + 1;
                    foreach (DataRow drr in ds.Tables[1].Rows)
                    {
                        if (drr[4].ToString() == C2.SelectedValue.ToString())
                            dr[1] = drr[1].ToString();

                    }
                    dr[2] = T1.Text;
                    dr[3] = int.Parse(T2.Text);
                    dr[4] = C3.SelectedIndex == 0 ? "plan" : C3.SelectedIndex == 1 ?
                        "use" : C3.SelectedIndex == 2 ? "ready" : "cancel";
                    dr[5] = C4.SelectedIndex == 0 ? "analysis" : C4.SelectedIndex == 1 ?
                        "deployment" : "support";
                    dr[6] = T4.Text;
                    dr[7] = D2.SelectedDate;
                    dr[8] = D1.SelectedDate;
                    dr[9] = int.Parse(T3.Text);
                    dr[10] = DateTime.Now;
                }
                catch (Exception)
                {
                    MessageBox.Show("Проверьте правильность" +
                        " заполненных данных",
                        "Ошибка", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    g = false;
                }
                if (g)
                {
                    Connect.add(dr);
                    load();
                }
            }
            else
            {
                bool g = true;

                DataRow dr = ds.Tables[0].NewRow();
                try
                {
                    if (int.Parse(T2.Text) > 50 || int.Parse(T2.Text) < 1)
                    {
                        MessageBox.Show("Сложность может быть только от 1 до 50",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        g = false;
                    }
                    Regex rgx = new Regex(@"[?*+^$[\]\\(){}|\`\'\""\-]");
                    if (rgx.IsMatch(T1.Text) || rgx.IsMatch(T2.Text) ||
                        rgx.IsMatch(T3.Text) || rgx.IsMatch(T4.Text))
                    {
                        MessageBox.Show("Недопустимые символы",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        g = false;
                    }
                    if (C2.SelectedIndex == -1 || C3.SelectedIndex == -1 ||
                        C4.SelectedIndex == -1)
                    {
                        MessageBox.Show("Проверьте правильность " +
                            "заполненных данных",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        g = false;
                    }
                    dr[0] = che[C1.SelectedIndex - 1];
                    foreach (DataRow drr in ds.Tables[1].Rows)
                        if (drr[4].ToString() == C2.SelectedValue.ToString())
                            dr[1] = drr[1].ToString();
                    dr[2] = T1.Text;
                    dr[3] = int.Parse(T2.Text);
                    dr[4] = C3.SelectedIndex == 0 ? "plan" : C3.SelectedIndex == 1 ?
                        "use" : C3.SelectedIndex == 2 ? "ready" : "cancel";
                    dr[5] = C4.SelectedIndex == 0 ? "analysis" : C4.SelectedIndex == 1 ?
                        "deployment" : "support";
                    dr[6] = T4.Text;
                    dr[7] = D2.SelectedDate;
                    dr[8] = D1.SelectedDate;
                    dr[9] = int.Parse(T3.Text);
                    dr[10] = DateTime.Now;
                }
                catch (Exception)
                {
                    MessageBox.Show("Проверьте правильность заполненных данных",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    g = false;
                }
                if (g)
                {
                    Connect.edit(dr);
                    load();
                }

            }
        }
        private void Button_Click_del(object sender, RoutedEventArgs e)
        {
            
            if(MessageBox.Show("Вы точно хотите удалить задачу " +
                "№" + che[C1.SelectedIndex - 1] + "?", "Проверка",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            if (C1.HasItems)
            {
                Connect.del(che[C1.SelectedIndex - 1]);
                load();
            }
            else
            {
                MessageBox.Show("Ошибка", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void C2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void C3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void C4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
