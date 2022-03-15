using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lesson_8
{
    [Serializable]
    public class Person 
    {
        [XmlAttribute]
        public string Name { get; set; }
        public Address Address { get; set; }
        public Phones Phones { get; set; }
        public Person() { }
        public Person (string name, string street, int houseNum, int flatNum, string mobPhone, string flatPhone)
        {
            Name = name;
            Address = new Address(street, houseNum, flatNum);
            Phones = new Phones(mobPhone, flatPhone);
        }

        

    }
}
