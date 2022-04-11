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
    public class Consultant:Employer, IConsultantChange
    {
       
        public Consultant()
        {
            LoginPassword = new Dictionary<string, string>();
            GetSavedLoginPass();
        }

        public Consultant(Client client)
        {
            ClientFirstName = client.FirstName;
            ClientSecondName = client.SecondName;
            ClientMiddleName = client.MiddleName;
            ClientPassNumber = client.PassNumber.Replace(client.PassNumber, new string('*', client.PassNumber.Length - 1));
            ClientPhoneNumber = client.PhoneNumber;

        }
        public override void GetClientInformation (Client client)
        {
            ClientFirstName = client.FirstName;
            ClientSecondName = client.SecondName;
            ClientMiddleName = client.MiddleName;
            ClientPassNumber = client.PassNumber.Replace(client.PassNumber, new string('*', client.PassNumber.Length - 1));
            ClientPhoneNumber = client.PhoneNumber;

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
    }
}
