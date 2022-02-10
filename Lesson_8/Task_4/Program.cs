using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Lesson_8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите имя");
            string _name = Console.ReadLine();
            Console.WriteLine("Введите Улицу");
            string _street = Console.ReadLine();
            Console.WriteLine("Введите номер дома");
            int _houseNumber = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите номер квартиры");
            int _flatNumber = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите номер мобильного телефона");
            string _mobPhone = Console.ReadLine();
            Console.WriteLine("Введите домашнего телефона");
            string _flatPhone = Console.ReadLine();

            //XElement person = new XElement("Person");
            //XElement address = new XElement("Address");
            //XElement phones = new XElement("Phones");

            XElement street = new XElement("Street",_street);
            XElement houseNum = new XElement("HouseNember",_houseNumber);
            XElement flatNum = new XElement("FlatNumber",_flatNumber);
            XElement mobPhone = new XElement("MobilePhone",_mobPhone);
            XElement flatPhone = new XElement("FlatPhone",_flatPhone);

            XAttribute name = new XAttribute("name", _name);
            #region Создание экземпляров с заполненными данными посредством конструкторов.
            XElement address = new XElement("Address", street, houseNum, flatNum);
            XElement phones = new XElement("Phones", mobPhone, flatPhone);
            XElement person = new XElement ("Person",address,phones, name);
            #endregion

            //phones.Add(mobPhone, flatPhone);
            //address.Add(street, houseNum,flatNum);
            //person.Add(address, phones, name);

            person.Save("Persons.xml");



        }
    }
}
