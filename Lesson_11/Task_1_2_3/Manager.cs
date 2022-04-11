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
            ClientFirstName = client.FirstName;
            ClientSecondName = client.SecondName;
            ClientMiddleName = client.MiddleName;
            ClientPassNumber = client.PassNumber;
            ClientPhoneNumber = client.PhoneNumber;

        }
       
        public override void GetSavedLoginPass()
        {
            if (File.Exists("managerLoginPassword.json"))
            {
                string jsonLoginPassword = File.ReadAllText("managerLoginPassword.json");
                LoginPassword = JsonConvert.DeserializeObject<Dictionary<string,string>>(jsonLoginPassword);
            }
        }

        public override void SaveLoginPass()
        {
            File.WriteAllText("managerLoginPassword.json", JsonConvert.SerializeObject(LoginPassword));
        }
    }
}
