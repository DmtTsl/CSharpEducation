using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;



namespace Lesson_7
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

        start:
            Console.WriteLine("Выберите что делать\n1 - выбрать существующий файл\n2 - создать новый файл");
            string fileName = Actions.New1OrExisting2File();

        proceed:
            string[] lines = Actions.ReadFile(fileName);
            ListOfEmp employees = new ListOfEmp(lines);
        
            if (employees.Count() == 0)
            {
                Console.WriteLine("Выберите что делать\n1 - добавить новую запись\n2 - выбрать другой файл\n3 - закрыть программу");
                int choice = Actions.ChoiceOf3();
                switch (choice)
                {
                    case 1:
                        employees.Add(fileName);
                        goto proceed;

                    case 2: goto start;
                    case 3: return;
                }
            }
            else if (employees.Count() == 1)
            {

                Console.WriteLine("Выберите что делать\n1 - прочитать запись\n2 - добавить новую запись\n3 - изменить запись\n" +
                    "4 - удалить запись\n5 - выбрать другой файл\n6 - закрыть программу");
                int choice = Actions.ChoiceOf6();
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Введите ID сотрудника для вывода записи или 0, чтобы вывести все записи");
                        employees.Show(Actions.TryParse());
                        goto proceed;

                    case 2:
                        employees.Add(fileName);
                        goto proceed;

                    case 3:
                        employees.Edit(fileName);
                        goto proceed;

                    case 4:
                        employees.Erase(fileName);
                        goto proceed;

                    case 5: goto start;

                    case 6: return;
                }
            }
            else
            {
                Console.WriteLine("Выберите что делать\n1 - прочитать запись\n2 - добавить новую запись\n3 - изменить запись\n" +
                    "4 - удалить запись\n5 - вывод записей по диапазону дат\n6 - вывод всех записей, отсортированных по дате\n" +
                    "7 - выбрать другой файл\n8 - закрыть программу");
                int choice = Actions.ChoiceOf8();
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Введите ID сотрудника для вывода записи или 0, чтобы вывести все записи");
                        employees.Show(Actions.TryParse());
                        goto proceed;

                    case 2:
                        employees.Add(fileName);
                        goto proceed;

                    case 3:
                        employees.Edit(fileName);
                        goto proceed;

                    case 4:
                        employees.Erase(fileName);
                        goto proceed;

                    case 5:
                        employees.ShowDates();
                        goto proceed;

                    case 6:
                        employees.DateSorting();
                        goto proceed;

                    case 7: goto start;

                    case 8: return;
                }



            }

            

            
        
        }
        
        
        
        
       
            
    }
}
