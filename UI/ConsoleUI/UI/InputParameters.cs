using System;
using System.Globalization;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    static class InputParameters
    {
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


        private static void ShowText(string text)
        {
            Console.WriteLine();
            Console.WriteLine(text);
            Console.WriteLine();
        }
    }
}
