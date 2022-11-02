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
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Common;
using Microsoft.Win32;

namespace Task_1_2_3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MWindow : Window
    {
        public MWindow()
        {
            InitializeComponent();            
        }
        public void Information()
        {
            DataTable table = new OleDbEnumerator().GetElements();
            string inf = "";
            foreach (DataRow row in table.Rows)
                inf += row["SOURCES_NAME"] + "\n";

            MessageBox.Show(inf);
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {

            string sqlConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\d-ts\source\repos\CSharpEducation\Lesson_17\Task_1-2-3\Database1.mdf;Integrated Security=True;Asynchronous Processing=True;MultipleActiveResultSets=True;Connect Timeout=30";// @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Database1;Integrated Security=True;Connect Timeout=30";
            string sqlQuery = "SELECT * FROM Client";
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                AttachDBFilename = @"|DataDirectory|\Database1.mdf",

                IntegratedSecurity = true
            };
            using (SqlConnection sqlConnection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlQuery, sqlConnection);
                DataSet ds1 = new DataSet();
                sqlDataAdapter.Fill(ds1);
                //textBox.Text = ds1.Tables.Count.ToString();
                foreach (DataTable dataTable in ds1.Tables)
                {
                    //textBox.Text += dataTable.TableName.ToString();
                }
                DataTable dt1 = ds1.Tables[0];
                //clientDataGrid.ItemsSource = dt1.DefaultView;
            }
            string oledbQuery = "SELECT * FROM [Order]";
            OleDbConnectionStringBuilder oleDbConnectionStringBuilder = new OleDbConnectionStringBuilder()
            {
                Provider = "Microsoft.ACE.OLEDB.12.0",
                DataSource = @"|DataDirectory|\Database2.accdb",
                PersistSecurityInfo = true
            };
            MessageBox.Show(oleDbConnectionStringBuilder.ConnectionString);
            string oledbConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\d-ts\\source\\repos\\CSharpEducation\\Lesson_17\\Task_1-2-3\\Database2.accdb;Persist Security Info=True";
            using (OleDbConnection oleDbConnection = new OleDbConnection(oleDbConnectionStringBuilder.ConnectionString))
            {
                OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oledbQuery, oleDbConnection);
                DataSet ds2 = new DataSet();
                oleDbDataAdapter.Fill(ds2);
                DataTable dt2 = ds2.Tables[0];
                //orderDataGrid.ItemsSource = dt2.DefaultView;
            }





            //SqlConnection sqlConnection = new SqlConnection(connectionStringBuilder.ConnectionString);
            //sqlConnection.Open();
            //SqlCommand sqlCommand = sqlConnection.CreateCommand();
            //sqlCommand.CommandText = "SELECT * FROM Client";
            //SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            //while (sqlDataReader.Read())
            //{
            //    for (int i = 0; i < sqlDataReader.FieldCount; i++)
            //        textBox.Text += $"{sqlDataReader[i],4}|";
            //    textBox.Text += "\n";
            //}
            //sqlConnection.Close();

        }
    }
}
