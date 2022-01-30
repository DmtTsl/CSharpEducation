using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_7
{
    struct EmpRepository
    {
        private Employee[] employees;

        private int length;
        public EmpRepository(string[] lines)
        {
            length = lines.Length;
            employees = new Employee[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] args = lines[i].Split('#');

                employees[i] = new Employee(int.Parse(args[0]), Convert.ToDateTime(args[1]), args[2], int.Parse(args[3]), int.Parse(args[4]), Convert.ToDateTime(args[5]), args[6]);

            }
            
        }

        public Employee this[int index]
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
        public void GetAllOrByID(int index)
        {
            Console.WriteLine(EmpService.Title());
            if (index == 0)
            {
                for (int i = 0; i < length; i++)
                {
                    
                    Console.WriteLine(employees[i].DataToShow());
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
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

        public int Count()
        {
            return employees.Count();
        }


        /// <summary>
        /// Добавляет в массив employees данные нового сотрудника и перезаписывает файл с новыми данными
        /// </summary>
        /// <param name="fileName"></param>
        public void Create(string fileName)
        {
            if(employees.Length == length) Array.Resize(ref employees, employees.Length + 100);
            length++;
            string[] args = NewEmpData();
            employees[length-1] = new Employee(int.Parse(args[0]), Convert.ToDateTime(args[1]), args[2], int.Parse(args[3]), int.Parse(args[4]), Convert.ToDateTime(args[5]), args[6]);
            EmpService.Save(fileName, ArrayToWrite());
        }

        /// <summary>
        /// Правка записи с выбранным ID
        /// </summary>
        /// <param name="fileName"></param>
        public void Update(string fileName)
        {
            Console.WriteLine("Введите ID сотрудника для изменения данных");
            int index = EmpService.TryParse();
            for (int i = 0; i < length; i++)
            {
                if (index == employees[i].ID)
                {
                    string[] args = NewEmpDataEdit(i);
                    employees[i] = new Employee(int.Parse(args[0]), Convert.ToDateTime(args[1]), args[2], int.Parse(args[3]), int.Parse(args[4]), Convert.ToDateTime(args[5]), args[6]);
                    EmpService.Save(fileName, ArrayToWrite());
                    return;
                }

            }
            Console.WriteLine($"Работника с ID {index} не существует");
        }

        /// <summary>
        /// Удаление записи с выбранным ID
        /// </summary>
        /// <param name="fileName"></param>
        public void Delete(string fileName)
        {
            Console.WriteLine("Введите ID сотрудника для удаления данных");
            int index = EmpService.TryParse();
            int j = 0;
            Employee[] employeesTemp = new Employee[length-1];
            try
            {
                for (int i = 0; i < length; i++)
                {
                    if (index != employees[i].ID)
                    {

                        employeesTemp[j] = employees[i];
                        j++;
                    }

                }
                length--;
                employees = employeesTemp;
                EmpService.Save(fileName, ArrayToWrite());
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
            DateTime[] now = new DateTime[length];

            for (int i = 0; i < length; i++)
            {

                now[i] = employees[i].Now;
            }
            Console.WriteLine("1 - вывести по возрастанию дат\n2 - вывести по убыванию дат");
            int choice = EmpService.ChoiceOf2();
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
            
            Console.WriteLine(EmpService.Title());
            for (int i = 0; i < length; i++)
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
            int year = EmpService.TryParse();
            Console.WriteLine("Месяц");
            int month = EmpService.TryParse();
            Console.WriteLine("День");
            int day = EmpService.TryParse();
            DateTime start = new DateTime(year, month, day);
            Console.WriteLine("Введите дату конца списка\nГод");
            year = EmpService.TryParse();
            Console.WriteLine("Месяц");
            month = EmpService.TryParse();
            Console.WriteLine("День");
            day = EmpService.TryParse();
            DateTime finish = new DateTime(year, month, day);

            for (int i = 0; i < length; i++)
            {
                if (start <= employees[i].Now && employees[i].Now <= finish)
                {
                    Console.WriteLine(employees[i].DataToShow());
                }
                
            }


        }


        public string[] NewEmpDataEdit(int i)
        {
            string[] newEmpData = new string[7];
            
            newEmpData[0] = employees[i].ID.ToString();
            Console.WriteLine("Введите ФИО сотрудника");
            newEmpData[2] = Console.ReadLine();
            Console.WriteLine("Введите возраст сотрудника");
            newEmpData[3] = EmpService.TryParse().ToString();
            Console.WriteLine("Введите рост сотрудника");
            newEmpData[4] = EmpService.TryParse().ToString();
            Console.WriteLine("Введите дату рождения сотрудника\nГод");
            int year = EmpService.TryParse();
            Console.WriteLine("Месяц");
            int month = EmpService.TryParse();
            Console.WriteLine("День");
            int day = EmpService.TryParse();
            DateTime db = new DateTime(year, month, day);
            newEmpData[5] = db.ToString();
            Console.WriteLine("Введите место рождения сотрудника");
            newEmpData[6] = Console.ReadLine();
            newEmpData[1] = DateTime.Now.ToString();

            return newEmpData;

        }


        public string[] NewEmpData()
        {
            string[] newEmpData = new string[7];
            Console.WriteLine("Введите ID");
            newEmpData[0] = GetID();
            Console.WriteLine("Введите ФИО сотрудника");
            newEmpData[2] = Console.ReadLine();
            Console.WriteLine("Введите возраст сотрудника");
            newEmpData[3] = EmpService.TryParse().ToString();
            Console.WriteLine("Введите рост сотрудника");
            newEmpData[4] = EmpService.TryParse().ToString();
            Console.WriteLine("Введите дату рождения сотрудника\nГод");
            int year = EmpService.TryParse();
            Console.WriteLine("Месяц");
            int month = EmpService.TryParse();
            Console.WriteLine("День");
            int day = EmpService.TryParse();
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
            int _ID = EmpService.TryParse();
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
            string[] lines = new string[length];
            for (int i = 0; i< lines.Length; i++)
            {
                lines[i] = employees[i].DataToWrite();
            }

            return lines;
        }


        

    }
}
