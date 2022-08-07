﻿using System.Collections.Generic;

namespace Task
{
    public class AccountTempList
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
    }
}
