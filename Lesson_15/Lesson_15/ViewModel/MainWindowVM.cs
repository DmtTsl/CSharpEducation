using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using LogicLibrary;


namespace Lesson_15
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
        public decimal SumToAddTake {get; set; }
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
                Clients = JsonMethods.GetJsonFileInfo<Client>("clients.json");
                Logs = JsonMethods.GetJsonFileInfo<Log>("logs.json");
                if (!File.Exists("AccNumCount.txt"))
                {
                    File.WriteAllText("AccNumCount.txt", "0");
                }
                if (!File.Exists("FreeNumber.json"))
                {
                    File.WriteAllText("FreeNumber.json", "[]");
                }
            }
            else Application.Current.Shutdown();
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
                            JsonMethods.CreateJsonFile("clients.json",Clients);
                            Logs.Add(Log);
                            JsonMethods.CreateJsonFile("logs.json", Logs);
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
                                $"Невозможно удалить клиента! На счете номер {account.AccountNumber:D7} есть денежные средства".Show();
                                return;
                            }
                        }
                        foreach (Account account in SelectedClient.Accounts)
                        {
                            AccountNumberRepository.freeNumber.Add(account.AccountNumber);
                        }
                        AccountNumberRepository.SaveData();
                        Clients.Remove(Client.DeleteClient(SelectedClient, ClientLogEventHandler));
                        JsonMethods.CreateJsonFile("clients.json", Clients);
                        Logs.Add(Log);
                        JsonMethods.CreateJsonFile("logs.json", Logs);

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
                            JsonMethods.CreateJsonFile("clients.json", Clients);
                            Logs.Add(Log);
                            JsonMethods.CreateJsonFile("logs.json", Logs);
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
                            JsonMethods.CreateJsonFile("clients.json", Clients);
                            Logs.Add(Log);
                            JsonMethods.CreateJsonFile("logs.json", Logs);
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
                        JsonMethods.CreateJsonFile("clients.json", Clients);
                        Logs.Add(Log);
                        JsonMethods.CreateJsonFile("logs.json", Logs);
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
                            JsonMethods.CreateJsonFile("clients.json", Clients);
                            Logs.Add(Log);
                            JsonMethods.CreateJsonFile("logs.json", Logs);
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
                                JsonMethods.CreateJsonFile("clients.json", Clients);
                                Logs.Add(Log);
                                JsonMethods.CreateJsonFile("logs.json", Logs);
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
