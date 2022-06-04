using System;
using System.Collections.Generic;


namespace Task_1_2_3_MVVM
{
    public abstract class Employer
    {
        
       
        internal Dictionary<string, string> LoginPassword { get; set; }
        public List<string> Changes { get; set; }

        public abstract Client GetClientInformation (Client client);
        public abstract Client SetClientInformation(Client client);
        public abstract void GetSavedLoginPass();
        public abstract void SaveLoginPass();

        public abstract void GetAllChangesAddClient(Client client);


        public abstract void GetAllChangesChangeClient(Client oldClient, Client newClient);
       
    }
}
