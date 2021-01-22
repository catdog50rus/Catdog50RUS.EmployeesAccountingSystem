using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catdog50RUS.EmployeesAccountingSystem.Models.Reports
{
    public class SalaryReport
    {
        public DateTime FirstDate { get; }
        public DateTime LastDate { get; }
        public BaseEmployee Employee { get; }
        public List<CompletedTaskLog> TasksList { get; }
        public double TotalTime { get; }
        public decimal TotalSamary { get; set; }

        public SalaryReport(List<CompletedTaskLog> tasksList)
        {
            TasksList = tasksList;
            TotalTime = tasksList.Sum(time => time.Time);

        }

        public SalaryReport(BaseEmployee employee, List<CompletedTaskLog> tasksList) : this(tasksList)
        {
            Employee = employee;
        }
    }
}
