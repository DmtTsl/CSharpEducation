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
        public Address address = new Address();
        public Phones phones = new Phones();
        public Person() { }
        public Person (string name, string street, int houseNum, int flatNum, string mobPhone, string flatPhone)
        {
            Name = name;
            address.Street = street;
            address.HouseNumber = houseNum;
            address.FlatNumber = flatNum;
            phones.MobilePhone = mobPhone;
            phones.FlatPhone = flatPhone;
        }

        

    }
}
