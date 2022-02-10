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
            var phoneBook = new Dictionary<string, string>();
            
            ExitOrNot(phoneBook);           

        }

        static void NewRecord(Dictionary<string, string> dic)
        {
            string telNumber;
            string fullName;
            while (true)
            {                               
                while (true)
                {
                    Console.WriteLine("Введите номер телефона");
                    telNumber = Console.ReadLine();
                    if (dic.ContainsKey(telNumber))
                    {
                        Console.WriteLine("Такой номер телефона уже есть в базе");
                    }
                    else break;

                }
                if (telNumber == "0")
                {
                    return;
                }
                Console.WriteLine("Введите ФИО");
                fullName = Console.ReadLine();
                dic.Add(telNumber, fullName);

            }
        }

        static void FindUser(Dictionary<string,string> dic)
        {
            Console.WriteLine("Введите номер телефона, чтобы найти пользователя");
            string telNumber=Console.ReadLine();
            if (dic.TryGetValue(telNumber, out string fullName))
            {
                Console.WriteLine($"{telNumber} - {fullName}");
            }
            else Console.WriteLine("Такого номера телефона нет в базе");
        }

        static void ExitOrNot(Dictionary<string, string> dic)
        {
            do
            {
                NewRecord(dic);
                FindUser(dic);
                Console.WriteLine("Продолжить работу? Y - да; N - нет, закрыть программу");
            }
            while (char.ToLower(Convert.ToChar(Console.ReadLine())) == 'y');
        }
    }
}
