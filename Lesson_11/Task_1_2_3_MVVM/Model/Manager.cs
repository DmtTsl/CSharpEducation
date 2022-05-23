using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;


namespace Task_1_2_3_MVVM
{
    public class Manager:Employer
    {
        
        public Manager()
        {
            LoginPassword = new Dictionary<string, string>();
            GetSavedLoginPass();
        }

        public override void GetClientInformation(Client client)
        {
            Client = new Client(client.SecondName, client.FirstName, client.MiddleName, client.PassNumber, client.PhoneNumber);
            Client.Logs = client.Logs;
        }

        public override void GetSavedLoginPass()
        {
            if (File.Exists("managerLoginPassword.json"))
            {
                string jsonLoginPassword = File.ReadAllText("managerLoginPassword.json");
                LoginPassword = JsonConvert.DeserializeObject<Dictionary<string,string>>(jsonLoginPassword);
            }
        }

        public override Client SetClientInformation()
        {
            return Client;
            
        }

        public override void SaveLoginPass()
        {
            File.WriteAllText("managerLoginPassword.json", JsonConvert.SerializeObject(LoginPassword));
        }

        public override void GetAllChangesAddClient(Client client)
        {
            Changes = new List<string>();

                Changes.Add("Фамилия");            
                Changes.Add("Имя");
            
            if (client.MiddleName != "")
            {
                Changes.Add("Отчество");
            }

                Changes.Add("Номер паспорта");
           
            if (client.PhoneNumber != "")
            {
                Changes.Add("Номер телефона");
            }
        }

        public override void GetAllChangesChangeClient(Client oldClient, Client newClient)
        {
            Changes = new List<string>();
            if (oldClient.SecondName != newClient.SecondName)
            {
                Changes.Add("Фамилия");
            }
            if (oldClient.FirstName != newClient.FirstName)
            {
                Changes.Add("Имя");
            }
            if (oldClient.MiddleName != newClient.MiddleName)
            {
                Changes.Add("Отчество");
            }
            if (oldClient.PassNumber != newClient.PassNumber)
            {
                Changes.Add("Номер паспорта");
            }
            if (oldClient.PhoneNumber != newClient.PhoneNumber)
            {
                Changes.Add("Номер телефона");
            }
        }
        public override string ToString()
        {
            return "Менеджер";
        }
    }
}
