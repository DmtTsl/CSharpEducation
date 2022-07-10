using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Task_1_2_3_MVVM
{
    public class AddClientVM
    {
        public Client NewClient { get; set; }
        public AddClient window { get; set; }
        public AddClientVM(AddClient window)
        {
            NewClient = new Client();
            this.window = window;
        }
        private RelayCommand _addClientCommand;
        public RelayCommand AddClientCommand
        {
            get
            {
                return _addClientCommand ?? 
                    (_addClientCommand = new RelayCommand(obj =>
                       {                           
                           if (NewClient.SecondName == "" || NewClient.FirstName == "" || NewClient.PassNumber == "")
                           {
                               MessageBox.Show("Не введены все данные клиента. Фамилия, имя и номер паспорта обязательны для введения");
                           }
                           else if (!string.IsNullOrEmpty(NewClient.PassNumber) && NewClient.PassNumber.Length < 10)
                           {
                               MessageBox.Show("Номер паспорта должен состоять из 10 цифр");
                           }
                           else if (!string.IsNullOrEmpty(NewClient.PhoneNumber) && NewClient.PhoneNumber.Length < 10)
                           {
                               MessageBox.Show("Номер nелефона должен состоять из 10 цифр");
                           }
                           else
                           {
                               window.DialogResult = true;
                           }
                       }));
            }
        }
    }
}
