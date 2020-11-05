using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Globalization;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    /// <summary>
    /// Класс компонентов UI 
    /// Получение параметров от пользователя
    /// </summary>
    static class InputParameters
    {
        /// <summary>
        /// Получение текстового параметра
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string InputStringParameter(string text)
        {
            ShowText(text);
            var res = Console.ReadLine();
            while (string.IsNullOrEmpty(res) || string.IsNullOrWhiteSpace(res))
            {
                Console.WriteLine("Некорректный ввод!");
                Console.WriteLine("Попробуйте еще раз!");
                res = Console.ReadLine();
            }
            return res;
        }
        /// <summary>
        /// Получение параметра Double
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static double InputDoubleParameter(string text)
        {
            ShowText(text);
            double res = 0;
            while (res <= 0)
            {
                var input = Console.ReadLine();
                if(!double.TryParse(input, out res))
                {
                    Console.WriteLine("Некорректный ввод!");
                    Console.WriteLine("Попробуйте еще раз!");
                    Console.WriteLine();
                }
            }
            return res;
        }
        /// <summary>
        /// Получение параметра Decimal
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static decimal InputDecimlParameter(string text)
        {
            ShowText(text);
            decimal res = 0;
            while (res <= 0)
            {
                var input = Console.ReadLine();
                if (!decimal.TryParse(input, out res))
                {
                    Console.WriteLine("Некорректный ввод!");
                    Console.WriteLine("Попробуйте еще раз!");
                    Console.WriteLine();
                }
            }
            return res;
        }
        /// <summary>
        /// Получение параметра Даты
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DateTime InputDateParameter(string text)
        {
            ShowText(text);
            DateTime date = default;
            bool exitWhile = false;
            while(!exitWhile)
            {
                var res = Console.ReadLine();
                exitWhile = DateTime.TryParseExact(res, "dd.MM.yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out date);
                if(!exitWhile)
                {
                    Console.WriteLine("Некорректный ввод!");
                    Console.WriteLine("Попробуйте еще раз!");
                    Console.WriteLine();
                }

            }
            return date;
        }
        /// <summary>
        /// Получение параметра период
        /// </summary>
        /// <returns></returns>
        public static (DateTime, DateTime) GetPeriod()
        {
            DateTime start, end = default;
            Console.Clear();
            start = InputDateParameter("Введите начальную дату");

            while(end <= start)
            {
                end = InputDateParameter($"Введите конечную дату (дата должна быть старше чем {start:dd.MM.yyyy})");
            }

            return (start, end);
        }
        /// <summary>
        /// Получение параметра Departments
        /// </summary>
        /// <returns></returns>
        public static Departments InputDepartment()
        {
            Departments dep = default;
            Console.WriteLine();
            Console.WriteLine("Выберите отдел сотрудника:");
            Console.WriteLine("1 - Управление компанией");
            Console.WriteLine("2 - IT отдел");
            Console.WriteLine();
            var key = Console.ReadKey().KeyChar;
            switch (key)
            {
                case '1':
                    dep = Departments.Managment;
                    break;
                case '2':
                    dep = Departments.IT;
                    break;
                default:
                    InputDepartment();
                    break;
            }
            return dep;

        }
        /// <summary>
        /// Получение параметр Positions
        /// </summary>
        /// <returns></returns>
        public static Positions InputPosition()
        {
            Positions pos = default;
            Console.WriteLine();
            Console.WriteLine("Выберите должность сотрудника:");
            Console.WriteLine("1 - Директор");
            Console.WriteLine("2 - Разработчик в штате");
            Console.WriteLine("3 - Разработчик вне штата");
            Console.WriteLine();
            var key = Console.ReadKey().KeyChar;
            switch (key)
            {
                case '1':
                    pos = Positions.Director;
                    break;
                case '2':
                    pos = Positions.Developer;
                    break;
                case '3':
                    pos = Positions.Freelance;
                    break;
                default:
                    InputPosition();
                    break;
            }
            return pos;

        }

        /// <summary>
        /// Вывод текстовой строки
        /// </summary>
        /// <param name="text"></param>
        private static void ShowText(string text)
        {
            Console.WriteLine();
            Console.WriteLine(text);
            Console.WriteLine();
        }
    }
}
