using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System;

namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    public class CompletedTask
    {
        public Guid IdTask { get; set; }
        public BaseEmployee Employee { get; set; }
        public DateTime Date { get; set; }
        public double Time { get; set; }
        public string TaskName { get; set; }

        public CompletedTask() { }

        public CompletedTask(Guid id, DateTime date, double time, string task) : this(date, time, task)
        {
            IdTask = id;
        }

        public CompletedTask(DateTime date, double time, string task)
        {
            IdTask = Guid.NewGuid();

            Date = date;

            Time = (time > 0) ? time : throw new ArgumentException("Рабочее время должно быть положительным числом");
            TaskName = (!string.IsNullOrEmpty(task)) ? task : throw new ArgumentException("Ошибка в наименовании задачи");
        }

        public CompletedTask(BaseEmployee employee, DateTime date, double time, string task) : this(date, time, task)
        {
            Employee = employee ?? throw new ArgumentNullException(employee.ToString(), "Ошибка сотрудника");
        }



        public string ToFile()
        {
            return $"{IdTask};{Date};{Employee.Id};{Time};{TaskName}";
        }

        public string ToDisplay()
        {
            return $"Дата: {Date:dd.MM.yyyy}, Затраченное время {Time} часов: {TaskName}";
        }

        public string ToInsert()
        {
            return $"Добавлена выполненная задача: {TaskName},\n Дата выполнения: {Date:dd.MM.yyyy},\n Время выполнения: {Time} часов";
        }

    }
}
