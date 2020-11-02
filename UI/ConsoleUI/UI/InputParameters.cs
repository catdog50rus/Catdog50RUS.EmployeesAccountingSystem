using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Text;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    static class InputParameters
    {
        public static string InputStringParameter()
        {
            var res = Console.ReadLine();
            if (string.IsNullOrEmpty(res) || string.IsNullOrWhiteSpace(res))
            {
                Console.WriteLine("Некорректный ввод!");
                Console.WriteLine("Попробуйте еще раз!");
                return "";
            }
            else return res;
        }

        public static DateTime InputDateParameter()
        {
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
                }

            }
            return date;
        }

        public static (DateTime, DateTime) GetPeriod()
        {
            DateTime start, end = default;
            Console.Clear();
            Console.WriteLine("Введите начальную дату");
            Console.WriteLine();
            start = InputDateParameter();

            while(end <= start)
            {
                Console.WriteLine();
                Console.WriteLine("Введите конечную дату");
                Console.WriteLine();
                end = InputDateParameter();
            }

            return (start, end);
        }
    }
}
