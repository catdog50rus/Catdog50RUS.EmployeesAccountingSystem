using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Globalization;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Services
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
            //Выводим на консоль запрос ввода
            ShowText(text);
            var res = Console.ReadLine();
            //Проверяем введенные данные
            while (string.IsNullOrWhiteSpace(res))
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
            //Выводим на консоль запрос ввода
            ShowText(text);
            double res = 0;

            while (res <= 0)
            {
                var input = Console.ReadLine();
                //Проверяем введенные данные
                if (!double.TryParse(input, out res))
                {
                    ShowUncorrectDataMessage();
                }
            }
            return res;
        }
        /// <summary>
        /// Получение параметра Integer
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int InputIntegerParameter(string text)
        {
            //Выводим на консоль запрос ввода
            ShowText(text);
            int res = 0;
            while (res <= 0)
            {
                var input = Console.ReadLine();
                //Проверяем введенные данные
                if (!int.TryParse(input, out res))
                {
                    ShowUncorrectDataMessage();
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
            //Выводим на консоль запрос ввода
            ShowText(text);
            decimal res = 0;
            while (res <= 0)
            {
                var input = Console.ReadLine();
                //Проверяем введенные данные
                if (!decimal.TryParse(input, out res))
                {
                    ShowUncorrectDataMessage();
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
            //Выводим на консоль запрос ввода
            ShowText(text);
            DateTime date = default;
            bool exitFromWhile = false;
            while(!exitFromWhile)
            {
                var res = Console.ReadLine();
                exitFromWhile = DateTime.TryParseExact(res, "dd.MM.yyyy", CultureInfo.CurrentCulture, 
                                                   DateTimeStyles.None, out date);
                //Проверяем введенные данные
                if (!exitFromWhile)
                {
                    ShowUncorrectDataMessage();
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
            //Проверяем введенные данные
            while (end <= start)
            {
                end = InputDateParameter($"Введите конечную дату (дата должна быть старше чем {start:dd.MM.yyyy})");
            }
            return (start, end);
        }
        /// <summary>
        /// Получение месячного периода
        /// </summary>
        /// <returns></returns>
        public static (DateTime, DateTime) GetMonth()
        {
            int month = 0; 
            while (month < 1 || month > 12)
            {
                month = InputIntegerParameter("Введите номер месяца (например: 5)");
            }
            var date = $"01.{month}.{DateTime.Today.Year}";
            DateTime.TryParse(date, out DateTime res);
            Console.Clear();
            return (res, res.AddMonths(1));
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
            Console.WriteLine();
            return dep;

        }
        /// <summary>
        /// Получение параметр Positions
        /// </summary>
        /// <returns></returns>
        public static Positions InputPosition(Departments dep)
        {
            Positions pos = default;
            Console.WriteLine();
            Console.WriteLine("Выберите должность сотрудника:");
            if(dep == Departments.Managment)
                Console.WriteLine("1 - Директор");
            else
            {
                Console.WriteLine("2 - Разработчик в штате");
                Console.WriteLine("3 - Разработчик вне штата");
            }
            Console.WriteLine();
            var key = Console.ReadKey().KeyChar;
            switch (key)
            {
                case '1':
                    if (!dep.Equals(Departments.Managment))
                        break;
                    pos = Positions.Director;
                    break;
                case '2':
                    pos = Positions.Developer;
                    break;
                case '3':
                    pos = Positions.Freelance;
                    break;
                default:
                    InputPosition(dep);
                    break;
            }
            Console.WriteLine(); 
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
        /// <summary>
        /// Вывод сообщения о вводе некорректных данных
        /// </summary>
        private static void ShowUncorrectDataMessage()
        {
            Console.WriteLine("Некорректный ввод!");
            Console.WriteLine("Попробуйте еще раз!");
            Console.WriteLine();
        }
    }
}
