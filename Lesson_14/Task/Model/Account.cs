using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Task
{    
    public abstract class Account: INotifyPropertyChanged
    {
        public int AccountNumber { get; set; }

        private decimal _accountSum;
        public decimal AccountSum 
        { get { return _accountSum; } set { _accountSum = value; OnPropertyChanged(); } }
        
        [JsonIgnore]
        public string AccountType
        {
            get { return this is DepositAccount?"Депозитный":"Расчетный"; }            
        }
        public Account() { }
        public Account(bool isNew)
        {
            if (AccountNumberRepository.freeNumber.Count > 0)
            {
                AccountNumberRepository.freeNumber.Sort();
                AccountNumber = AccountNumberRepository.freeNumber.First();
                AccountNumberRepository.freeNumber.Remove(AccountNumber);                
            }
            else
            {
                AccountNumber = ++AccountNumberRepository.accountNumberCount;                
            }
            AccountNumberRepository.SaveData();            
        }        
        public bool TransferMoney(int accountRecieverNumber, decimal transferSum, IEnumerable<Client> clients)
        {
            if (AccountSum >= transferSum)
            {
                AccountTempList accountTempList = new AccountTempList(clients);
                foreach (Account account in accountTempList)
                {
                    if (account.AccountNumber == accountRecieverNumber)
                    {
                        account.AccountSum += transferSum;
                        AccountSum -= transferSum;
                        AccountChangedEvent?.Invoke(AccountChange.Перевод, transferSum, accountRecieverNumber);
                        ClearEvent();
                        return true;
                    }                    
                }
                MessageBox.Show($"Счет с номером {accountRecieverNumber:D7} не найден");
                return false;
            }
            else
            {
                MessageBox.Show($"Перевод невозможен! На счете номер {AccountNumber:D7} недостаточно средств",
                    "ВНИМАНИЕ", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
        }
        public void AddMoney(decimal sum)
        {
            this.AccountSum += sum;
            AccountChangedEvent?.Invoke(AccountChange.Пополнение, sum);
            ClearEvent();
        }
        public bool TakeMoney(decimal sum)
        {
            if (this.AccountSum >= sum)
            {
                this.AccountSum -= sum;
                AccountChangedEvent?.Invoke(AccountChange.Списание, sum);
                ClearEvent();
                return true;
            }
            else
            {
                MessageBox.Show($"Снятие средств невозможно! На счете номер {this.AccountNumber:D7} недостаточно средств",
                        "ВНИМАНИЕ", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            
        }
        public override string ToString()
        {
            string str = this.AccountNumber.ToString().PadLeft(8,'0');

            return str;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public delegate void AccountChangedEventHandler(AccountChange accountChange,decimal sum, int? accountToTransfer = null);
        public event AccountChangedEventHandler AccountChangedEvent; 
        public void ClearEvent()
        {
            AccountChangedEvent = null;
        }
    }
}
