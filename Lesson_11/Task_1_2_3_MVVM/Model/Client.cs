using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Task_1_2_3_MVVM
{    
    public class Client : INotifyPropertyChanged
    {
        private string _firstName;
        private string _secondName;
        private string _middleName;
        private string _passNumber;
        private string _phoneNumber;
       
        public string SecondName 
        { 
            get { return _secondName; } 
            set { if(this._secondName!= value)_secondName = value; OnPropertyChanged(); } 
        }
        
        public string FirstName 
        { 
            get { return _firstName; } 
            set { if (this._firstName != value) _firstName = value; OnPropertyChanged(); }
        }
       
        public string MiddleName 
        { 
            get { return _middleName; } 
            set { if (this._middleName != value) _middleName = value; OnPropertyChanged(); } 
        }  
        
        public string PassNumber 
        { 
            get { return _passNumber; } 
            set { if (this._passNumber != value) _passNumber = value; OnPropertyChanged(); } 
        }
        
        public string PhoneNumber 
        { 
            get { return _phoneNumber; } 
            set { if (this._phoneNumber != value) _phoneNumber = value; OnPropertyChanged(); } 
        }
        
        public Client(string secondName, string firstName, string middleName, string passNumber, string phoneNumber)
        {
            SecondName = secondName;
            FirstName = firstName;
            MiddleName = middleName;
            PassNumber = passNumber;
            PhoneNumber = phoneNumber;
            Logs = new ObservableCollection<Log>();
        }
        public Client()
        {
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
