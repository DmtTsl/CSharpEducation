using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Task_1_2_3_MVVM
{
    public class AddClientViewModel
    {
        public Client NewClient { get; set; }
        public AddClient window { get; set; }
        public AddClientViewModel(AddClient window)
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
                           else
                           {
                               window.DialogResult = true;
                           }
                       }));
            }
        }
    }
}
