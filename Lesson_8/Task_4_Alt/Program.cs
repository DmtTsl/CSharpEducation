using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Lesson_8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите имя");
            string name = Console.ReadLine();
            Console.WriteLine("Введите Улицу");
            string street = Console.ReadLine();
            Console.WriteLine("Введите номер дома");
            int houseNumber = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите номер квартиры");
            int flatNumber = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите номер мобильного телефона");
            string mobPhone = Console.ReadLine();
            Console.WriteLine("Введите домашнего телефона");
            string flatPhone = Console.ReadLine();
            Person person = new Person(name, street, houseNumber, flatNumber, mobPhone, flatPhone);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Person));
            using(Stream stream = new FileStream("Notes.xml", FileMode.Append, FileAccess.Write, FileShare.None))
            {
                xmlSerializer.Serialize(stream, person);
            }



        }
    }
}
