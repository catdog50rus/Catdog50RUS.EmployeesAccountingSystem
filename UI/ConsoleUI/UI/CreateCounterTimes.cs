using Catdog50RUS.EmployeesAccountingSystem.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    class CreateCounterTimes
    {
        public static CounterTimes CreatNewCounter(Person person)
        {
            string task, date = DateTime.Today.ToString("dd.MM.yyyy");
            double time = 0;

            Console.Clear();

            Console.WriteLine("Введите наименование задачи");
            Console.WriteLine();
            task = InputParameters.InputStringParameter();

            while (time <= 0)
            {
                Console.WriteLine();
                Console.WriteLine("Введите затраченное время в часах (например: 3,5)");
                Console.WriteLine();
                double.TryParse(InputParameters.InputStringParameter(), out time);
            }

            CounterTimes counter = new CounterTimes(person, date, time, task);
            return counter;
        }
    }
}
