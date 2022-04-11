using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_11
{
    public class Client
    {
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }        
        public string PassNumber { get; set; }
        public string PhoneNumber { get; set; }

        public Client(string secondName, string firstName, string middleName, string passNumber, string phoneNumber)
        {
            SecondName = secondName;
            FirstName = firstName;
            MiddleName = middleName;
            PassNumber = passNumber;
            PhoneNumber = phoneNumber;
        }

        public List<Log> Logs { get; set; }

        public class Log
        {
            public DateTime ChangeDateTime { get; set; }
            public List<string> WhatChanged { get; set; }
            public string TypeOfChanges { get; set; }
            public string WhoChanged { get; set; }
        }

        public override string ToString()
        {
            return $"{SecondName} {FirstName} {MiddleName}";
        }


    }
}
