using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_10
{
    public class User
    {
        public long ID { get; set; }
        public string Name { get; set; }

        public ObservableCollection<Message> Messages { get; set; }
        public User() 
        {
            Messages = new ObservableCollection<Message>();
        }
        public User (long id, string name)
        {
            ID = id;
            Name = name;
            Messages = new ObservableCollection<Message>();
        }


        
    }
}
