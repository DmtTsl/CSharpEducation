using System.Collections;
using System.Collections.Generic;

namespace Lesson_15
{
    public class AccountTempList : IEnumerable
    {
        public List<Account> AccountList { get; set; }
        public AccountTempList(IEnumerable<Client> clients)
        {
            AccountList = new List<Account>();
            foreach (Client client in clients)
            {
                foreach (Account account in client.Accounts)
                {
                    AccountList.Add(account);                    
                }
            }            
        }

        public IEnumerator GetEnumerator()
        {
            return AccountList.GetEnumerator();
        }

        
    }
}
