using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_2_3_MVVM
{
    public class LogResource
    {
        public Dictionary<Employer, string> Employers { get; set; }
        public LogResource()
        {
            Employers = new Dictionary<Employer, string>()
            {
                 {new Consultant(),"Консультант" },
                 {new Manager(),"Менеджер" }
            };           
            
        }
    }
}
