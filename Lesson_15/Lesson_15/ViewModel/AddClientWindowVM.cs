using ExceptionsLibrary;
using System.Windows;

namespace Lesson_15
{
    public class AddClientWindowVM
    {
        public class NewClientInfo
        {
            public string SecondName { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string PassportNumber { get; set; }
            public string PhoneNumber { get; set; }
        }
        public NewClientInfo NewClient { get; set; }
        AddClientWindow AddClientWindow { get; set; }  
        public AddClientWindowVM (AddClientWindow addClientWindow)
        {
            NewClient = new NewClientInfo();
            AddClientWindow = addClientWindow;
        }
        private void AddClient()
        {
            if (NewClient.SecondName == "" || NewClient.FirstName == "" || NewClient.PassportNumber == "")
            {
               throw new NewClientLossOrWrongData("Имя, фамилия и номер паспорта не должны быть пустыми");
            }
            else if (!string.IsNullOrEmpty(NewClient.PassportNumber) && NewClient.PassportNumber.Length < 10)
            {
                throw new NewClientLossOrWrongData("Номер паспорта должен состоять из 10 цифр");
            }
            else if (!string.IsNullOrEmpty(NewClient.PhoneNumber) && NewClient.PhoneNumber.Length < 10)
            {
                throw new NewClientLossOrWrongData("Номер паспорта должен состоять из 10 цифр");
            }
            else
            {
                AddClientWindow.DialogResult = true;
            }
        }
        private RelayCommand _addClientCommand;
        public RelayCommand AddClientCommand
        {
            get
            {
                return _addClientCommand ??
                    (_addClientCommand = new RelayCommand(obj =>
                    {
                        try { AddClient(); }
                        catch(NewClientLossOrWrongData e) { MessageBox.Show(e.Message); }
                    }));
            }
        }
    }
}
