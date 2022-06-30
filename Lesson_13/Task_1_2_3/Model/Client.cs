using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_2_3
{
    public class Client
    {
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string PassportNumber { get; set; }
        public string PhoneNumber   { get; set; }

        public ObservableCollection<Account> Accounts { get; set; }
        public bool HasCurrentAcc { get; set; }
        public bool HasDepositAcc { get; set; }
    }
}
