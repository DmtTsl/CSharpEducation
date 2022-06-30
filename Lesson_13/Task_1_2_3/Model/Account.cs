using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_2_3
{
    public abstract class Account
    {
        public int AccountNumber { get; }
        public decimal Sum { get; set; }
        public Account()
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
            
        }
        public void TransferMoney(Account accountReciever, decimal transferSum)
        {
            if (this.Sum >= transferSum)
            {
                this.Sum -= transferSum;
                accountReciever.Sum += transferSum;
            }
        }
        public void AddMoney(decimal addSum)
        {
            this.Sum += addSum;
        }
        public override string ToString()
        {
            string str = this.AccountNumber.ToString().PadLeft(8,'0');

            return str;
        }
    }
}
