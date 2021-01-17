using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System.Collections.Generic;

namespace Catdog50RUS.EmployeesAccountingSystem.Models.Reports
{
    public class SalaryReport
    {
        public BaseEmployee Employee { get; set; }
        public double Time { get; set; }
        public decimal Salary { get; set; }
        public List<CompletedTask> Tasks { get; set; }

        public SalaryReport(double time, decimal salary, List<CompletedTask> tasks)
        {
            Time = time;
            Salary = salary;
            Tasks = tasks;
        }

        public SalaryReport(BaseEmployee employee, double time, decimal salary, List<CompletedTask> tasks) : this(time, salary, tasks)
        {
            Employee = employee;
        }
    }
}
