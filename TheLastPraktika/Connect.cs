using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TheLastPraktika
{
    class Connect
    {
        private static SqlConnectionStringBuilder connS = new SqlConnectionStringBuilder()
        {
            DataSource = "303-2\\SQLEXPRESS",
            //DataSource = "DESKTOP-U5HC5KL",
            InitialCatalog = "Ver4",
            IntegratedSecurity = true
        };
        public static string connect(string log, string pass)
        {
            using (SqlConnection conn = new SqlConnection(connS.ConnectionString))
            {
                string b = "";
                conn.Open();
                string d = $"SELECT * FROM dbo.IspolDB WHERE '{log}' = Логин_исполнителя AND '{pass}' = Пароль_исполнителя";
                string d1 = $"SELECT * FROM dbo.MenejDB WHERE '{log}' = Логин_менеджера AND '{pass}' = Пароль_менеджера";
                SqlCommand cmd = new SqlCommand(d, conn);
                SqlDataReader sql = cmd.ExecuteReader();
                if (sql.HasRows)
                {
                    sql.Read();
                    MainWindow.idd = Int16.Parse(sql[0].ToString());
                    MainWindow.id = 0;
                    MainWindow.log.Clear();
                    MainWindow.log.Add(sql[1].ToString());
                    b = sql[4].ToString();
                }
                else
                {
                    sql.Close();
                    cmd = new SqlCommand(d1, conn);
                    sql = cmd.ExecuteReader();
                    if (sql.HasRows)
                    {
                        sql.Read();
                        MainWindow.idd = Int16.Parse(sql[0].ToString());
                        MainWindow.id = 1;
                        MainWindow.log.Clear();
                        MainWindow.log.Add(sql[1].ToString());
                        b = sql[3].ToString();
                    }
                }
                sql.Close();
                conn.Close();
                if (!String.IsNullOrEmpty(b)) return b;
                return "";
            }
        }
        public static DataTable read(string name)
        {
            using (SqlConnection conn = new SqlConnection(connS.ConnectionString))
            {
                DataTable dt = new DataTable();
                conn.Open();
                string d = $"Select * From {name}";
                SqlCommand cmd = new SqlCommand(d, conn);
                using (SqlDataAdapter ada = new SqlDataAdapter(cmd))
                {
                    dt.Clear();
                    ada.Fill(dt);

                }

                conn.Close();
                return dt;
            }
        }
        public static void load(DataRow name)
        {
            using (SqlConnection conn = new SqlConnection(connS.ConnectionString))
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                DataTable dt = new DataTable();
                conn.Open();
                string d = $"UPDATE RabDB Set [Junior_мин_ЗП] = '{name[1]}'," +
                    $" [Middle_мин_ЗП] = '{name[2]}', [Senior_мин_ЗП] = '{name[3]}'," +
                    $" [Коэффициент_для_Анализ_и_проектирование]= '{name[4]}'," +
                    $" [Коэффициент_для_Установка_оборудования] = '{name[5]}', " +
                    $"[Коэффициент_для_Техническое_обслуживание_и_сопровождение] = '{name[6]}', " +
                    $"[Коэффициент_времени] = '{name[7]}', [Коэффициент_сложности] = '{name[8]}'," +
                    $" [Коэффициент_для_перевода_в_денежный_эквивалент] = '{name[9]}' WHERE Id_d = '{name[0]}'";
                new SqlCommand(d, conn).ExecuteNonQuery();
                MessageBox.Show("Успешно!");
                conn.Close();

            }
        }
        public static void edit(DataRow name)
        {
            using (SqlConnection conn = new SqlConnection(connS.ConnectionString))
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                DataTable dt = new DataTable();
                conn.Open();
                string d = $"UPDATE tasks_for_executors Set [Логин_исполнителя] = '{name[1]}'," +
                    $" [Заголовок] = '{name[2]}', [Сложность] = '{name[3]}'," +
                    $" [Статус]= '{name[4]}'," +
                    $" [Характер_работы] = '{name[5]}' WHERE id_rab = '{name[0]}'";
                new SqlCommand(d, conn).ExecuteNonQuery();
                
                conn.Close();
                MessageBox.Show("Успешно!");

            }
        }
        public static void add(DataRow name)
        {
            using (SqlConnection conn = new SqlConnection(connS.ConnectionString))
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                DataTable dt = new DataTable();
                conn.Open();
                string d = $"Insert Into tasks_for_executors ([id_rab], [Логин_исполнителя], [Заголовок], [Сложность], [Статус], [Характер_работы]) VALUES ('{name[0]}', '{name[1]}', '{name[2]}', '{name[3]}', '{name[4]}', '{name[5]}')";
                new SqlCommand(d, conn).ExecuteNonQuery();

                conn.Close();
                MessageBox.Show("Успешно!");

            }
        }
        public static void del(int id)
        {
            using (SqlConnection conn = new SqlConnection(connS.ConnectionString))
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                DataTable dt = new DataTable();
                conn.Open();
                string d = $"Delete tasks_for_executors Where id_rab = '{id}'";
                new SqlCommand(d, conn).ExecuteNonQuery();

                conn.Close();
                MessageBox.Show("Успешно!");

            }
        }
    }
}
