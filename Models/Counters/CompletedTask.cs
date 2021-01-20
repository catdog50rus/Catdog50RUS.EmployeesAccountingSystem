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
        //public CompletedTask() { }

        public CompletedTask(DateTime date, double time, string task)
        {
            IdTask = Guid.NewGuid();
            Date = date;
            Time = time;
            TaskName = task;
        }

        public CompletedTask(Guid idEmployee, DateTime date, double time, string task) : this(date, time, task)
        {
            IdEmployee = idEmployee;
        }

        public CompletedTask(Guid id, Guid idEmployee, DateTime date, double time, string task) : this(idEmployee, date, time, task)
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
