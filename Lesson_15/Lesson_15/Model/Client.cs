using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Windows;

namespace Lesson_15
{
    public class Client
    {
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string PassportNumber { get; set; }
        public string PhoneNumber   { get; set; }
        public ObservableCollection<Account> Accounts { get; set; }
        public bool HasPaymentAcc { get; set; }
        public bool HasDepositAcc { get; set; }

        [JsonIgnore]
        public Account SelectedAccount { get; set; }
        public Client(string secondName, string firstName,  string passportNumber, string middleName = null, string phoneNumber = null)
        {
            SecondName = secondName;
            FirstName = firstName;
            PassportNumber = passportNumber;
            MiddleName = middleName;
            PhoneNumber = phoneNumber;
            Accounts = new ObservableCollection<Account>();
            HasPaymentAcc = false;
            HasDepositAcc = false;
        }
        public override string ToString()
        {
            return $"{SecondName} {FirstName} {MiddleName}";
        }
        public static Client AddClient(string secondName, string firstName, string passportNumber, ClientChangedEventHandler clientChangedEventHandler, string middleName = null, string phoneNumber = null)
        {
            Client client = new Client(secondName, firstName, passportNumber, middleName, phoneNumber);
            client.ClientChangedEvent += clientChangedEventHandler;
            client.ClientChangedEvent?.Invoke(client.ToString(),ClientChange.Создание);
            client.ClearEvent();
            return client;
        }
        public static Client DeleteClient(Client client, ClientChangedEventHandler clientChangedEventHandler)
        {
            client.ClientChangedEvent += clientChangedEventHandler;
            client.ClientChangedEvent?.Invoke(client.ToString(), ClientChange.Удаление);
            client.ClearEvent();
            return client;
        }
        public bool DeleteAccount(Account account)
        {
            if (SelectedAccount.AccountSum != 0)
            {                
                MessageBox.Show($"Невозможно закрыть счет! На счете номер {SelectedAccount.AccountNumber:D7} есть денежные средства",
                    "ВНИМАНИЕ", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (SelectedAccount is PaymentAccount) HasPaymentAcc = false;
            else HasDepositAcc = false;
            AccountNumberRepository.freeNumber.Add(SelectedAccount.AccountNumber);
            AccountNumberRepository.SaveData();
            Accounts.Remove(SelectedAccount);
            ClientChangedEvent?.Invoke(this.ToString(), ClientChange.Закрытие_счета, account.AccountNumber);
            ClearEvent();
            return true;
        }
        public void AddAccount(Account account)
        {            
            Accounts.Add(account);
            if (account is PaymentAccount) HasPaymentAcc = true;
            else HasDepositAcc = true;
            ClientChangedEvent?.Invoke(this.ToString(),ClientChange.Открытие_счета, account.AccountNumber);
            ClearEvent();
        }
        public delegate void ClientChangedEventHandler(string clientName, ClientChange clientChange, int? account = null);
        public event ClientChangedEventHandler ClientChangedEvent;
        public void ClearEvent()
        {
            ClientChangedEvent = null;
        }
    }
}
