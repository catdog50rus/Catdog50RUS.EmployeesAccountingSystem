using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Globalization;

namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    public class CompletedTask
    {
        public Guid IdTask { get; set; }
        public Person Person { get; set; }
        public DateTime Date { get; set; }
        public double Time { get; set; }
        public string TaskName { get; set; }

        public CompletedTask() { }

        public CompletedTask(Guid id, string date, double time, string task) : this(date, time, task)
        {
            IdTask = id;
        }

        public CompletedTask(string date, double time, string task)
        {
            IdTask = Guid.NewGuid();

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

        public CompletedTask(Person person, string date, double time, string task) : this(date, time, task)
        {
            Person = person ?? throw new ArgumentNullException(person.ToString(), "Ошибка сотрудника");
        }



        public string ToFile()
        {
            return $"{IdTask};{Date};{Person.IdPerson};{Time};{TaskName}";
        }

        public string ToDisplay()
        {
            return $"Дата: {Date:dd.MM.yyyy}, Затраченное время {Time} часов: {TaskName}";
        }

        public string ToInsert()
        {
            return $"Добавлена выполненная задача: {TaskName} \n, Дата выполнения: {Date:dd.MM.yyyy} \n , Время выполнения:{Time} часов";
        }

    }
}
