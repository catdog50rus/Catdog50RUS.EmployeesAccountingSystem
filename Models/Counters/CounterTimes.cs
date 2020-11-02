using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Globalization;

namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    public class CounterTimes
    {
        public Guid IdCounter { get; set; }
        public Person Person { get; set; }
        public DateTime Date { get; set; }
        public double Time { get; set; }
        public string TaskName { get; set; }

        public CounterTimes() { }

        public CounterTimes(Guid id, string date, double time, string task) : this(date, time, task)
        {
            IdCounter = id;
        }

        public CounterTimes(string date, double time, string task)
        {
            IdCounter = Guid.NewGuid();

            if (!string.IsNullOrEmpty(date))
            {
                DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime tryDate);
                Date = tryDate;
            }
            else 
                Date = DateTime.Today;

            Time = (time > 0) ? time : throw new ArgumentException("Рабочее время должно быть положительным числом");
            TaskName = (!string.IsNullOrEmpty(task)) ? task : throw new ArgumentException("Ошибка в наименовании задачи");
        }

        public CounterTimes(Person person, string date, double time, string task) : this(date, time, task)
        {
            Person = person ?? throw new ArgumentNullException(person.ToString(), "Ошибка сотрудника");
        }

        public string ToFile()
        {
            return $"{IdCounter};{Date};{Person.IdPerson};{Time};{TaskName}";
        }

        public string ToDisplay()
        {
            return $"Дата: {Date:dd.MM.yyyy}, Затраченное время {Time} часов: {TaskName}";
        }

    }
}
