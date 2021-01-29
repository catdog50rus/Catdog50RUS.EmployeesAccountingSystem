using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using Catdog50RUS.EmployeesAccountingSystem.Reports.Models.SalaryReport;
using System;
using System.Collections.Generic;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Services
{
    /// <summary>
    /// Реализация вывода данных на консоль
    /// </summary>
    static class ShowOnConsole
    {
        /// <summary>
        /// Вывод списка сотрудников
        /// </summary>
        /// <param name="employeesList"></param>
        public static void ShowEmployeesList(IEnumerable<BaseEmployee> employeesList)
        {
            ShowMessage("Список сотрудников: ");
            Console.WriteLine();
            foreach (var item in employeesList)
            {
                var employee = item.ToEmployeeModel();
                Console.WriteLine(employee.ToDisplay());
            }
        }

        //Вывод отчетов
        /// <summary>
        /// Вывод отчета по сотруднику
        /// </summary>
        /// <param name="report"></param>
        public static void ShowEmployeeSalaryReport(this EmployeeSalaryReport report)
        {
            Console.WriteLine(new string('-', 75));
            Console.WriteLine(report.Header);
            ShowTaskLogsInReport(report.TasksLogList);
            Console.WriteLine(new string('-', 75));
            Console.WriteLine($"Всего сотрудником {report.Employee} отработано: {report.TotalTime} часов, к выплате: {report.TotalSalary} рублей.");
        }
        /// <summary>
        /// Вывод отчета по всем сотрудникам
        /// </summary>
        /// <param name="period"></param>
        /// <param name="report"></param>
        public static void ShowAllEmployeeSalaryReport(this ExtendedSalaryReportAllEmployees report)
        {
            Console.WriteLine(report.Header);
            foreach (var item in report.EmployeeSalaryReports)
            {
                ShowEmployeeSalaryReport(item);
            }
            Console.WriteLine(new string('-', 75));
            Console.WriteLine($"Итого сотрудниками отработано: {report.TotalTime} часов, к выплате: {report.TotalSalary} рублей.");
            Console.WriteLine();
        }
        /// <summary>
        /// Вывод отчета по отделам
        /// </summary>
        /// <param name="period"></param>
        /// <param name="report"></param>
        public static void ShowDepartmetsSalaryReport(this ExtendedSalaryReportAllDepatments report)
        {
            Console.WriteLine(report.Header);

            foreach (var item in report.EmployeeSalaryReports)
            {
                ShowAllEmployeeSalaryReport(item);
                Console.WriteLine(new string('-', 75));
            }
            Console.WriteLine($"Всего по организации:  отработано: {report.TotalTime} часов, " +
                              $"к выплате: {report.TotalSalary} рублей.");
            Console.WriteLine(new string('-', 75));
        }
        /// <summary>
        /// Вывод списка выполненных задач сотрудником
        /// </summary>
        /// <param name="task"></param>
        private static void ShowTaskLogsInReport(this IEnumerable<CompletedTaskLog> taskLogsList)
        {
            foreach (var task in taskLogsList)
            {
                var log = task.ToTaskLogModel();
                Console.WriteLine(log.ToDisplay());
            }
        }

        //Вывод сообщений
        /// <summary>
        /// Вывод подтверждения о добавлении сотрудника
        /// </summary>
        /// <param name="employee"></param>
        public static void ShowInsertNewEmployeeMessage(Employee employee)
        {
            ShowMessage(employee.ToInsert());
            Console.WriteLine();
        }      
        /// <summary>
        /// Вывод подтверждения об удалении сотрудника
        /// </summary>
        /// <param name="employee"></param>
        public static void ShowDeleteEmployeeMessage(Employee employee)
        {
            ShowMessage(employee.ToDelete());
            Console.WriteLine();
        }
        /// <summary>
        /// Вывод подтверждения о добавлении задачи
        /// </summary>
        /// <param name="task"></param>
        public static void ShowInsertNewTaskMessage(TaskLog task)
        {
            ShowMessage(task.ToInsert());
            Console.WriteLine();
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
        /// Вывод уведомления о нажатии клавиши для продолжения
        /// </summary>
        public static void ShowContinue()
        {
            Console.WriteLine();
            Console.WriteLine("Для продолжения нажмите любую клавишу");
            Console.ReadKey();
        }
        
    }
}
