using Catdog50RUS.EmployeesAccountingSystem.Models;
using System.Collections.Generic;

namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    public class SalaryReportModel
    {
        public Person Person { get; set; }
        public double Time { get; set; }
        public decimal Salary { get; set; }
        public List<CompletedTask> Tasks { get; set; }

        public SalaryReportModel() { }

        public SalaryReportModel(double time, decimal salary, List<CompletedTask>tasks)
        {
            Time = time;
            Salary = salary;
            Tasks = tasks;
        }

        public SalaryReportModel(Person person, double time, decimal salary, List<CompletedTask> tasks) : this(time,salary,tasks)
        {
            Person = person;
        }
    }
}
