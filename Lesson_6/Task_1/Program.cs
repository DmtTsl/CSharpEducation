using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;



namespace Lesson_6
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            do
            {
                
                Greeting();
                if (WhatToDo() == 1)
                {
                    string fileName = fileSelection();
                    ReadFile(fileName);
                }
                else
                {
                    NewOrExistingFile();
                    if (WhatToDo() == 1)
                    {
                        tryagain:
                        Console.WriteLine("Введите название файла");
                        string fileNm = Console.ReadLine();

                        string fileName = fileSelection(fileNm);
                        if(File.Exists(fileName))
                        {
                            Console.WriteLine("Такой файл существует.\n1 - внести изменения в существующий файл\n2 - создать файл с другим именем или расположением");
                            if (WhatToDo() == 2) goto tryagain;
                        }

                        var data = GetNewData();
                        WriteFile(data, fileName);
                    }
                    else
                    {
                        string fileName = fileSelection();
                        var data = GetNewData();
                        WriteFile(data, fileName);
                    }

                }
                Console.WriteLine("Продолжить работу? Y - да; N - нет, закрыть программу");
                
            }
            while (char.ToLower(Convert.ToChar(Console.ReadLine())) == 'y');
            
        }
        static int WhatToDo()
        {

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
            {
                Console.WriteLine($"Введено неверное значение. Необходимо ввести 1 или 2");
            }
            return choice;
            

            
        }
        static void Greeting()
        {
            Console.WriteLine("Выберите что делать\n1 - вывести информацию из файла на экран\n2 - добавить новую запись в файл");
          
        }
        static void NewOrExistingFile()
        {
            Console.WriteLine("Выберите что делать\n1 - создать новый файл и добавить\n2 - добавить в существующий файл");

        }
        /// <summary>
        /// открывает окно Виндоус выбора файла
        /// </summary>
        /// <returns>путь к файлу</returns>
        /// 
        static string fileSelection()
        {
            string fileName = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
            }
            return fileName;
        }
        /// <summary>
        /// формирует путь к файлу, используя введенное имя
        /// </summary>
        /// <param name="fileNm">имя файла, который будет создан</param>
        /// <returns>путь к файлу</returns>
        static string fileSelection(string fileNm)
        {
            
            string dirName = string.Empty;
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            if(folderBrowser.ShowDialog() == DialogResult.OK)
            {
                dirName = folderBrowser.SelectedPath;
            }
                      
            string fileName = $@"{dirName}\{fileNm}.txt";
            
                
                
            return fileName;
        }

        static void ReadFile(string fileName)
        {
            
           string[] lines = File.ReadAllLines(fileName);
            
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i].Replace("#", "\t"));
            }


        }
        /// <summary>
        /// собирает данные для записи в файл
        /// </summary>
        /// <returns>кортеж значений данных</returns>
        static (int ID,string Name,int Age,int Height,string DateOfBirth,string PlaceOfBirth) GetNewData()
        {
            Console.WriteLine("Введите ID");
            int _ID = TryParse();
            Console.WriteLine("Введите ФИО сотрудника");
            string name = Console.ReadLine();
            Console.WriteLine("Введите возраст сотрудника");
            int age = TryParse();
            Console.WriteLine("Введите рост сотрудника");
            int height = TryParse();
            Console.WriteLine("Введите дату рождения сотрудника\nГод");
            int year = TryParse();
            Console.WriteLine("Месяц");
            int month = TryParse();
            Console.WriteLine("День");
            int day = TryParse();
            DateTime db = new DateTime(year,month,day);
            string dateOfBirth = db.ToShortDateString();
            Console.WriteLine("Введите место рождения сотрудника");
            string placeOfBirth = Console.ReadLine(); 
            // можно было бы получить текущую дату и время здесь и сформировать строку для записи в файл,
            // но я решил максимально приблизить фиксацию времени к моменту записи в файл
            var tuple = (_ID,name,age,height,dateOfBirth,placeOfBirth);
            return tuple;

        }
        static int TryParse()
        {
            int value;
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine($"Введено неверное значение. Необходимо ввести цифры");
            }
            return value;
        }
        static void WriteFile((int ID, string Name, int Age, int Height, string DateOfBirth, string PlaceOfBirth) tuple, string fileName)
        {
            
            string dateTime = DateTime.Now.ToString();
            string dataToWrite = $"{tuple.ID}#{dateTime}#{tuple.Name}#{tuple.Age}#{tuple.Height}#{tuple.DateOfBirth}#{tuple.PlaceOfBirth}";
            StreamWriter streamWriter = new StreamWriter(fileName,true);
            streamWriter.WriteLine(dataToWrite);
            streamWriter.Close();
        }
    }
}
