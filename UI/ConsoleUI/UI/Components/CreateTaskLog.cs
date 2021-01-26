using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.Models;
using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Services;
using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Components
{
    /// <summary>
    /// Компонент UI
    /// Получаем новую задачу
    /// </summary>
    class CreateTaskLog
    {
        private readonly ICompletedTaskLogsService _taskLogsService;
        private readonly Autorize _autorize;

        public CreateTaskLog(ICompletedTaskLogsService taskLogsService, Autorize autorize)
        {
            _taskLogsService = taskLogsService;
            _autorize = autorize;
        }

        public TaskLog CreatNewTask(BaseEmployee employee)
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
            //Возвращаем задачу
            return new TaskLog
            {
                Date = date,
                IdEmployee = employee.Id,
                TaskName = taskName,
                Time = time
            }; 
        }

        
    }
}
