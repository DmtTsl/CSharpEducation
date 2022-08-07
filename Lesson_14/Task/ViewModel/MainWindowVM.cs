using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using Newtonsoft.Json;

namespace Task
{
    public class MainWindowVM
    {
        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<Log> Logs { get; set; }
        public Log Log { get; set; }
        public Client SelectedClient { get; set; }
        public Emploee Emploee { get; set; }
        private Account _selectedAccount;
        public Account SelectedAccount 
        { 
            get => _selectedAccount;
            set
            {
                _selectedAccount = value;
                if(SelectedClient != null) SelectedClient.SelectedAccount = value;
            } 
        }       
        public decimal SumToAddTake { get; set; }
        public string Title { get; set; }
        public AddClientWindow AddClientWindow { get; set; }
        public AddClientWindowVM AddClientWindowVM { get; set; }
        public AddAccountWindow AddAccountWindow { get; set; }
        public AddAccountWindowVM AddAccountWindowVM { get; set; } 
        public TransferSumWindow TransferSumWindow { get; set; }
        public TransferSumWindowVM TransferSumWindowVM { get; set; }
        public LogListWindow LogListWindow { get; set; }
        public MainWindowVM()
        {
            Authentication authentication = new Authentication();
            
            authentication.ShowDialog();
            if ((bool)authentication.DialogResult)
            {
                AuthenticationVM authenticationVM = authentication.DataContext as AuthenticationVM;
                Emploee = new Emploee(authenticationVM.FirstName, authenticationVM.SecondName, authenticationVM.MiddleName);
                Title = "Работник: " + Emploee.ToString();
                GetClients();
                GetLogs();
                if (!File.Exists("AccNumCount.txt"))
                {
                    File.WriteAllText("AccNumCount.txt", "0");
                }
                if (!File.Exists("FreeNumber.json"))
                {
                    File.WriteAllText("FreeNumber.json", "[]");
                }
            }     
        }       
        private void GetClients()
        {
            if (File.Exists("clients.json"))
            {
                string jsonClients = File.ReadAllText("clients.json");
                JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                Clients = JsonConvert.DeserializeObject<ObservableCollection<Client>>(jsonClients,settings);
            }
            else Clients = new ObservableCollection<Client>();
        }
        private void SaveClients()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            File.WriteAllText("clients.json", JsonConvert.SerializeObject(Clients,settings));
        }
        private void GetLogs()
        {
            if (File.Exists("logs.json"))
            {
                string jsonClients = File.ReadAllText("logs.json");
                JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                Logs = JsonConvert.DeserializeObject<ObservableCollection<Log>>(jsonClients, settings);
            }
            else Logs = new ObservableCollection<Log>();
        }
        private void SaveLogs()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            File.WriteAllText("logs.json", JsonConvert.SerializeObject(Logs, settings));
        }
        private RelayCommand _addClient;
        public RelayCommand AddClient
        {
            get
            {
                return _addClient ??
                    (_addClient = new RelayCommand(obj =>
                    {
                        AddClientWindow = new AddClientWindow();
                        AddClientWindowVM = new AddClientWindowVM(AddClientWindow);
                        AddClientWindow.DataContext = AddClientWindowVM;
                        AddClientWindow.ShowDialog();
                        if (AddClientWindow.DialogResult == true)
                        {
                            Clients.Add(Client.AddClient(AddClientWindowVM.NewClient.SecondName, AddClientWindowVM.NewClient.FirstName, 
                                AddClientWindowVM.NewClient.PassportNumber, ClientLogEventHandler, AddClientWindowVM.NewClient.MiddleName, 
                                AddClientWindowVM.NewClient.PhoneNumber));
                            SaveClients();
                            Logs.Add(Log);
                            SaveLogs();
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
                        foreach (Account account in SelectedClient.Accounts)
                        {
                            if (account.AccountSum != 0)
                            {
                                MessageBox.Show($"Невозможно удалить клиента! На счете номер {account.AccountNumber:D7} есть денежные средства", 
                                    "ВНИМАНИЕ",MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }
                        }
                        foreach (Account account in SelectedClient.Accounts)
                        {
                            AccountNumberRepository.freeNumber.Add(account.AccountNumber);
                        }
                        AccountNumberRepository.SaveData();
                        Clients.Remove(Client.DeleteClient(SelectedClient, ClientLogEventHandler));
                        SaveClients();
                        Logs.Add(Log);
                        SaveLogs();
                        
                    }, (obj) => SelectedClient != null));
            }
        }
        private RelayCommand _addAccount;
        public RelayCommand AddAccount
        {
            get
            {
                return _addAccount ??
                    (_addAccount = new RelayCommand(obj =>
                    {                        
                        AddAccountWindow = new AddAccountWindow();
                        AddAccountWindowVM = new AddAccountWindowVM(AddAccountWindow,SelectedClient);
                        AddAccountWindow.DataContext = AddAccountWindowVM;
                        AddAccountWindow.ShowDialog();
                        if (AddAccountWindow.DialogResult == true)
                        {
                            SelectedClient.ClientChangedEvent += ClientLogEventHandler;
                            SelectedClient.AddAccount(AddAccountWindowVM.NewAccount);
                            SaveClients();
                            Logs.Add(Log);
                            SaveLogs();
                        }
                    },(obj) => (SelectedClient != null && SelectedClient.Accounts.Count<2)));
            }
        }
        private RelayCommand _deleteAccount;
        public RelayCommand DeleteAccount
        {
            get
            {
                return _deleteAccount ??
                    (_deleteAccount = new RelayCommand(obj =>
                    {
                        SelectedClient.ClientChangedEvent += ClientLogEventHandler;
                        if (SelectedClient.DeleteAccount(SelectedClient.SelectedAccount)) 
                        {
                            SaveClients();
                            Logs.Add(Log);
                            SaveLogs();
                        }    
                    }, (obj) => (SelectedClient != null && SelectedClient.SelectedAccount != null)));
            }
        }
        public void ClientLogEventHandler(string clientName, ClientChange clientChange, int? accountNumber = null)
        {
            Log = new ClientInfoLog(clientName, Emploee.ToString(), clientChange, accountNumber);
            MessageBox.Show(Log.Message(accountNumber));
        }
        private RelayCommand _addSum;
        public RelayCommand AddSum
        {
            get
            {
                return _addSum ??
                    (_addSum = new RelayCommand(obj =>
                    {
                        SelectedAccount.AccountChangedEvent += AccountLogEventHandler;
                        SelectedClient.SelectedAccount.AddMoney(SumToAddTake);
                        SaveClients();
                        Logs.Add(Log);
                        SaveLogs();
                    }, (obj) => SelectedClient != null && SelectedClient.SelectedAccount != null && SumToAddTake != 0));
            }
        }
        private RelayCommand _takeSum;
        public RelayCommand TakeSum
        {
            get
            {
                return _takeSum ??
                    (_takeSum = new RelayCommand(obj =>
                    {
                        SelectedAccount.AccountChangedEvent += AccountLogEventHandler;
                        if (SelectedClient.SelectedAccount.TakeMoney(SumToAddTake))
                        {
                            SaveClients();
                            Logs.Add(Log);
                            SaveLogs();
                        }
                    }, (obj) => SelectedClient != null && SelectedClient.SelectedAccount != null && SumToAddTake != 0));
            }
        }
        private RelayCommand _transferSum;
        public RelayCommand TransferSum
        {
            get
            {
                return _transferSum ??
                    (_transferSum = new RelayCommand(obj =>
                    {
                        TransferSumWindow = new TransferSumWindow();
                        TransferSumWindowVM = new TransferSumWindowVM(TransferSumWindow);
                        TransferSumWindow.DataContext = TransferSumWindowVM;
                        TransferSumWindow.ShowDialog();
                        if (TransferSumWindow.DialogResult == true)
                        {
                            SelectedAccount.AccountChangedEvent += AccountLogEventHandler;
                            if (SelectedAccount.TransferMoney(TransferSumWindowVM.AccountNumberToTransfer, TransferSumWindowVM.SumToTransfer,Clients))
                            {
                                SaveClients();
                                Logs.Add(Log);
                                SaveLogs();
                            }
                        }
                    }, (obj) => SelectedClient != null && SelectedClient.SelectedAccount != null));
            }
        }
        public void AccountLogEventHandler (AccountChange accountChange,decimal sum, int? accountToTransfer = null)
        {
            Log = new ClientAccountLog(SelectedClient.ToString(), Emploee.ToString(), SelectedAccount.AccountNumber, accountChange, sum, accountToTransfer);
            MessageBox.Show(Log.Message(accountToTransfer));
        }
        private RelayCommand _showLogs;
        public RelayCommand ShowLogs
        {
            get
            {
                return _showLogs ??
                    (_showLogs = new RelayCommand(obj =>
                    {
                        LogListWindow = new LogListWindow();
                        LogListWindow.DataContext = new LogListWindowVM(Logs);
                        LogListWindow.ShowDialog();
                    }, (obj) => Logs.Count > 0));
            }
        }
    }
}
