using Newtonsoft.Json;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Task_1_2_3
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
        public void TransferMoney(Account accountReciever, decimal transferSum)
        {
            if (this.AccountSum >= transferSum)
            {
                this.AccountSum -= transferSum;
                accountReciever.AccountSum += transferSum;
            }
        }
        public void AddMoney(decimal addSum)
        {
            this.AccountSum += addSum;
        }
        public void TakeMoney(decimal addSum)
        {
            this.AccountSum -= addSum;
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
    }
}
