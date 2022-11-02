using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_2_3
{
    public class AddClientWindowVM
    {
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public AddClientWindowVM() { }
        public AddClientWindowVM(string secondName, string firstName, string middleName, string phoneNumber, string email)
        {
            SecondName = secondName;
            FirstName = firstName;
            MiddleName = middleName;
            PhoneNumber = phoneNumber;
            Email = email;
        }
        
    }
}
