using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Services
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
        /// <param name="report"></param>
        public static void ShowPersonTasks((DateTime, DateTime) period, SalaryReportModel report)
        {
            Console.Clear();
            Console.WriteLine($"Отчет по Сотруднику: {report.Person} \nВ период с {period.Item1:dd.MM.yyyy} по {period.Item2:dd.MM.yyyy}: ");
            Console.WriteLine();
            ShowTasks(report);
            
        }
        
        /// <summary>
        /// Вывод отчета по всем сотрудникам
        /// </summary>
        /// <param name="period"></param>
        /// <param name="report"></param>
        public static void ShowAllPersonsTasks((DateTime, DateTime) period, List<SalaryReportModel> report)
        {
            Console.Clear();
            Console.WriteLine($"Отчет по сотрудникам за {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(period.Item1.Month)} месяц {period.Item1.Year} года");
            Console.WriteLine();
            ShowPersonsReport(report);
            Console.WriteLine($"Итого: отработано: {report.Sum(t => t.Time)} часов, к выплате: {report.Sum(t => t.Salary)} рублей.");
        }

        /// <summary>
        /// Вывод отчета по отделам
        /// </summary>
        /// <param name="period"></param>
        /// <param name="report"></param>
        public static void ShowDepartmetsTasks((DateTime, DateTime) period, List<(Departments, List<SalaryReportModel>)> report)
        {
            Console.Clear();
            Console.WriteLine($"Отчет по отделам за {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(period.Item1.Month)} месяц {period.Item1.Year} года");
            Console.WriteLine();
            foreach (var depatment in report)
            {
                Console.WriteLine($"Отдел {depatment.Item1}:");
                Console.WriteLine();
                ShowPersonsReport(depatment.Item2);
                Console.WriteLine($"Итого по отделу {depatment.Item1}:  отработано: {depatment.Item2.Sum(t => t.Time)} часов, к выплате: {depatment.Item2.Sum(t => t.Salary)} рублей.");
                Console.WriteLine(new string('-', 100));
                Console.WriteLine();
            }
            Console.WriteLine($"Всего по организации:  отработано: {report.Sum(t=>t.Item2.Sum(s=>s.Time))} часов, к выплате: {report.Sum(t => t.Item2.Sum(s => s.Salary))} рублей.");
            Console.WriteLine(new string('-', 100));
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
        }
        
        /// <summary>
        /// Вывод сообщения
        /// </summary>
        /// <param name="mes"></param>
        public static void ShowMessage(string mes)
        {
            Console.Clear();
            Console.WriteLine(mes);
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
        /// Вывод списка выполненных задач сотрудником
        /// </summary>
        /// <param name="task"></param>
        private static void ShowTasks(SalaryReportModel report)
        {
            foreach (var task in report.Tasks)
            {
                Console.WriteLine(task.ToDisplay());
            }
            Console.WriteLine($"Всего отработано: {report.Time} часов, к выплате: {report.Salary} рублей.");

        }
        /// <summary>
        /// Вывод списка выполненных задач сотрудниками
        /// </summary>
        /// <param name="report"></param>
        private static void ShowPersonsReport(List<SalaryReportModel> report)
        {
            foreach (var item in report)
            {
                Console.WriteLine(item.Person.ToDisplay());
                ShowTasks(item);
                Console.WriteLine();
            }           
        }
    }
}
