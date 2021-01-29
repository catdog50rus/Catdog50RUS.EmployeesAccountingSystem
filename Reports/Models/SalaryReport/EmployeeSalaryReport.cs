using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catdog50RUS.EmployeesAccountingSystem.Reports.Models.SalaryReport
{
    /// <summary>
    /// Реализация модели отчета по сотруднику
    /// </summary>
    public class EmployeeSalaryReport
    {
        public string Header { get; set; }
        public DateTime FirstDate { get; }
        public DateTime LastDate { get; }
        public BaseEmployee Employee { get; }
        public IEnumerable<CompletedTaskLog> TasksLogList { get; }
        public double TotalTime { get; }
        public decimal TotalSalary { get; set; }

        public EmployeeSalaryReport(DateTime firstDate, DateTime lastDate, BaseEmployee employee,
                            IEnumerable<CompletedTaskLog> tasksLoagList)
        {
            if (employee is null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            if (tasksLoagList is null)
            {
                throw new ArgumentNullException(nameof(tasksLoagList));
            }

            FirstDate = firstDate;
            LastDate = lastDate;
            Employee = employee;
            TasksLogList = tasksLoagList;
            TotalTime = tasksLoagList.Sum(x => x.Time);
        }

    }


}
