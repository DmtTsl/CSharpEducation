using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;



namespace Task_1_2_3
{
    public class MainWindowVM
    {
        public ObservableCollection<Client> Clients { get; set; }
        
        public Client SelectedClient { get; set; }
        public Account SelectedAccount { get; set; }       
        public decimal SumToAddTake { get; set; }
        public AddClientWindow AddClientWindow { get; set; }
        public AddClientWindowVM AddClientWindowVM { get; set; }
        public AddAccountWindow AddAccountWindow { get; set; }
        public AddAccountWindowVM AddAccountWindowVM { get; set; } 
        public AccountTempList AccountTempList { get; set; }
        public TransferSumWindow TransferSumWindow { get; set; }
        public TransferSumWindowVM TransferSumWindowVM { get; set; }

        public MainWindowVM()
        {
            GetClients();
            
            if (!File.Exists("AccNumCount.txt"))
            {
                File.WriteAllText("AccNumCount.txt", "0");
            }
            if (!File.Exists("FreeNumber.json"))
            {
                File.WriteAllText("FreeNumber.json", "[]");
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
        private void AddNewClient(Client client)
        {
            Clients.Add(client);
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
                            AddNewClient(new Client(AddClientWindowVM.NewClient.SecondName, AddClientWindowVM.NewClient.FirstName, 
                                AddClientWindowVM.NewClient.PassportNumber, AddClientWindowVM.NewClient.MiddleName, 
                                AddClientWindowVM.NewClient.PhoneNumber));
                            SaveClients();
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
                        Clients.Remove(SelectedClient);
                        SaveClients();
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
                            SelectedClient.Accounts.Add(AddAccountWindowVM.NewAccount);
                            if (AddAccountWindowVM.NewAccount is PaymentAccount) SelectedClient.HasPaymentAcc = true;
                            else SelectedClient.HasDepositAcc = true;
                            SaveClients();
                            
                        }
                    },(obj)=>SelectedClient!=null && SelectedClient.Accounts.Count<2));
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
                        if (SelectedAccount.AccountSum != 0)
                        {
                            MessageBox.Show($"Невозможно закрыть счет! На счете номер {SelectedAccount.AccountNumber:D7} есть денежные средства",
                                "ВНИМАНИЕ", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        if (SelectedAccount is PaymentAccount) SelectedClient.HasPaymentAcc = false;
                        else SelectedClient.HasDepositAcc = false;
                        AccountNumberRepository.freeNumber.Add(SelectedAccount.AccountNumber);
                        AccountNumberRepository.SaveData();
                        
                        SelectedClient.Accounts.Remove(SelectedAccount);
                        SaveClients();
                    }, (obj) => SelectedAccount != null));
            }
        }
        private RelayCommand _addSum;
        public RelayCommand AddSum
        {
            get
            {
                return _addSum ??
                    (_addSum = new RelayCommand(obj =>
                    {
                        SelectedAccount.AddMoney(SumToAddTake);
                        SaveClients();
                        
                    }, (obj) => SelectedAccount != null && SumToAddTake != 0));
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
                        if (SelectedAccount.AccountSum >= SumToAddTake)
                        {
                            SelectedAccount.TakeMoney(SumToAddTake);
                            SaveClients();
                        }
                        else MessageBox.Show($"Снятие средств невозможно! На счете номер {SelectedAccount.AccountNumber:D7} недостаточно средств",
                                    "ВНИМАНИЕ", MessageBoxButton.OK, MessageBoxImage.Warning);

                    }, (obj) => SelectedAccount != null && SumToAddTake != 0));
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
                            if (SelectedAccount.AccountSum >= TransferSumWindowVM.SumToTransfer)
                            {
                                AccountTempList = new AccountTempList(Clients);
                                foreach (Account account in AccountTempList.AccountList)
                                {
                                    if (account.AccountNumber == TransferSumWindowVM.AccountNumberToTransfer)
                                    {
                                        account.AccountSum += TransferSumWindowVM.SumToTransfer;
                                        SelectedAccount.AccountSum -= TransferSumWindowVM.SumToTransfer;
                                        SaveClients();
                                    }
                                }
                            }
                            else MessageBox.Show($"Перевод невозможен! На счете номер {SelectedAccount.AccountNumber:D7} недостаточно средств",
                                    "ВНИМАНИЕ", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                    }, (obj) => SelectedAccount != null));
            }
        }
    }
}
