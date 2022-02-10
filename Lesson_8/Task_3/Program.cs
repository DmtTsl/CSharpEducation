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
            HashSet<int> collection = new HashSet<int>();
            
            do
            {
                Console.WriteLine("Введите число");
                int value = int.Parse(Console.ReadLine());
                if (!collection.Contains(value))
                {
                    collection.Add(value);
                    Console.WriteLine("Число сохранено");
                }
                            
                else Console.WriteLine("Число уже есть в базе");
                Console.WriteLine("Продолжить работу? Y - да; N - нет, закрыть программу");
            }
            while (char.ToLower(Convert.ToChar(Console.ReadLine())) == 'y');
        }
    }
}
