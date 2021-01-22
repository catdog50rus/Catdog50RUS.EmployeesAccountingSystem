using Catdog50RUS.EmployeesAccountingSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    //к удалению
    public class SalaryReportModel
    {
        public Person Person { get; set; }
        public double Time { get; set; }
        public decimal Salary { get; set; }
        public List<CompletedTaskLog> Tasks { get; set; }

        public SalaryReportModel() { }

        public SalaryReportModel(double time, decimal salary, List<CompletedTaskLog>tasks)
        {
            Time = time;
            Salary = salary;
            Tasks = tasks;
        }

        public SalaryReportModel(Person person, double time, decimal salary, List<CompletedTaskLog> tasks) : this(time,salary,tasks)
        {
            Person = person;
        }
    }
}
