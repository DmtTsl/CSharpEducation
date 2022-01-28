using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_7
{
    struct ListOfEmp
    {
        private Employee[] employees;

        public ListOfEmp (string[] lines)
        {
            int index = lines.Length;
            employees = new Employee[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                employees[i] = new Employee(lines[i].Split('#'));

            }

        }

        public Employee this [int index]
            {
            get => employees[index];
            set => employees[index] = value;
            }



       /// <summary>
       /// Вывод списка сотрудников на экран
       /// 0 - весь список
       /// ID сотрудника - данные сотрудника с указанным ID
       /// </summary>
       /// <param name="index">0 или ID сотрудника</param>
        public void Show(int index)
        {
            Console.WriteLine(Actions.Title());
            if (index == 0)
            {
                for (int i = 0; i < employees.Length; i++)
                {
                    
                    Console.WriteLine(employees[i].DataToShow());
                }
            }
            else
            {
                for (int i = 0; i < employees.Length; i++)
                {
                    if (index == employees[i].ID)
                    {
                       
                        Console.WriteLine(employees[i].DataToShow());
                        return;
                    }
                }
                Console.WriteLine($"Работника с ID {index} не существует");
            }
            
        }
        
       


        /// <summary>
        /// Добавляет в массив employees данные нового сотрудника и перезаписывает файл с новыми данными
        /// </summary>
        /// <param name="fileName"></param>
        public void Add(string fileName)
        {
            Array.Resize(ref employees, employees.Length + 1);
            employees[employees.Length - 1] = new Employee(NewEmpData());
            Actions.Save(fileName, ArrayToWrite());
        }

        /// <summary>
        /// Правка записи с выбранным ID
        /// </summary>
        /// <param name="fileName"></param>
        public void Edit(string fileName)
        {
            Console.WriteLine("Введите ID сотрудника для изменения данных");
            int index = Actions.TryParse();
            for (int i = 0; i < employees.Length; i++)
            {
                if (index == employees[i].ID)
                {
                    employees[i] = new Employee(NewEmpData());
                    Actions.Save(fileName, ArrayToWrite());
                    return;
                }

            }
            Console.WriteLine($"Работника с ID {index} не существует");
        }

        /// <summary>
        /// Удаление записи с выбранным ID
        /// </summary>
        /// <param name="fileName"></param>
        public void Erase(string fileName)
        {
            Console.WriteLine("Введите ID сотрудника для уудаления данных");
            int index = Actions.TryParse();
            int j = 0;
            Employee[] employeesTemp = new Employee[employees.Length - 1];
            try
            {
                for (int i = 0; i < employees.Length; i++)
                {
                    if (index != employees[i].ID)
                    {

                        employeesTemp[j] = employees[i];
                        j++;
                    }

                }
                employees = employeesTemp;
                Actions.Save(fileName, ArrayToWrite());
            }
            catch
            {
                Console.WriteLine($"Сотрудника с ID {index} нет в списке");
            }
           
        }

        /// <summary>
        /// Сортировка записей по дате
        /// </summary>
        public void DateSorting()
        {
            DateTime[] now = new DateTime[employees.Length];

            for (int i = 0; i < employees.Length; i++)
            {

                now[i] = employees[i].Now;
            }
            Console.WriteLine("1 - вывести по возрастанию дат\n2 - вывести по убыванию дат");
            int choice = Actions.ChoiceOf2();
            switch (choice)
            {
                case 1:
                    Array.Sort(now, employees);
                    break;
                case 2:
                    Array.Reverse(now);
                    Array.Sort(now, employees) ;
                    break;
            }
            
            Console.WriteLine(Actions.Title());
            for (int i = 0; i < employees.Length; i++)
            {

                Console.WriteLine(employees[i].DataToShow());
            }
           
        }

        /// <summary>
        /// Вывод записей по выбранному диапазону дат
        /// </summary>
        public void ShowDates()
        {
            Console.WriteLine("Введите дату начала списка\nГод");
            int year = Actions.TryParse();
            Console.WriteLine("Месяц");
            int month = Actions.TryParse();
            Console.WriteLine("День");
            int day = Actions.TryParse();
            DateTime start = new DateTime(year, month, day);
            Console.WriteLine("Введите дату конца списка\nГод");
            year = Actions.TryParse();
            Console.WriteLine("Месяц");
            month = Actions.TryParse();
            Console.WriteLine("День");
            day = Actions.TryParse();
            DateTime finish = new DateTime(year, month, day);

            for (int i = 0; i < employees.Length; i++)
            {
                if (start <= employees[i].Now && employees[i].Now <= finish)
                {
                    Console.WriteLine(employees[i].DataToShow());
                }
                
            }


        }




        public string[] NewEmpData()
        {
            string[] newEmpData = new string[7];
            Console.WriteLine("Введите ID");
            newEmpData[0] = GetID();
            Console.WriteLine("Введите ФИО сотрудника");
            newEmpData[2] = Console.ReadLine();
            Console.WriteLine("Введите возраст сотрудника");
            newEmpData[3] = Actions.TryParse().ToString();
            Console.WriteLine("Введите рост сотрудника");
            newEmpData[4] = Actions.TryParse().ToString();
            Console.WriteLine("Введите дату рождения сотрудника\nГод");
            int year = Actions.TryParse();
            Console.WriteLine("Месяц");
            int month = Actions.TryParse();
            Console.WriteLine("День");
            int day = Actions.TryParse();
            DateTime db = new DateTime(year, month, day);
            newEmpData[5] = db.ToString();
            Console.WriteLine("Введите место рождения сотрудника");
            newEmpData[6] = Console.ReadLine();
            newEmpData[1] = DateTime.Now.ToString();

            return newEmpData;

        }

        private string GetID()
        {
            start:
            int _ID = Actions.TryParse();
            for(int i = 0; i < employees.Length; i++)
            {
                if(_ID == employees[i].ID)
                {
                    Console.WriteLine("Сотрудник с таким ID уже существует. Введите другой ID");
                    goto start;
                }
            }

            return _ID.ToString();
        }

        /// <summary>
        /// Формирует массив для записи в файл
        /// </summary>
        /// <returns></returns>
        public string[] ArrayToWrite()
        {
            string[] lines = new string[employees.Length];
            for (int i = 0; i< lines.Length; i++)
            {
                lines[i] = employees[i].DataToWrite();
            }

            return lines;
        }


        

    }
}
