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
    struct EmpService
        
    {/// <summary>
    /// Проверка на возможность преобразования в Int32
    /// Если успешно, то возвращается число
    /// Если ошибка, то выводится сообщение о неверном вводе
    /// </summary>
    /// <returns>Значение INT32</returns>
        public static int TryParse()
        {
            int value;
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine($"Введено неверное значение. Необходимо ввести цифры");
            }
            return value;
        }

        

        /// <summary>
        /// открывает окно Виндоус выбора файла
        /// </summary>
        /// <returns>путь к файлу</returns>
        /// 
        private static string FileSelection()
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
        private static string FileSelection(string fileNm)
        {

            string dirName = string.Empty;
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                dirName = folderBrowser.SelectedPath;
            }

            string fileName = $@"{dirName}\{fileNm}.txt";

            return fileName;
        }

        public static string[] ReadFile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            return lines;
        }

       


        

        /// <summary>
        /// Запись строк данных в файл
        /// </summary>
        /// <param name="path">путь к файлу</param>
        /// <param name="lines">массив строк для записи</param>
        public static void Save(string path, string[] lines)
        {
            File.WriteAllLines(path, lines);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int ChoiceOf8()
        {

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2 && choice != 3 && choice != 4 && choice != 5 && choice != 6 && choice != 7 && choice != 8))
            {
                Console.WriteLine($"Введено неверное значение. Необходимо ввести цифру в диапазоне от 1 до 8");
            }
            return choice;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int ChoiceOf6()
        {
            
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2 && choice != 3 && choice != 4 && choice != 5 && choice != 6))
            {
                Console.WriteLine($"Введено неверное значение. Необходимо ввести цифру в диапазоне от 1 до 6");
            }
            return choice;
        }

        
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int ChoiceOf3()
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2 && choice != 3))
            {
                Console.WriteLine($"Введено неверное значение. Необходимо ввести 1, 2 или 3");
            }
            return choice;
        }
        /// <summary>
        /// Проверяет правильность введеной информации для выбора одного из двух вариантов
        /// </summary>
        /// <returns>Выбранный вариант 1 ил 2</returns>
        public static int ChoiceOf2()
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
            {
                Console.WriteLine($"Введено неверное значение. Необходимо ввести 1 или 2");
            }
            return choice;
        }


        
        /// <summary>
        /// Позволяет выбрать существующий файл или создать новый
        /// </summary>
        /// <param name="_new">true если создан новый файл, false если работа с существующим файлом</param>
        /// <returns>путь к файлу</returns>
        public static string New1OrExisting2File()
        {
            if (ChoiceOf2() == 2)
            {
            tryagain:
                Console.WriteLine("Введите название файла");
                string fileNm = Console.ReadLine();

                string fileName = FileSelection(fileNm);
                if (File.Exists(fileName))
                {
                    Console.WriteLine("Такой файл существует.\n1 - открыть существующий файл\n2 - создать файл с другим именем или расположением");
                    if (ChoiceOf2() == 1)
                    {
                        
                        return fileName;
                        
                    }
                    else
                    {

                        goto tryagain;
                    }
                }
                else
                {
                   
                    return fileName;
                   
                }
                           
            }
            else
            {
                
                string fileName = FileSelection();
                return fileName;               

            }
        }

        
        public static string Title()
        {
            string[] title = { "ID","Дата и время записи","ФИО","Возраст","Рост","Дата рождения","Место рождения" };
            string s = $"{title[0]}\t{title[1]}\t{title[2],-30}\t{title[3]}\t{title[4]}\t{title[5]}\t{title[6]}";

            return s;
        }
        

    }
}
