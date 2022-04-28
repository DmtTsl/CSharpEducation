using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace Lesson_11
{
        
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Repository _repository;
        private Employer Employer;
        
        public MainWindow()
        {
            InitializeComponent();
            this.Show();
            
            LogIn login = new LogIn();
            login.Owner = this;
            login.ShowDialog();
            StartWork(login);           

        }

        private void StartWork(Window login)
        {
            if (login.DialogResult == false)
            {
                this.Title = "Работа с клиентами";
                textBoxSecondName.IsReadOnly = true;
                textBoxFirstName.IsReadOnly = true;
                textBoxMiddleName.IsReadOnly = true;
                textBoxPassNumber.IsReadOnly = true;
                textBoxPhoneNumber.IsReadOnly = true;
                
                buttonAddClient.IsEnabled = false;
                buttonSaveClient.IsEnabled = false;
                return;
            }
            _repository = new Repository();
            
            if (login.Content is Consultant)
            {
                this.Title = "Работа с клиентами: Консультант";
                Employer = login.Content as Consultant;
                Employer.Name = "Консультант";
                textBoxSecondName.IsReadOnly = true;
                textBoxFirstName.IsReadOnly = true;
                textBoxMiddleName.IsReadOnly = true;
                textBoxPassNumber.IsReadOnly = true;
                textBoxPhoneNumber.IsReadOnly = false;
                
                buttonAddClient.IsEnabled = false;
                buttonSaveClient.IsEnabled = true;
            }
            else
            {
                this.Title = "Работа с клиентами: Менеджер";
                Employer = login.Content as Manager;
                Employer.Name = "Менеджер";
                textBoxSecondName.IsReadOnly = false;
                textBoxFirstName.IsReadOnly = false;
                textBoxMiddleName.IsReadOnly = false;
                textBoxPassNumber.IsReadOnly = false;
                textBoxPhoneNumber.IsReadOnly = false;
                
                buttonAddClient.IsEnabled = true;
                buttonSaveClient.IsEnabled = true;
            }
            
            DataContext = _repository;
            _repository.Clients.CollectionChanged += Clients_CollectionChanged;
            
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
                    Employer.GetAllChangesAddClient(Client);                    
                    whatChanged = String.Join("\n", Employer.Changes);
                    typeOfChange = "Добавление";
                    whoChanged = "Менеджер";
                    _repository.Clients[e.NewStartingIndex].Logs.Add(new Client.Log(date, whatChanged, typeOfChange, whoChanged));
                    listBoxClientList.SelectedIndex = e.NewStartingIndex;
                    break;
                case NotifyCollectionChangedAction.Replace:                    
                    Client oldClient = (Client)e.OldItems[0];
                    Client newClient = (Client)e.NewItems[0];
                    if (oldClient == newClient) break;
                    Employer.GetAllChangesChangeClient(oldClient,newClient);
                                                             
                    whatChanged = String.Join("\n", Employer.Changes);                       
                    whoChanged = Employer.Name;
                   
                    typeOfChange = "Изменение";
                    
                    _repository.Clients[e.NewStartingIndex].Logs.Add(new Client.Log(date, whatChanged, typeOfChange, whoChanged));                    
                    listBoxClientList.SelectedIndex = e.NewStartingIndex;
                    break;
                    
            }

        }

        private void buttonChangeEmployer_Click(object sender, RoutedEventArgs e)
        {            
            DataContext = null;
            textBoxSecondName.Text = "";
            textBoxFirstName.Text = "";
            textBoxMiddleName.Text = "";
            textBoxPassNumber.Text = "";
            textBoxPhoneNumber.Text = "";
            LogIn login = new LogIn();
            login.Owner = this;
            login.ShowDialog();
            StartWork(login);
            
        }

        
        private void listBoxClientList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_repository.SelectedClient == null) return;            
            Employer.GetClientInformation(_repository.SelectedClient);
            groupBoxClientInfo.DataContext = Employer.Client;            
        }

        private void buttonAddClient_Click(object sender, RoutedEventArgs e)
        {
            if (Employer.Client == null)
            {
                Employer.Client = new Client(textBoxSecondName.Text, textBoxFirstName.Text, textBoxMiddleName.Text, textBoxPassNumber.Text, textBoxPhoneNumber.Text);
            }
            Employer.Client.Logs = new ObservableCollection<Client.Log>();
            
            if (Employer.Client.SecondName == "" || Employer.Client.FirstName == "" || Employer.Client.PassNumber == "")
            {
                MessageBox.Show("Не введены все данные клиента. Фамилия, имя и номер паспорта обязательны для введения");
            }
            else
            {
                _repository.Clients.Add(Employer.Client);

                _repository.SaveClients();
            }
            
        }

        private void buttonSaveClient_Click(object sender, RoutedEventArgs e)
        {
            
            if (Employer.Client.SecondName == "" || Employer.Client.FirstName == "" || Employer.Client.PassNumber == "")
            {
                MessageBox.Show("Фамилия, имя и номер паспорта не могут быть пустыми");
            }
            else
            {                       
                _repository.SelectedClient = Employer.SetClientInformation();                
                _repository.Clients[_repository.SelectedClientIndex] = _repository.SelectedClient;               
                _repository.SaveClients();
                
            }
            
        }
    }
}
