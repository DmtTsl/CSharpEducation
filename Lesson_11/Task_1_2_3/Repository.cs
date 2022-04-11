using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lesson_11
{
    public class Repository
    {
        public ObservableCollection<Client> Clients { get; set; }
        
        
        public Repository()
        {            
            Clients = new ObservableCollection<Client>();
            GetSavedClients();
        }

        public void GetSavedClients()
        {
            if (File.Exists("clients.json"))
            {
                string jsonClients = File.ReadAllText("clients.json");
                Clients = JsonConvert.DeserializeObject<ObservableCollection<Client>>(jsonClients);
            }
        }
        
        public void SaveClients()
        {
            File.WriteAllText("clients.json", JsonConvert.SerializeObject(Clients));
        }


}
}
