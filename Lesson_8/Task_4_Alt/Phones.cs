using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_8
{
    public class Phones
    {
        
        public string MobilePhone { get; set; }
        public string FlatPhone { get; set; }
        public Phones() { }
        public Phones(string mobPhone, string flatPhone)
        {
            MobilePhone = mobPhone;
            FlatPhone = flatPhone;
        }
    }

}
