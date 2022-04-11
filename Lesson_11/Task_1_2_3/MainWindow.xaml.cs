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

namespace Lesson_11
{
        
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Repository _repository;
        public Employer Employer { get; set; }
        
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
                textBoxSecondName.IsEnabled = false;
                textBoxFirstName.IsEnabled = false;
                textBoxMiddleName.IsEnabled = false;
                textBoxPassNumber.IsEnabled = false;
                textBoxPhoneNumber.IsEnabled = false;
                buttonHistory.IsEnabled = false;
                buttonAddClient.IsEnabled = false;
                buttonSaveClient.IsEnabled = false;
                return;
            }
            _repository = new Repository();
            if (login.Content is Consultant)
            {
                MessageBox.Show("Cons");
                this.Title = "Работа с клиентами: Консультант";
                Employer = login.Content as Consultant;
                textBoxSecondName.IsEnabled = false;
                textBoxFirstName.IsEnabled = false;
                textBoxMiddleName.IsEnabled = false;
                textBoxPassNumber.IsEnabled = false;
                textBoxPhoneNumber.IsEnabled = true;
                buttonHistory.IsEnabled = true;
                buttonAddClient.IsEnabled = false;
                buttonSaveClient.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Manag");
                this.Title = "Работа с клиентами: Менеджер";
                Employer = login.Content as Manager;
                textBoxSecondName.IsEnabled = true;
                textBoxFirstName.IsEnabled = true;
                textBoxMiddleName.IsEnabled = true;
                textBoxPassNumber.IsEnabled = true;
                textBoxPhoneNumber.IsEnabled = true;
                buttonHistory.IsEnabled = true;
                buttonAddClient.IsEnabled = true;
                buttonSaveClient.IsEnabled = true;
            }
            listBoxClientList.ItemsSource = _repository.Clients;
        }

        private void buttonChangeEmployer_Click(object sender, RoutedEventArgs e)
        {
            listBoxClientList.ItemsSource = null;
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

        private void textBoxPassNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void listBoxClientList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxClientList.SelectedItem == null) return;
            Employer.GetClientInformation((Client)listBoxClientList.SelectedItem);
            groupBoxClientInfo.DataContext = Employer;
        }

        private void buttonAddClient_Click(object sender, RoutedEventArgs e)
        {
            Client client = new Client(textBoxSecondName.Text, textBoxFirstName.Text, textBoxMiddleName.Text, textBoxPassNumber.Text, textBoxPhoneNumber.Text);
            if (textBoxSecondName.Text == "" || textBoxFirstName.Text == "" || textBoxPassNumber.Text == "")
            {
                MessageBox.Show("Не введены все данные клиента. Фамилия, имя и номер паспорта обязательны для введения");
            }
            else
            {
                _repository.Clients.Add(client);
                _repository.SaveClients();
            }
            
        }

        private void buttonSaveClient_Click(object sender, RoutedEventArgs e)
        {
            _repository.SaveClients();
        }
    }
}
