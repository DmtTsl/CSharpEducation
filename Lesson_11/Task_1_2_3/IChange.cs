using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_11
{
    internal interface IChange
    {
        List<string> Changes { get; set; }
        void GetAllChangesAddClient(Client client);
        void GetAllChangesChangeClient(Client oldClient, Client newClient);
    }
}
