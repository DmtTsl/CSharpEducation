using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Task
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
    }
}
