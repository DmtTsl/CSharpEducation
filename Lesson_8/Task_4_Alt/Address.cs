using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_8
{
    public class Address
    {
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int FlatNumber { get; set; }
        public Address() { }
        public Address (string street, int houseNum,int flatNum)
        {
            Street = street;
            HouseNumber = houseNum;
            FlatNumber = flatNum;
        }
    }
}
