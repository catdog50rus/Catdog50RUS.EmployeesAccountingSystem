using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Models.Counters
{
    public class CountersTime
    {
        public Person Person { get; set; }
        public DateTime Date { get; set; }
        public double Time { get; set; }
        public string TaskName { get; set; }

        public CountersTime(Person person, DateTime date, double time, string task)
        {
            Person = person ?? throw new ArgumentNullException(person.ToString(), "Ошибка сотрудника");
            Date = (date == null)? DateTime.Today : date;
            Time = (time > 0)? time: throw new ArgumentException("Рабочее время должно быть положительным числом");
            TaskName = (!string.IsNullOrEmpty(task))? task : throw new ArgumentException("Ошибка в наименовании задачи");
        }

    }
}
