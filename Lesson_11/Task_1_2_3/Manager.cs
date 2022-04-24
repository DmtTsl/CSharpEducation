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
    public class Manager:Employer, IManagerChange
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
    }
}
