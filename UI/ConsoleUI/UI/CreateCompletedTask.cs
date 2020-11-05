using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    /// <summary>
    /// Компонент UI
    /// Получаем новую задачу
    /// </summary>
    class CreateCompletedTask
    {
        public static CompletedTask CreatNewTask(Person person)
        {
            Console.Clear();
            //Получаем данные от пользователя используя компоненты UI
            string date = DateTime.Today.ToString("dd.MM.yyyy");
            string taskName = InputParameters.InputStringParameter("Введите наименование задачи");
            double time = InputParameters.InputDoubleParameter("Введите затраченное время в часах (например: 3,5)");
            //Возвращаем задачу
            return new CompletedTask(person, date, time, taskName);
        }
    }
}
