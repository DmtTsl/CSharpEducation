using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Task_1_2_3_MVVM
{
    public class MainWindowViewModel:INotifyPropertyChanged
    {
        private RelayCommand _changeEmployer;
        public RelayCommand ChangeEmployer
        {
            get
            {
                return _changeEmployer ??
                    (_changeEmployer = new RelayCommand(obj =>
                    {
                        Clients.CollectionChanged -= Clients_CollectionChanged;
                        Clients.Clear();
                        ClientToShow = new Client();
                        MainWindow.DataContext = null;                        
                        LogIn = new LogIn();
                        LogInViewModel.LogIn = LogIn;
                        LogIn.DataContext = LogInViewModel;
                        LogIn.ShowDialog();
                        StartWork(LogIn.DialogResult, MainWindow, LogInViewModel.SelectedEmployer);
                    }));
            }
        }
        private RelayCommand _contentRendered;
        public RelayCommand ContentRendered
        {
            get
            {
                return _contentRendered ?? (_contentRendered = new RelayCommand(obj =>
                {
                    LogIn = new LogIn();
                    LogInViewModel = new LogInViewModel(LogIn);
                    LogIn.DataContext = LogInViewModel;
                    LogIn.ShowDialog();
                    StartWork(LogIn.DialogResult, MainWindow, LogInViewModel.SelectedEmployer);
                }));
            }
        }
        private RelayCommand _selectionChanged;
        public RelayCommand SelectionChanged
        {
            get
            {
                return _selectionChanged ??
                    (_selectionChanged = new RelayCommand(obj =>
                    {
                        if (SelectedClient == null) return;
                        ClientToShow = LogInViewModel.SelectedEmployer.GetClientInformation(SelectedClient);
                        
                    }));
            }
        }
                
        private RelayCommand _addClient;
        public RelayCommand AddClient
        {
            get
            {
                return _addClient ??
                    (_addClient = new RelayCommand(obj =>
                    {
                        AddClientWindow = new AddClient();
                        AddClientViewModel = new AddClientVM(AddClientWindow);
                        AddClientWindow.DataContext = AddClientViewModel;
                        AddClientWindow.ShowDialog();
                        if (AddClientWindow.DialogResult == true)
                        {
                            AddNewClient(AddClientViewModel.NewClient);
                            Sort(Clients);
                        }
                        ClientToShow = new Client();
                    }));
            }
        }
        private RelayCommand _saveClientChanges;
        public RelayCommand SaveClientChanges
        {
            get
            {
                return _saveClientChanges ??
                  (_saveClientChanges = new RelayCommand(obj =>
                  {
                      if (ClientToShow.SecondName == "" ||
                      ClientToShow.FirstName == "" ||
                      ClientToShow.PassNumber == "")
                      {
                          MessageBox.Show("Фамилия, имя и номер паспорта не могут быть пустыми");
                      }
                      else if(!string.IsNullOrEmpty(ClientToShow.PassNumber) && ClientToShow.PassNumber.Length < 10)
                      {
                          MessageBox.Show("Номер паспорта должен состоять из 10 цифр");
                      }
                      else if (!string.IsNullOrEmpty(ClientToShow.PhoneNumber) && ClientToShow.PhoneNumber.Length < 10)
                      {
                          MessageBox.Show("Номер nелефона должен состоять из 10 цифр");
                      }
                      else
                      {
                          SelectedClient = LogInViewModel.SelectedEmployer.SetClientInformation(ClientToShow);
                          Clients[SelectedClientIndex] = SelectedClient;
                          SaveClients();
                          Sort(Clients);
                          ClientToShow = new Client();
                      }
                      
                  }, 
                  (obj) => SelectedClient != null));
            }
        }
        private RelayCommand _removeClient;
        public RelayCommand RemoveClient
        {
            get
            {
                return _removeClient ??
                    (_removeClient = new RelayCommand(obj =>
                    {
                        Clients.Remove(SelectedClient);
                        SaveClients();
                        ClientToShow = new Client();
                    },
                    obj => SelectedClient != null));
            }
        }
        public MainWindow MainWindow { get; set; }
        public LogIn LogIn { get; set; }
        public LogInViewModel LogInViewModel { get; set; }
        public AddClient AddClientWindow { get; set; }
        public AddClientVM AddClientViewModel { get; set; }
       
        public ObservableCollection<Client> Clients { get; set; }

        public Client SelectedClient { get; set; }

        public int SelectedClientIndex { get; set; }
        private Client _clientToShow;
        public Client ClientToShow 
        { 
            get 
            {
                return _clientToShow;  
            }
            set 
            {
                _clientToShow = value; OnPropertyChanged();
            } }
        
        public MainWindowViewModel()
        {
            Clients = new ObservableCollection<Client>();
            
            MainWindow = new MainWindow();
            MainWindow.DataContext = this;
            MainWindow.Show();
           
        }
        private void SaveClients()
        {
            File.WriteAllText("clients.json", JsonConvert.SerializeObject(Clients));
        }
        
        public void GetSavedClients()
        {
            if (File.Exists("clients.json"))
            {
                string jsonClients = File.ReadAllText("clients.json");
                Clients = JsonConvert.DeserializeObject<ObservableCollection<Client>>(jsonClients);
            }
        }
        private void Clients_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            string date = DateTime.Now.ToString();
            string whatChanged = null;
            string typeOfChange = null;
            string whoChanged = null;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Client Client = (Client)e.NewItems[0];
                    LogInViewModel.SelectedEmployer.GetAllChangesAddClient(Client);
                    whatChanged = String.Join("\n", LogInViewModel.SelectedEmployer.Changes);
                    typeOfChange = "Добавление";
                    whoChanged = LogInViewModel.SelectedEmployer.ToString();
                    Clients[e.NewStartingIndex].Logs.Add(new Client.Log(date, whatChanged, typeOfChange, whoChanged));
                    SelectedClientIndex = e.NewStartingIndex;
                    break;
                case NotifyCollectionChangedAction.Replace:
                    Client oldClient = (Client)e.OldItems[0];
                    Client newClient = (Client)e.NewItems[0];
                    if (oldClient == newClient) break;
                    LogInViewModel.SelectedEmployer.GetAllChangesChangeClient(oldClient, newClient);

                    whatChanged = String.Join("\n", LogInViewModel.SelectedEmployer.Changes);
                    whoChanged = LogInViewModel.SelectedEmployer.ToString();

                    typeOfChange = "Изменение";

                    Clients[e.NewStartingIndex].Logs.Add(new Client.Log(date, whatChanged, typeOfChange, whoChanged));
                    SelectedClientIndex = e.NewStartingIndex;
                    break;

            }
        }
        private void StartWork(bool? dialogResult, MainWindow window, Employer employer)
        {
            if (dialogResult == false)
            {
                window.Title = "Работа с клиентами";
                window.textBoxSecondName.IsReadOnly = true;
                window.textBoxFirstName.IsReadOnly = true;
                window.textBoxMiddleName.IsReadOnly = true;
                window.textBoxPassNumber.IsReadOnly = true;
                window.textBoxPhoneNumber.IsReadOnly = true;

                window.buttonAddClient.IsEnabled = false;
                window.buttonRemoveClient.IsEnabled = false;
                window.buttonSaveClient.IsEnabled = false;

                window.DataContext = this;
                return;
            }
            window.Title = $"Работа с клиентами: {employer}";
            if (employer is Consultant)
            {
                window.textBoxSecondName.IsReadOnly = true;
                window.textBoxFirstName.IsReadOnly = true;
                window.textBoxMiddleName.IsReadOnly = true;
                window.textBoxPassNumber.IsReadOnly = true;
                window.textBoxPhoneNumber.IsReadOnly = false;

                window.buttonAddClient.IsEnabled = false;
                window.buttonRemoveClient.IsEnabled = false;
                window.buttonSaveClient.IsEnabled = true;
            }
            else
            {
                window.textBoxSecondName.IsReadOnly = false;
                window.textBoxFirstName.IsReadOnly = false;
                window.textBoxMiddleName.IsReadOnly = false;
                window.textBoxPassNumber.IsReadOnly = false;
                window.textBoxPhoneNumber.IsReadOnly = false;

                window.buttonAddClient.IsEnabled = true;
                window.buttonRemoveClient.IsEnabled = true;
                window.buttonSaveClient.IsEnabled = true;
            }
            GetSavedClients();
            Sort(Clients);


            MainWindow.DataContext = null;
            MainWindow.DataContext = this;
            
            Clients.CollectionChanged += Clients_CollectionChanged;
        }
        public void Sort(ObservableCollection<Client> clients)
        {
        start:
            for (int i = 0; i < clients.Count - 1; i++)
            {
                if (clients[i].CompareTo(clients[i + 1]) > 0)
                {
                    clients.Move(i, i + 1);
                    goto start;
                }
            }
        }
        public void AddNewClient(Client client)
        {
            Clients.Add(client);
            SaveClients();
        }        
       
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

       
    }
}
