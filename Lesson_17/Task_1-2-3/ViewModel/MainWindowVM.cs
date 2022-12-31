using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
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
        private SqlCommandBuilder _sqlCommandBuilder { get; set; }
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
        public DataTable AccessDataTable { get; set; }
        private OleDbCommandBuilder _oleDbCommandBuilder { get; set; }
        
        private DataTable _clientOrders;
        public DataTable ClientOrders
        {
            get => _clientOrders;
            set
            {
                _clientOrders = value;
                OnPropertyChanged();
            }
        }
        private DataRowView _selectedClient;
        public DataRowView SelectedClient 
        { 
            get=>_selectedClient;
            set 
            {
                if (value != null)
                {
                    _selectedClient = value;
                    GetClientOrders(value);
                }
               
            } 
        }
        
        public DataRowView SelectedOrder { get; set; }
        public Authentication Authentication { get; set; }
        public AuthenticationVM AuthenticationVM { get; set; }
        
        public MainWindowVM()
        {
            AuthenticationVM = new AuthenticationVM();
            Authentication = new Authentication();
            Authentication.DataContext = AuthenticationVM;
            authentication_show:
            Authentication.ShowDialog();
            if(Authentication.DialogResult == true)
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settingsCollection = config.ConnectionStrings.ConnectionStrings;
                var section = config.GetSection("connectionStrings");
                if (section.SectionInformation.IsProtected)
                {
                    section.SectionInformation.UnprotectSection();
                    config.Save();
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
                try
                {
                    GetSQLDataSet(_DatabasePath.GetSQlConnectionString(AuthenticationVM.Login,AuthenticationVM.Password));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Warning");
                    goto authentication_show;
                }
                
                GetAccessDataSet(_DatabasePath.GetAccessConnectionString());
                SQLDataTable = _sqlDataSet.Tables[0];
                AccessDataTable = _accessDataSet.Tables[0];
                ClientOrders = AccessDataTable.Clone();
            } 
            else App.Current.Shutdown();
        }
        private void GetSQLDataSet(string sqlStringConnection)
        {    
            _sqlDataSet = new DataSet();
            string selectQuery = "SELECT * FROM Client";
            _sqlDataAdapter = new SqlDataAdapter(selectQuery, sqlStringConnection);
            _sqlCommandBuilder = new SqlCommandBuilder(_sqlDataAdapter);
            string insertQuery = @"INSERT INTO Client (SecondName, FirstName, MiddleName, PhoneNumber, Email)
                                VALUES (@SecondName, @FirstName, @MiddleName, @PhoneNumber, @Email);
                                SET @Id = @@IDENTITY;";
            _sqlDataAdapter.InsertCommand = new SqlCommand(insertQuery);
            _sqlDataAdapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id").Direction = ParameterDirection.Output;
            _sqlDataAdapter.InsertCommand.Parameters.Add("@SecondName", SqlDbType.NVarChar, 50, "SecondName");
            _sqlDataAdapter.InsertCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50, "FirstName");
            _sqlDataAdapter.InsertCommand.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 50, "MiddleName");
            _sqlDataAdapter.InsertCommand.Parameters.Add("@PhoneNumber", SqlDbType.NChar, 10, "PhoneNumber");
            _sqlDataAdapter.InsertCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 50, "Email");

            _sqlDataAdapter.Fill(_sqlDataSet);            
        }
        private void GetAccessDataSet(string accessStringConnection)
        {   
            _accessDataSet = new DataSet();
            string selectQuery = "SELECT * FROM Orders";                        
            _accessDataAdapter = new OleDbDataAdapter(selectQuery,accessStringConnection);
            _oleDbCommandBuilder = new OleDbCommandBuilder(_accessDataAdapter);
            _accessDataAdapter.Fill(_accessDataSet);
        }
        private void GetClientOrders(DataRowView value)
        {
            var orders = from order in AccessDataTable.AsEnumerable()
                         where (string)order["Email"] == GetString(value["Email"])
                         select order;

            ClientOrders.Clear();

            foreach (var order in orders)
            {
                DataRow newRow = ClientOrders.NewRow();
                newRow["ID"] = order["ID"];
                newRow["Email"] = order["Email"];
                newRow["ProductCode"] = order["ProductCode"];
                newRow["ProductName"] = order["ProductName"];
                ClientOrders.Rows.Add(newRow);
            }
        }
        private static String GetString(Object o)
        {
            if (o == DBNull.Value) return null;
            return (string)o;
        }
        private static Int32? GetInt(Object o)
        {
            if (o == DBNull.Value) return null;
            return (int)o;
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
                            _sqlDataAdapter.Update(_sqlDataSet);
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
                        AddClientWindowVM addClientWindowVM = new AddClientWindowVM(GetString(SelectedClient[1]), 
                            GetString(SelectedClient[2]), GetString(SelectedClient[3]), GetString(SelectedClient[4]), 
                            GetString(SelectedClient[5]));
                        AddClientWindow addClientWindow = new AddClientWindow()
                        {
                            Title = "Изменение данных клиента",
                            DataContext = addClientWindowVM
                        };
                        addClientWindow.ShowDialog();
                        if(addClientWindow.DialogResult == true)
                        {
                            SelectedClient[1] = addClientWindowVM.SecondName;
                            SelectedClient[2] = addClientWindowVM.FirstName;
                            SelectedClient[3] = addClientWindowVM.MiddleName;
                            SelectedClient[4] = addClientWindowVM.PhoneNumber;
                            if (GetString(SelectedClient[5]) != addClientWindowVM.Email)
                            {
                                var orders = from order in AccessDataTable.AsEnumerable()
                                             where (string)order["Email"] == GetString(SelectedClient["Email"])
                                             select order;
                                foreach (DataRow row in orders)
                                {
                                   row["Email"] = addClientWindowVM.Email;
                                }
                                foreach(DataRow row in ClientOrders.Rows)
                                {
                                    row["Email"] = addClientWindowVM.Email;
                                }
                                SelectedClient["Email"] = addClientWindowVM.Email;
                            }                            
                            _sqlDataAdapter.Update(_sqlDataSet);
                            _accessDataAdapter.Update(_accessDataSet);
                        }                        
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
                        var orders = from order in AccessDataTable.AsEnumerable()
                                     where (string)order["Email"] == GetString(SelectedClient["Email"])
                                     select order;
                        foreach (DataRow row in orders)
                        {
                            row.Delete();
                        }
                        ClientOrders.Clear();
                        SelectedClient.Row.Delete();
                        _sqlDataAdapter.Update(_sqlDataSet);
                        _accessDataAdapter.Update(_accessDataSet);
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
                        try
                        {
                            GetSQLDataSet(_DatabasePath.GetSQlConnectionString(AuthenticationVM.Login, AuthenticationVM.Password));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Warning");
                        }
                        GetAccessDataSet(_DatabasePath.GetAccessConnectionString());
                        SQLDataTable = _sqlDataSet.Tables[0];
                        AccessDataTable = _accessDataSet.Tables[0];
                    }));
            }
        }
        private RelayCommand _addOrder;
        public RelayCommand AddOrder
        {
            get
            {
                return _addOrder ??
                    (_addOrder = new RelayCommand(obj =>
                    {
                        AddOrderWindowVM addOrderWindowVM = new AddOrderWindowVM();
                        AddOrderWindow addOrderWindow = new AddOrderWindow()
                        {                            
                            DataContext = addOrderWindowVM
                        };
                        addOrderWindow.ShowDialog();

                        if (addOrderWindow.DialogResult == true)
                        {
                            DataRow newRow = AccessDataTable.NewRow();
                            newRow[1] = GetString(SelectedClient["Email"]);
                            newRow[2] = addOrderWindowVM.ProductCode;
                            newRow[3] = addOrderWindowVM.ProductName;                            
                            AccessDataTable.Rows.Add(newRow);
                            _accessDataAdapter.Update(_accessDataSet);
                            _accessDataSet.Clear();
                            _accessDataAdapter.Fill(_accessDataSet);
                            GetClientOrders(SelectedClient);
                        }
                    }, (obj) => SelectedClient != null));
            }
        }
        private RelayCommand _deleteOrder;
        public RelayCommand DeleteOrder
        {
            get
            {
                return _deleteOrder ??
                    (_deleteOrder = new RelayCommand(obj =>
                    {                        
                        var rows = from row in AccessDataTable.AsEnumerable()
                                          where (int)row["ID"] == GetInt(SelectedOrder["ID"])
                                          select row;
                        DataRow rowToDelete = rows.First();   
                        rowToDelete.Delete();
                        SelectedOrder.Row.Delete();
                        _accessDataAdapter.Update(_accessDataSet);
                        
                    }, (obj) => SelectedOrder != null));
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
