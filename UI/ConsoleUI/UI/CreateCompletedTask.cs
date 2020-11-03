using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    class CreateCompletedTask
    {
        public static CompletedTask CreatNewTask(Person person)
        {
            Console.Clear();

            string date = DateTime.Today.ToString("dd.MM.yyyy");
            string taskName = InputParameters.InputStringParameter("Введите наименование задачи");
            double time = InputParameters.InputDoubleParameter("Введите затраченное время в часах (например: 3,5)");

            return new CompletedTask(person, date, time, taskName);
        }
    }
}
