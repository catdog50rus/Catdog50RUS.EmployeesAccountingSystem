using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    class ShowOnConsole
    {
        public static void ShowPersons(IEnumerable<Person> collection)
        {
            Console.Clear();
            Console.WriteLine("Список сотрудников: ");
            Console.WriteLine();
            foreach (var item in collection)
            {
                ShowPerson(item);
            }
        }

        public static void ShowPersonTasks(Person person, IEnumerable<CompletedTask> collection, (DateTime, DateTime) period, (double, decimal) data)
        {
            Console.Clear();
            Console.WriteLine($"Перечень выполненных задач Сотрудником {person} \nВ период с {period.Item1:dd.MM.yyyy} по {period.Item2:dd.MM.yyyy}: ");
            Console.WriteLine();
            foreach (var item in collection)
            {
                ShowTask(item);
            }
            Console.WriteLine($"Всего затрачено {data.Item1} часов, к выплате: {data.Item2} рублей.");
        }

        public static void ShowNewPerson(Person person)
        {
            Console.Clear();
            Console.WriteLine(person.ToInsert());
            Console.WriteLine();
        }

        public static void ShowDeletePerson(Person person)
        {
            Console.Clear();
            Console.WriteLine(person.ToDelete());
            Console.WriteLine();
        }

        public static void ShowContinue()
        {
            Console.WriteLine();
            Console.WriteLine("Для продолжения нажмите любую клавишу");
            Console.ReadKey();
            Console.Clear();
        }

        public static void ShowError(string err)
        {
            Console.Clear();
            Console.WriteLine(err);
        }


        public static void ShowNewTask(CompletedTask task)
        {
            Console.Clear();
            Console.WriteLine(task.ToInsert());
            Console.WriteLine();
        }

        private static void ShowPerson(Person person)
        {
            Console.WriteLine(person.ToDisplay());
        }

        private static void ShowTask(CompletedTask task)
        {
            Console.WriteLine(task.ToDisplay());
        }
    }
}
