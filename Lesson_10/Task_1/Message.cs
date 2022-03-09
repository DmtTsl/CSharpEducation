using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_10
{
    public class Message
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }

        public Message() { }
       
        public Message(DateTime date, string name, string text)
        {
            Date = date;
            Name = name;
            Text = text;
        }

    }
}
