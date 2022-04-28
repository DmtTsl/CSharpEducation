using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_11
{
    public abstract class Employer:IChange
    {
        
        public Client Client { get; set; }        
        public string Name { get; set; }
        public Dictionary<string, string> LoginPassword { get; set; }
        public List<string> Changes { get; set; }

        public abstract void GetClientInformation (Client client);
        public abstract Client SetClientInformation();
        public abstract void GetSavedLoginPass();
        public abstract void SaveLoginPass();

        public abstract void GetAllChangesAddClient(Client client);


        public abstract void GetAllChangesChangeClient(Client oldClient, Client newClient);
       
    }
}
