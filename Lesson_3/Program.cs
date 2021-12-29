using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Lesson_3
{
    class Program
    {
        static void Main(string[] args)
        {
        #region task 1
            Console.WriteLine("Enter an integer");
            int integer = EnterInt();
            if (integer % 2 == 0) Console.WriteLine("The integer is an even number");
            else Console.WriteLine("The integer is an odd number");
            Console.ReadKey();
            #endregion

        #region task 2
            Console.WriteLine("How many cards do you have?");
            int amount = EnterInt();
            int sum = 0;
            Console.WriteLine("For cards with numeric face value enter the number\nFor cards with picture enter" +
                "J for jack, Q for queen, K for king, T for ace");
            for (int i = 1; i <= amount; i++)
            {
                Console.WriteLine($"Enter card #{i} value");
                int value;
                while (true)
                {
                    string card = Console.ReadLine().ToUpper();
                    if (int.TryParse(card, out value))
                    {
                        if (value >= 2 && value <= 10)
                        {
                            sum += value;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Impossible card value\n\n" + $"Enter card #{i} value");


                        }
                    }
                    else
                    {
                        switch (card)
                        {
                            case "J":
                                sum += 10;
                                break;
                            case "Q":
                                sum += 10;
                                break;
                            case "K":
                                sum += 10;
                                break;
                            case "T":
                                sum += 10;
                                break;
                            default:
                                Console.WriteLine("Impossible card value\n\n" + $"Enter card #{i} value");
                                continue;
                        }
                        break;
                    }

                }

            }
            Console.WriteLine($"Result value sum: {sum}");
            Console.ReadKey();
        #endregion

        #region task 3
        mark:
            Console.WriteLine("Enter an integer");
            int number = EnterInt();
            bool result = false;
            int z = 2;
            while (z < number)
            {
                if (number % z == 0)
                {
                    result = true;
                    break;
                }
                else z++;
            }
            if (result) Console.WriteLine("The number is not a prime one");
            else Console.WriteLine("The number is a prime one");

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
        static int EnterInt()
        {
            
            int integer;
            while (!int.TryParse(Console.ReadLine(), out integer))
            {
                Console.WriteLine("You've entered wrong value\n\nTry again");
            }
            return integer;
        }
    }
}
