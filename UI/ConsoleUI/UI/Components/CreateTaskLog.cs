using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.Models;
using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Components
{
    /// <summary>
    /// Компонент UI
    /// Получаем новую задачу
    /// </summary>
    class CreateTaskLog
    {
        /// <summary>
        /// Внедрение авторизации
        /// </summary>
        private readonly Autorize _autorize;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="autorize"></param>
        public CreateTaskLog(Autorize autorize)
        {
            _autorize = autorize;
        }
        /// <summary>
        /// Создать DTO TaskLog
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TaskLog CreatNewTask(Guid id)
        {
            Console.Clear();
            var role = _autorize.UserRole;
            //Получаем данные от пользователя используя компоненты UI

            //Проверяем, чтобы введенная дата не была будущей 
            DateTime date = DateTime.Today.AddDays(1);
            while(date > DateTime.Today)
            {
                date = InputParameters.InputDateParameter("Введите дату выполнения задачи");
                //Если пользователь фрилансер, проверяем, чтобы дата была не позднее,
                //чем за два дня до сегодняшней
                if (role.Equals(Role.Freelancer))
                {
                    while (date < DateTime.Today.AddDays(-2))
                    {
                        ShowOnConsole.ShowMessage($"Сотрудник фрилансер не может добавлять дату задачи старше чем {DateTime.Today.AddDays(-2):dd.MM.yyyy}");
                        date = InputParameters.InputDateParameter("Введите дату выполнения задачи");
                    }
                }
                    
            }

            string taskName = InputParameters.InputStringParameter("Введите наименование задачи");
            double time = InputParameters.InputDoubleParameter("Введите затраченное время в часах (например: 3,5)");
            //Возвращаем новую задачу DTO
            return new TaskLog
            {
                Date = date,
                IdEmployee = id,
                TaskName = taskName,
                Time = time
            }; 
        }
    }
}
