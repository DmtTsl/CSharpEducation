using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_8
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> randList = new List<int>();
            Fill(randList);
            Print(randList);
            Remove(randList);
            Print(randList);
            Console.Read();
        }


        static void Fill(List<int> list)
        {
            Random r = new Random();
            int i = 0;
            while (i != 1000)
            {
                list.Add(r.Next(101));
                i++;
            }
        }
        static void Print (List<int> list)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                Console.Write($"{list[i],-5}");
            }
            Console.WriteLine("\n");
        }
        static void Remove (List<int> list)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                if(list[i] > 25 && list [i] < 50)
                {
                    list.RemoveAt(i);
                }
            }
            
        }
    }
}
