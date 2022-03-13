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

        public long ID { get; set; }

        /// <summary>
        /// Конструктор сообщения
        /// </summary>
        /// <param name="date"></param>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <param name="id">значение по-умолчанию присваивается для исходящих сообщений</param>
        public Message(DateTime date, string name, string text, long id = 0)
        {
            Date = date;
            Name = name;
            Text = text;
            ID = id;
        }

    }
}
