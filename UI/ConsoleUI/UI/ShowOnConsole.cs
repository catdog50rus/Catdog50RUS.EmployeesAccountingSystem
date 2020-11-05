using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    /// <summary>
    /// Реализация вывода данных на консоль
    /// </summary>
    class ShowOnConsole
    {
        /// <summary>
        /// Вывод списка сотрудников
        /// </summary>
        /// <param name="collection"></param>
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

        /// <summary>
        /// Вывод отчета по сотруднику
        /// </summary>
        /// <param name="person"></param>
        /// <param name="period"></param>
        /// <param name="data"></param>
        public static void ShowPersonTasks(Person person, (DateTime, DateTime) period, (double, decimal, IEnumerable<CompletedTask>) data)
        {
            Console.Clear();
            Console.WriteLine($"Перечень выполненных задач Сотрудником {person} \nВ период с {period.Item1:dd.MM.yyyy} по {period.Item2:dd.MM.yyyy}: ");
            Console.WriteLine();
            var collection = data.Item3;
            foreach (var item in collection)
            {
                ShowTask(item);
            }
            Console.WriteLine($"Всего затрачено {data.Item1} часов, к выплате: {data.Item2} рублей.");
        }
        
        /// <summary>
        /// Вывод подтверждения о добавлении сотрудника
        /// </summary>
        /// <param name="person"></param>
        public static void ShowNewPerson(Person person)
        {
            Console.Clear();
            Console.WriteLine(person.ToInsert());
            Console.WriteLine();
        }
        
        /// <summary>
        /// Вывод подтверждения об удалении сотрудника
        /// </summary>
        /// <param name="person"></param>
        public static void ShowDeletePerson(Person person)
        {
            Console.Clear();
            Console.WriteLine(person.ToDelete());
            Console.WriteLine();
        }
        
        /// <summary>
        /// Вывод уведомления о нажатии клавиши для продолжения
        /// </summary>
        public static void ShowContinue()
        {
            Console.WriteLine();
            Console.WriteLine("Для продолжения нажмите любую клавишу");
            Console.ReadKey();
            Console.Clear();
        }
        
        /// <summary>
        /// Вывод сообщения
        /// </summary>
        /// <param name="err"></param>
        public static void ShowError(string err)
        {
            Console.Clear();
            Console.WriteLine(err);
        }
        
        /// <summary>
        /// Вывод подтверждения о добавлении задачи
        /// </summary>
        /// <param name="task"></param>
        public static void ShowNewTask(CompletedTask task)
        {
            Console.Clear();
            Console.WriteLine(task.ToInsert());
            Console.WriteLine();
        }
        


        /// <summary>
        /// Вывод строки списка сотрудников
        /// </summary>
        /// <param name="person"></param>
        private static void ShowPerson(Person person)
        {
            Console.WriteLine(person.ToDisplay());
        }
        /// <summary>
        /// Вывод строки отчета
        /// </summary>
        /// <param name="task"></param>
        private static void ShowTask(CompletedTask task)
        {
            Console.WriteLine(task.ToDisplay());
        }
    }
}
