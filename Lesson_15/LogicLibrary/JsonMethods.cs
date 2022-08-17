using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

namespace LogicLibrary
{
    public static class JsonMethods
    {
        public static ObservableCollection<T> GetJsonFileInfo<T>(string filePath)
        {
            ObservableCollection<T> list;
            if (File.Exists("clients.json"))
            {
                string jsonString = File.ReadAllText(filePath);
                JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                list = JsonConvert.DeserializeObject<ObservableCollection<T>>(jsonString, settings);
                return list;
            }
            else { list = new ObservableCollection<T>(); return list; }
        }
        public static void CreateJsonFile<T>(string filePath, ObservableCollection<T> list)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            File.WriteAllText(filePath, JsonConvert.SerializeObject(list, settings));
        }
    }
}
