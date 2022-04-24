using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_11
{
    public class Client : INotifyPropertyChanged
    {
        private string _firstName;
        private string _secondName;
        private string _middleName;
        private string _passNumber;
        private string _phoneNumber;
       
        
        public string SecondName { get { return _secondName; } set { _secondName = value; OnPropertyChanged("SecondName"); } }
        public string FirstName { get { return _firstName; } set { _firstName = value; OnPropertyChanged("FirstName"); } }
        public string MiddleName { get { return _middleName; } set { _middleName = value; OnPropertyChanged("MiddleName"); } }        
        public string PassNumber { get { return _passNumber; } set { _passNumber = value; OnPropertyChanged("PassNumber"); } }
        public string PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; OnPropertyChanged("PhoneNumber"); } }
        
        public Client(string secondName, string firstName, string middleName, string passNumber, string phoneNumber)
        {
            SecondName = secondName;
            FirstName = firstName;
            MiddleName = middleName;
            PassNumber = passNumber;
            PhoneNumber = phoneNumber;
            Logs = new ObservableCollection<Log>();
        }

        public ObservableCollection<Log> Logs { get; set; }

        public class Log
        {
            public string ChangeDateTime { get; set; }
            public string WhatChanged { get; set; }
            public string TypeOfChanges { get; set; }
            public string WhoChanged { get; set; }

            public Log(string changeDateTime, string whatChanged, string typeOfChanges, string whoChanged)
            {
                ChangeDateTime = changeDateTime;
                WhatChanged = whatChanged;
                TypeOfChanges = typeOfChanges;
                WhoChanged = whoChanged;
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
