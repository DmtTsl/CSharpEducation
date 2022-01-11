using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_5
{
    class Program
    {
        static void Main(string[] args)
        {
            Print(Split(GetSentence()));
            Console.ReadKey();
            
        }
        static string GetSentence()
        {
            Console.WriteLine("Type a sentence");
            string sentence = Console.ReadLine();
            return sentence;
        }
        static string[] Split(string s)
        {
            string[] mass = s.Split(' ');
            return mass;
        }
        static void Print (string [] mass)
        {
            foreach(string s in mass)
            {
                Console.WriteLine(s);
            }
        }
    }
}
