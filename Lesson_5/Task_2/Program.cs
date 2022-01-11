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
            ReverseWords(GetSentence());
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
        static void ReverseWords(string inputPhrase)

        {
            string[] mass = Split(inputPhrase);
            Array.Reverse(mass);
            string resultString = default;
            foreach(string s in mass)
            {
                resultString += s + " ";
            }
            Console.WriteLine(resultString);
            
        }
        
    }
}
