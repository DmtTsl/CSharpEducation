using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_2_3
{
    public class AddAccountWindowVM: INotifyPropertyChanged
    {
        AddAccountWindow AddAccountWindow { get; set; }
        public Client SelectedClient { get; set; }
        public Account NewAccount { get; set; }  
        public decimal Sum { get; set; }
        public decimal Percent { get; set; }

        private bool _paymentAccIsChecked;
        public bool PaymentAccIsChecked 
        {
            get { return _paymentAccIsChecked; }
            set { _paymentAccIsChecked = value; OnCheckedChanged?.Invoke(); } 
        }
        private bool _depositAccIsChecked;
        public bool DepositAccIsChecked 
        {
            get { return _depositAccIsChecked; }
            set { _depositAccIsChecked = value; OnCheckedChanged?.Invoke(); } 
        }
        private bool _buttonEnabled;
        public bool ButtonEnabled 
        {
            get { return _buttonEnabled; }
            set 
            {
                _buttonEnabled = value;    
                OnPropertyChanged(); 
            }
        }
        public AddAccountWindowVM(AddAccountWindow addAccountWindow, Client client)
        {
            AddAccountWindow = addAccountWindow;
            OnCheckedChanged = ButtonEnabledHandler;
            SelectedClient = client;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        delegate void IsCheckedChanged();
        event IsCheckedChanged OnCheckedChanged;
        private void ButtonEnabledHandler()
        {
            ButtonEnabled = PaymentAccIsChecked | DepositAccIsChecked;
        }
        private RelayCommand _addAccountCommand;
        public RelayCommand AddAccountCommand
        {
            get
            {
                return _addAccountCommand ??
                    (_addAccountCommand = new RelayCommand(obj =>
                    {
                        if (PaymentAccIsChecked)
                        {
                            NewAccount = new PaymentAccount(Sum);
                        }
                        else
                        {
                            NewAccount = new DepositAccount(Percent,Sum);
                        }
                        AddAccountWindow.DialogResult = true;
                    },(obj)=> 
                    {
                        if (DepositAccIsChecked && Percent == 0)
                        {
                            return false;
                        }
                        else return true;
                    }));
            }
        }
    }
}
