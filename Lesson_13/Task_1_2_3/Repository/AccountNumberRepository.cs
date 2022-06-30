using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Task_1_2_3
{
    public static class AccountNumberRepository
    {              
        public static string accountNumberCount = File.ReadAllText("AccNumCount.txt");
        public static List<string> freeNumber = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText("FreeNumber.json"));
        public static void SaveData()
        {
            File.WriteAllText("AccNumCount.txt", accountNumberCount);
            File.WriteAllText("FreeNumber.json", JsonConvert.SerializeObject(freeNumber));
        }
        
    }
}
