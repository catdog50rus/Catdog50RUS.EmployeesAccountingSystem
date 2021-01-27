using System;

namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    public class CompletedTaskLog
    {
        public Guid IdTask { get; set; }
        public Guid IdEmployee { get; set; }
        public DateTime Date { get; set; }
        public double Time { get; set; }
        public string TaskName { get; set; }

        //Временное решение
        //public CompletedTask() { }

        public CompletedTaskLog(DateTime date, double time, string task)
        {
            IdTask = Guid.NewGuid();
            Date = date;
            Time = time;
            TaskName = task;
        }

        public CompletedTaskLog(Guid idEmployee, DateTime date, double time, string task) : this(date, time, task)
        {
            IdEmployee = idEmployee;
        }

        public CompletedTaskLog(Guid id, Guid idEmployee, DateTime date, double time, string task) : this(idEmployee, date, time, task)
        {
            IdTask = id;
            
        }





        public string ToFile(char dataSeparator)
        {
            return $"{IdTask}{dataSeparator}" +
                   $"{Date}{dataSeparator}" +
                   $"{IdEmployee}{dataSeparator}" +
                   $"{Time}{dataSeparator}" +
                   $"{TaskName}{dataSeparator}";
        }

        

    }
}
