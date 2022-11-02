using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Task_1_2_3
{
    public class DatabasePath
    {
        private SqlConnectionStringBuilder _SqlConnectionStringBuilder { get; set; }
        private OleDbConnectionStringBuilder _AccessConnectionStringBuilder { get; set; }
        private string _default_SQL_ClientDatabasePath = @"|DataDirectory|\Database1.mdf";
        private string _default_Access_OrderDatabasePath = @"|DataDirectory|\Database2.accdb";
        public string SQLConnectionString { get => _SqlConnectionStringBuilder.ConnectionString; }
        public string AccessConnectionString { get => _AccessConnectionStringBuilder.ConnectionString; }

        public DatabasePath() 
        {
            MessageBox.Show("Choose SQL Database file (.mdf) or press cancel to set Database file location in program folder with name \"Database1.mdf\"");
            OpenFileDialog SQL_openFileDialog = new OpenFileDialog();
            SQL_openFileDialog.Filter = "SQL Database Files (.mdf)|*.mdf";
            SQL_openFileDialog.Title = "Choose SQL Database file (.mdf)";
            if (SQL_openFileDialog.ShowDialog() == true)
            {
                _SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
                {
                    DataSource = @"(LocalDB)\MSSQLLocalDB",
                    AttachDBFilename = SQL_openFileDialog.FileName,
                    IntegratedSecurity = true
                };
            }
            else 
            {
                _SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
                {
                    DataSource = @"(LocalDB)\MSSQLLocalDB",
                    AttachDBFilename = _default_SQL_ClientDatabasePath,
                    IntegratedSecurity = true
                };
            }
            MessageBox.Show("Choose OleDB Database file (.accdb) or press cancel to set Database file location in program folder with name \"Database2.accdb\"");
            OpenFileDialog OleDB_openFileDialog = new OpenFileDialog();
            OleDB_openFileDialog.Filter = "OleDB Database Files (.accdb)|*.accdb";
            OleDB_openFileDialog.Title = "Choose Access Database file (.accdb)";
            if (OleDB_openFileDialog.ShowDialog() == true)
            {
                _AccessConnectionStringBuilder = new OleDbConnectionStringBuilder()
                {
                    Provider = "Microsoft.ACE.OLEDB.12.0",
                    DataSource = OleDB_openFileDialog.FileName,
                    PersistSecurityInfo = true
                };
            }
            else
            {
                _AccessConnectionStringBuilder = new OleDbConnectionStringBuilder()
                {
                    Provider = "Microsoft.ACE.OLEDB.12.0",
                    DataSource = _default_Access_OrderDatabasePath,
                    PersistSecurityInfo = true
                };
            }
        }
        public DatabasePath(string sQL_ClientDatabasePath, string access_OrderDatabasePath)
        { 
            _SqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            _SqlConnectionStringBuilder.ConnectionString = sQL_ClientDatabasePath;
            _AccessConnectionStringBuilder = new OleDbConnectionStringBuilder();
            _AccessConnectionStringBuilder.ConnectionString = access_OrderDatabasePath;
        }
        
    }
}
