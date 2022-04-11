using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_11
{
    public abstract class Employer : INotifyPropertyChanged
    {
        private string _clientFirstName;
        private string _clientSecondName;
        private string _clientMiddleName;
        private string _clientPassNumber;
        private string _clientPhoneNumber;
        public string ClientFirstName 
        { get
            {
                return _clientFirstName;
            }
          set
            {
                _clientFirstName = value;
                OnPropertyChanged("ClientFirstName");
            }
        }
        public string ClientSecondName 
        {
            get
            {
                return _clientSecondName;
            }
            set 
            { 
                _clientSecondName = value;
                OnPropertyChanged("ClientSecondName");
            } 
        }
        public string ClientMiddleName 
        {
            get 
            {
                return _clientMiddleName;
            }
            set 
            { 
                _clientMiddleName = value;
                OnPropertyChanged("ClientMiddleName");
            } 
        
        }
        public string ClientPassNumber 
        {
            get 
            { 
                return _clientPassNumber; 
            }
            set 
            { 
                _clientPassNumber = value;
                OnPropertyChanged("ClientPassNumber");
            } 
        }
        public string ClientPhoneNumber 
        { 
            get { return _clientPhoneNumber; }
            set { _clientPhoneNumber = value; OnPropertyChanged("ClientPhoneNumber"); }
        }
        public Dictionary<string, string> LoginPassword { get; set; }
        public abstract void GetClientInformation (Client client);
        public abstract void GetSavedLoginPass();
        public abstract void SaveLoginPass();

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
