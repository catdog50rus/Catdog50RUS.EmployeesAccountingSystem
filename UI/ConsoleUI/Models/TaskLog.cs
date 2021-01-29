using System;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.Models
{
    /// <summary>
    /// DTO TaskLog
    /// </summary>
    public class TaskLog
    {
        public Guid IdEmployee { get; set; }
        public DateTime Date { get; set; }
        public double Time { get; set; }
        public string TaskName { get; set; }

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
