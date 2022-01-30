using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_7
{
    struct Employee
    {
        
        public int ID { get; set; }
        public DateTime Now { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }

        //public Employee(string[] args)
        //{
        //    this.ID = int.Parse(args[0]);
        //    this.Now = Convert.ToDateTime(args[1]);
        //    this.Name = args[2];
        //    this.Age = int.Parse(args[3]);
        //    this.Height = int.Parse(args[4]);
        //    this.DateOfBirth = Convert.ToDateTime(args[5]);
        //    this.PlaceOfBirth = args[6];
        //}

        public Employee(int id, DateTime now, string name, int age, int height, DateTime dateOfBirth, string placeOfBirth)
        {
            this.ID = id;
            this.Now = now;
            this.Name = name;
            this.Age = age;
            this.Height = height;
            this.DateOfBirth = dateOfBirth;
            this.PlaceOfBirth = placeOfBirth;
        }

        /// <summary>
        /// Формирует строку для записи в файл
        /// </summary>
        /// <returns></returns>
        public string DataToWrite()
        {
            string s = $"{ID}#{Now}#{Name}#{Age}#{Height}#{DateOfBirth.ToShortDateString()}#{PlaceOfBirth}";
            return s;
        }

        /// <summary>
        /// Формирует строку для вывода в консоль
        /// </summary>
        /// <returns></returns>
        public string DataToShow()
        {
            string s = $"{ID}\t{Now}\t{Name,-30}\t{Age}\t{Height}\t{DateOfBirth.ToShortDateString()}\t{PlaceOfBirth}";
            return s;

        }
        

    }
}
