using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_2_3_MVVM
{

    public class Consultant : Employer
    {        
        public string PassNumber { get; set; }
        public Consultant()
        {
            LoginPassword = new Dictionary<string, string>();
            GetSavedLoginPass();
        }


        public override Client GetClientInformation(Client client)
        {
            PassNumber = client.PassNumber;
            string clientPassNumber = client.PassNumber.Replace(client.PassNumber, new string('*', client.PassNumber.Length));
            Client Client = new Client(client.SecondName, client.FirstName, client.MiddleName, clientPassNumber, client.PhoneNumber);
            Client.Logs = client.Logs;
            return Client;
        }

        public override Client SetClientInformation(Client client)
        {
            client.PassNumber = PassNumber;
            return client;
        }
        public override void GetSavedLoginPass()
        {
            if (File.Exists("consultantLoginPassword.json"))
            {
                string jsonLoginPassword = File.ReadAllText("consultantLoginPassword.json");
                LoginPassword = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonLoginPassword);
            }
        }

        public override void SaveLoginPass()
        {
            File.WriteAllText("consultantLoginPassword.json", JsonConvert.SerializeObject(LoginPassword));
        }


        public override void GetAllChangesAddClient(Client client)
        {

        }

        public override void GetAllChangesChangeClient(Client oldClient, Client newClient)
        {

            if (oldClient.PhoneNumber != newClient.PhoneNumber)
            {
                string whatChanged = "Номер телефона";
                Changes = new List<string> { whatChanged };
            }


        }
        public override string ToString()
        {
            return "Консультант";
        }
    }
}
