using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System;

namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    public class CompletedTask
    {
        public Guid IdTask { get; set; }
        public Guid IdEmployee { get; set; }
        public DateTime Date { get; set; }
        public double Time { get; set; }
        public string TaskName { get; set; }

        //Временное решение
        public CompletedTask() { }

        public CompletedTask(DateTime date, double time, string task)
        {
            IdTask = Guid.NewGuid();

            Date = date;
            Time = (time > 0) ? time : throw new ArgumentException("Рабочее время должно быть положительным числом");
            TaskName = (!string.IsNullOrEmpty(task)) ? task : throw new ArgumentException("Ошибка в наименовании задачи");
        }

        public CompletedTask(BaseEmployee employee, DateTime date, double time, string task) : this(date, time, task)
        {
            IdEmployee = employee != null? employee.Id: throw new ArgumentNullException(employee.ToString(), "Ошибка сотрудника");
        }

        public CompletedTask(Guid id, BaseEmployee employee, DateTime date, double time, string task) : this(employee, date, time, task)
        {
            IdTask = id;
            
        }





        public string ToFile()
        {
            return $"{IdTask};{Date};{IdEmployee};{Time};{TaskName}";
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
