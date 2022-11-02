using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows;
using System.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Task_1_2_3
{
    public class MainWindowVM:INotifyPropertyChanged
    {
        private DatabasePath _DatabasePath { get; set; }
        private SqlDataAdapter _sqlDataAdapter { get; set; }
        private OleDbDataAdapter _accessDataAdapter { get; set; }
        private DataSet _sqlDataSet { get; set; }
        private DataSet _accessDataSet { get; set; }
        private DataTable _sqlDataTable;
        public DataTable SQLDataTable 
        {
            get => _sqlDataTable; 
            set 
            {
                _sqlDataTable = value;
                OnPropertyChanged();
            } 
        }
        private DataTable _accessDataTable;
        public DataTable AccessDataTable 
        { 
            get => _accessDataTable; 
            set
            {
                _accessDataTable = value;
                OnPropertyChanged();
            } 
        }
        private DataRowView _selectedRow;
        public DataRowView SelectedRow 
        { 
            get=>_selectedRow;
            set 
            {
                _selectedRow = value;
                OnPropertyChanged();
            } 
        }
        public MainWindowVM()
        {            
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settingsCollection = config.ConnectionStrings.ConnectionStrings;
            var section = config.GetSection("connectionStrings");
            if (section.SectionInformation.IsProtected)
            {
                section.SectionInformation.UnprotectSection();
            }
            if (settingsCollection.Count == 0)
            {
                _DatabasePath = new DatabasePath();
                var sqlSettings = new ConnectionStringSettings()
                {
                    Name = "SQLConnection",
                    ConnectionString = _DatabasePath.SQLConnectionString
                };
                settingsCollection.Add(sqlSettings);
                var accessConnection = new ConnectionStringSettings()
                {
                    Name = "AccessConnection",
                    ConnectionString = _DatabasePath.AccessConnectionString
                };
                settingsCollection.Add(accessConnection);
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                config.Save();
            }
            else
            {                
                _DatabasePath = new DatabasePath(settingsCollection["SQLConnection"].ConnectionString, settingsCollection["AccessConnection"].ConnectionString);
            }
            GetSQLDataSet(_DatabasePath.SQLConnectionString);
            
            SQLDataTable = _sqlDataSet.Tables[0];
            AccessDataTable = _accessDataSet.Tables[0];
            
        }
        private void GetSQLDataSet(string sqlStringConnection)
        {    
            _sqlDataSet = new DataSet();
            string selectQuery = "SELECT * FROM Client";
            _sqlDataAdapter = new SqlDataAdapter(selectQuery, sqlStringConnection);
            _sqlDataAdapter.Fill(_sqlDataSet);            
        }
        private void GetAccessDataSet(string accessStringConnection, string email)
        {   
            _accessDataSet = new DataSet();
            string selectQuery = "SELECT * FROM [Order] WHERE Email = @email";
            _accessDataAdapter = new OleDbDataAdapter(selectQuery, accessStringConnection);
            _accessDataAdapter.Fill(_accessDataSet);  
        }
        private static String GetString(Object o)
        {
            if (o == DBNull.Value) return null;
            return (String)o;
        }
        #region Commands
        private RelayCommand _addClient;
        public RelayCommand AddClient
        {
            get
            {
                return _addClient ??
                    (_addClient = new RelayCommand(obj =>
                    {
                        
                        AddClientWindowVM addClientWindowVM = new AddClientWindowVM();
                        AddClientWindow addClientWindow = new AddClientWindow()
                        {
                            Title = "Добавление клиента",
                            DataContext = addClientWindowVM
                        };
                        addClientWindow.ShowDialog();

                        if (addClientWindow.DialogResult == true)
                        {
                            DataRow newRow = SQLDataTable.NewRow();
                            newRow[1] = addClientWindowVM.SecondName;
                            newRow[2] = addClientWindowVM.FirstName;
                            newRow[3] = addClientWindowVM.MiddleName;
                            newRow[4] = addClientWindowVM.PhoneNumber;
                            newRow[5] = addClientWindowVM.Email;
                            SQLDataTable.Rows.Add(newRow);

                        }

                    }));
            }
        }
        private RelayCommand _changeClient;
        public RelayCommand ChangeClient
        {
            get
            {
                return _changeClient ??
                    (_changeClient = new RelayCommand(obj =>
                    {
                        AddClientWindowVM addClientWindowVM = new AddClientWindowVM(GetString(SelectedRow[1]), GetString(SelectedRow[2]), GetString(SelectedRow[3]), GetString(SelectedRow[4]), GetString(SelectedRow[5]));
                        AddClientWindow addClientWindow = new AddClientWindow()
                        {
                            Title = "Изменение данных клиента",
                            DataContext = addClientWindowVM
                        };
                        addClientWindow.ShowDialog();
                        if(addClientWindow.DialogResult == true)
                        {
                            SelectedRow[1] = addClientWindowVM.SecondName;
                            SelectedRow[2] = addClientWindowVM.FirstName;
                            SelectedRow[3] = addClientWindowVM.MiddleName;
                            SelectedRow[4] = addClientWindowVM.PhoneNumber;
                            SelectedRow[5] = addClientWindowVM.Email;
                        }
                        //MessageBox.Show(SelectedRow.GetType().Name);
                    }));
            }
        }
        private RelayCommand _deleteClient;
        public RelayCommand DeleteClient
        {
            get
            {
                return _deleteClient ??
                    (_deleteClient = new RelayCommand(obj =>
                    {
                        SelectedRow.Row.Delete();
                    }));
            }
        }
        private RelayCommand _save;
        public RelayCommand Save
        {
            get
            {
                return _save ??
                    (_save = new RelayCommand(obj =>
                    {
                        SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(_sqlDataAdapter);
                        MessageBox.Show(sqlCommandBuilder.GetUpdateCommand().CommandText);
                        _sqlDataAdapter.Update(_sqlDataSet);
                        _sqlDataSet.Clear();
                        _sqlDataAdapter.Fill(_sqlDataSet);

                    }));
            }
        }
        private RelayCommand _changeDatabaseLocation;
        public RelayCommand ChangeDatabaseLocation
        {
            get
            {
                return _changeDatabaseLocation ??
                    (_changeDatabaseLocation = new RelayCommand(obj =>
                    {
                        var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        var settingsCollection = config.ConnectionStrings.ConnectionStrings;
                        var section = config.GetSection("connectionStrings");

                        if (section.SectionInformation.IsProtected)
                        {
                            section.SectionInformation.UnprotectSection();
                        }
                        settingsCollection.Clear();
                        _DatabasePath = new DatabasePath();
                        var sqlSettings = new ConnectionStringSettings()
                        {
                            Name = "SQLConnection",
                            ConnectionString = _DatabasePath.SQLConnectionString
                        };
                        settingsCollection.Add(sqlSettings);
                        var accessConnection = new ConnectionStringSettings()
                        {
                            Name = "AccessConnection",
                            ConnectionString = _DatabasePath.AccessConnectionString
                        };
                        settingsCollection.Add(accessConnection);
                        section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                        config.Save();
                        GetSQLDataSet(_DatabasePath.SQLConnectionString);
                        GetAccessDataSet(_DatabasePath.AccessConnectionString);
                        SQLDataTable = _sqlDataSet.Tables[0];
                        AccessDataTable = _accessDataSet.Tables[0];
                    }));
            }
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs (prop));
        }
    }
}
