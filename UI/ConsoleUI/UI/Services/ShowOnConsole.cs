using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using Catdog50RUS.EmployeesAccountingSystem.Reports.Models.SalaryReport;
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
        public static void ShowPersons(IEnumerable<BaseEmployee> collection)
        {
            Console.Clear();
            Console.WriteLine("Список сотрудников: ");
            Console.WriteLine();
            foreach (var item in collection)
            {
                var emploee = MapToEmployeeModel(item);
                ShowPerson(emploee);
            }
        }

        /// <summary>
        /// Вывод отчета по сотруднику
        /// </summary>
        /// <param name="person"></param>
        /// <param name="period"></param>
        /// <param name="report"></param>
        public static void ShowPersonTasks((DateTime, DateTime) period, SalaryReport report)
        {
            Console.Clear();
            Console.WriteLine($"Отчет по Сотруднику: {report.Employee} \nВ период с {period.Item1:dd.MM.yyyy} по {period.Item2:dd.MM.yyyy}: ");
            Console.WriteLine();
            ShowTasks(report);
            
        }
        
        /// <summary>
        /// Вывод отчета по всем сотрудникам
        /// </summary>
        /// <param name="period"></param>
        /// <param name="report"></param>
        public static void ShowAllPersonsTasks(SalaryReportPerAllEmployees report)
        {
            Console.Clear();
            Console.WriteLine(report.Header);
            ShowPersonsReport(report.EmployeeSalaryReports.ToList());
            Console.WriteLine($"Итого: отработано: {report.TotalTime} часов, к выплате: {report.TotalSalary} рублей.");
        }

        /// <summary>
        /// Вывод отчета по отделам
        /// </summary>
        /// <param name="period"></param>
        /// <param name="report"></param>
        public static void ShowDepartmetsTasks((DateTime, DateTime) period, List<(Departments, List<SalaryReport>)> report)
        {
            Console.Clear();
            Console.WriteLine($"Отчет по отделам за {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(period.Item1.Month)} месяц {period.Item1.Year} года");
            Console.WriteLine();
            foreach (var depatment in report)
            {
                Console.WriteLine($"Отдел {depatment.Item1}:");
                Console.WriteLine();
                ShowPersonsReport(depatment.Item2);
                Console.WriteLine($"Итого по отделу {depatment.Item1}:  отработано: {depatment.Item2.Sum(t => t.TotalTime)} часов, " +
                                  $"к выплате: { depatment.Item2.Sum(t => t.TotalSalary)} рублей.");
                Console.WriteLine(new string('-', 100));
                Console.WriteLine();
            }
            Console.WriteLine($"Всего по организации:  отработано: {report.Sum(t=>t.Item2.Sum(s=>s.TotalTime))} часов, " +
                              $"к выплате: {report.Sum(t => t.Item2.Sum(s => s.TotalSalary))} рублей.");
            Console.WriteLine(new string('-', 100));
        }

        /// <summary>
        /// Вывод подтверждения о добавлении сотрудника
        /// </summary>
        /// <param name="employee"></param>
        public static void ShowNewPerson(Employee employee)
        {
            Console.Clear();
            Console.WriteLine(employee.ToInsert());
            Console.WriteLine();
        }
        
        /// <summary>
        /// Вывод подтверждения об удалении сотрудника
        /// </summary>
        /// <param name="employee"></param>
        public static void ShowDeletePerson(Employee employee)
        {
            Console.Clear();
            Console.WriteLine(employee.ToDelete());
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
        public static void ShowNewTask(TaskLog task)
        {
            Console.Clear();
            Console.WriteLine(task.ToInsert());
            Console.WriteLine();
        }
        


        /// <summary>
        /// Вывод строки списка сотрудников
        /// </summary>
        /// <param name="employee"></param>
        private static void ShowPerson(Employee employee)
        {
            Console.WriteLine(employee.ToDisplay());
        }
        /// <summary>
        /// Вывод списка выполненных задач сотрудником
        /// </summary>
        /// <param name="task"></param>
        private static void ShowTasks(SalaryReport report)
        {
            foreach (var task in report.TasksLogList)
            {
                var log = MapToTaskLogModel(task);
                Console.WriteLine(log.ToDisplay());
            }
            Console.WriteLine($"Всего отработано: {report.TotalTime} часов, к выплате: {report.TotalSalary} рублей.");

        }
        /// <summary>
        /// Вывод списка выполненных задач сотрудниками
        /// </summary>
        /// <param name="report"></param>
        private static void ShowPersonsReport(List<SalaryReport> report)
        {
            foreach (var item in report)
            {
                var employee = MapToEmployeeModel(item.Employee);
                Console.WriteLine(employee.ToDisplay());
                ShowTasks(item);
                Console.WriteLine();
            }           
        }

        private static Employee MapToEmployeeModel(BaseEmployee e)
        {
            return new Employee
            {
                Id = e.Id,
                NamePerson = e.NamePerson,
                SurnamePerson = e.SurnamePerson,
                Department = e.Department,
                Positions = e.Positions,
                BaseSalary = e.BaseSalary
            };
        }

        private static TaskLog MapToTaskLogModel(CompletedTaskLog log)
        {
            return new TaskLog
            {
                Date = log.Date,
                IdEmployee = log.IdEmployee,
                TaskName = log.TaskName,
                Time = log.Time
            };
        }
    }
}
