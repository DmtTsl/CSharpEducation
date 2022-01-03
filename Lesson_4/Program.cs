using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_4
{
    class Program
    {
        static void Main(string[] args)
        {
            #region task 1
            Console.WriteLine("Enter the number of Rows");
            int rowsNumber = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the number of Columns");
            int columnsNumber = int.Parse(Console.ReadLine());
            int[,] matrix = new int[rowsNumber, columnsNumber];
            Random r = new Random();
            int sum = new int();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = r.Next(1, 10);
                    Console.Write($"{matrix[i, j],5}");
                    sum += matrix[i, j];
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Sum = {sum}");
            Console.ReadKey();

            #endregion

            #region task 2

            Console.WriteLine("Enter the number of elements");
            int elementsNumber = int.Parse(Console.ReadLine());
            int[] sequence = new int[elementsNumber];
            for (int i = 0; i < sequence.Length; i++)
            {
                Console.WriteLine($"Enter element {i + 1}");
                sequence[i] = int.Parse(Console.ReadLine());

            }
            int min = sequence[0];
            foreach (int e in sequence)
            {
                Console.Write($"{e}\t");
                if (e < min) min = e;
            }
            Console.WriteLine();
            Console.WriteLine($"The min value in sequence: {min}");
            Console.ReadKey();
        #endregion

        #region task 3
        mark:
            Console.WriteLine("Enter the max value of the hidden number");
            Random r = new Random();
            int hiddenNumber = r.Next(0,int.Parse(Console.ReadLine())+1);
            while(true)
            {
                Console.WriteLine("Enter the hidden number");
                int a; 
                if(!int.TryParse(Console.ReadLine(),out a))
                {
                    Console.WriteLine($"The hidden number is {hiddenNumber}\nGood bye!");
                    Console.ReadKey();
                    return;
                }
                if (a == hiddenNumber)
                {
                    Console.WriteLine("Congratulations!!! You're right");
                    break;
                }
                else if (a < hiddenNumber)
                {
                    Console.WriteLine("Your number is lower than the hidden one");
                    continue;
                }
                else if (a > hiddenNumber)
                {
                    Console.WriteLine("Your number is higher than the hidden one");
                    continue;
                }
            }
            for (; ; )
            {
                Console.WriteLine("Once again? Y/N");

                switch (Console.ReadLine().ToUpper())
                {
                    case "Y":
                        goto mark;
                    case "N":
                        return;
                    default:
                        Console.WriteLine("Wrong value");
                        break;
                }
            }

            
            #endregion


        }
    }
}
