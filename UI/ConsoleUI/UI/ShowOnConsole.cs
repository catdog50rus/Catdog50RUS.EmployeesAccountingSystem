using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    class ShowOnConsole
    {
        public static void ShowPersons(IEnumerable<Person> collection)
        {
            Console.Clear();
            Console.WriteLine("Список сотрудников: ");
            foreach (var item in collection)
            {
                ShowPerson(item);
            }
        }

        public static void ShowPersonTasks(IEnumerable<CounterTimes> collection, (DateTime, DateTime) period)
        {
            Console.Clear();
            Console.WriteLine($"Перечень выполненных задач в период с {period.Item1} по {period.Item2}: ");
            foreach (var item in collection)
            {
                ShowTask(item);
            }
        }

        private static void ShowPerson(Person person)
        {
            Console.WriteLine(person.ToDisplay());
            Console.WriteLine();
        }

        private static void ShowTask(CounterTimes counter)
        {
            Console.WriteLine(counter.ToDisplay());
            Console.WriteLine();
        }
    }
}
