using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type your full name");
            string fullName = Console.ReadLine();
            // adding Age with input check
            int age;
            for ( ; ; )
            {
                Console.WriteLine("Type your age");
                
                if (int.TryParse(Console.ReadLine(), out age))
                {
                    if (age < 12)
                    {
                        Console.WriteLine("You're too small to learn C#, Sorry!\nPress any key");
                        Console.ReadKey();
                        return;
                    }
                    else break;
                }
                else Console.WriteLine("Wrong input format!!!! TRY AGAIN\n");
            }

            Console.WriteLine("Type your e-mail");
            string eMail = Console.ReadLine();
            //adding Score in prog with check
            double progScore;
            for (; ; )
            {
                Console.WriteLine("Type your coding score");

                if (double.TryParse(Console.ReadLine(), (NumberStyles)32,CultureInfo.InvariantCulture, out progScore)) break;

                else Console.WriteLine("Wrong input format!!!! TRY AGAIN\n");
            }
            //adding Math score with check
            double mathScore;
            for (; ; )
            {
                Console.WriteLine("Type your math score");

                if (double.TryParse(Console.ReadLine(), (NumberStyles)32, CultureInfo.InvariantCulture, out mathScore)) break;

                else Console.WriteLine("Wrong input format!!!! TRY AGAIN\n");
            }
            //adding Phys score with check
            double physScore;
            for (; ; )
            {
                Console.WriteLine("Type your score in physics");

                if (double.TryParse(Console.ReadLine(), (NumberStyles)32, CultureInfo.InvariantCulture, out physScore)) break;

                else Console.WriteLine("Wrong input format!!!! TRY AGAIN\n");
            }

            
            Console.WriteLine($"| Full name | {fullName} |\n| Age | {age} |" +
                $"\n| e-mail | {eMail} |\n\n| Scores |\n| Coding | {progScore} |" +
                $"\n| Math | {mathScore} |\n| Phys | {physScore} |");
            Console.ReadKey();

            double sum = progScore + mathScore + physScore;
            double average = sum / 3;
            

            Console.WriteLine("Sum of all scores: {0:0.000}, Average score: {1:0.000}", sum, average);


            Console.ReadKey();

        }
    }
}
