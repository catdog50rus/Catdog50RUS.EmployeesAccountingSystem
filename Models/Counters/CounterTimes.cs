using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;

namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    public class CounterTimes
    {
        public Guid IdCounter { get; set; }
        public Person Person { get; set; }
        public DateTime Date { get; set; }
        public double Time { get; set; }
        public string TaskName { get; set; }

        public CounterTimes(Person person, DateTime date, double time, string task)
        {
            IdCounter = Guid.NewGuid();
            Person = person ?? throw new ArgumentNullException(person.ToString(), "Ошибка сотрудника");
            Date = (date == null)? DateTime.Today : date;
            Time = (time > 0)? time: throw new ArgumentException("Рабочее время должно быть положительным числом");
            TaskName = (!string.IsNullOrEmpty(task))? task : throw new ArgumentException("Ошибка в наименовании задачи");
        }

        public string ToFile()
        {
            return $"{IdCounter},{Date},{Person},{Time},{TaskName}";
        }

        public string ToDisplay()
        {
            return $"Дата: {Date}, Задача: {TaskName}, Затраченное время {Time}";
        }

    }
}
