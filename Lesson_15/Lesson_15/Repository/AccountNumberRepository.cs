using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Lesson_15
{
    public static class AccountNumberRepository
    {              
        public static int accountNumberCount = Int32.Parse(File.ReadAllText("AccNumCount.txt"));
        public static List<int> freeNumber = JsonConvert.DeserializeObject<List<int>>(File.ReadAllText("FreeNumber.json"));
        public static void SaveData()
        {
            File.WriteAllText("AccNumCount.txt", accountNumberCount.ToString());
            File.WriteAllText("FreeNumber.json", JsonConvert.SerializeObject(freeNumber));
        }        
    }
}
