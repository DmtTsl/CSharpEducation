using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_2_3
{
    abstract class Account
    {
        string AccountNumber { get; set; }
        double Sum { get; set; }
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
                AccountNumber = AccountNumberRepository.accountNumberCount + 1;
                
            }
            
        }
    }
}
