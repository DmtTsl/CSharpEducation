using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_11
{

    public class Consultant : Employer
    {
        public string PassNumber { get; set; }
        public Consultant()
        {
            LoginPassword = new Dictionary<string, string>();
            GetSavedLoginPass();
        }


        public override void GetClientInformation(Client client)
        {
            PassNumber = client.PassNumber;
            string clientPassNumber = client.PassNumber.Replace(client.PassNumber, new string('*', client.PassNumber.Length - 1));
            Client = new Client(client.SecondName, client.FirstName, client.MiddleName, clientPassNumber, client.PhoneNumber);
            Client.Logs = client.Logs;
        }

        public override Client SetClientInformation()
        {
            Client.PassNumber = PassNumber;
            return Client;
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
    }
}
