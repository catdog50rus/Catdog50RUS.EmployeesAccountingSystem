using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Services
{
    /// <summary>
    /// Реализация бизнес логики
    /// Получение списка задач
    /// </summary>
    public class CompletedTasksLogsService : ICompletedTaskLogs
    {
        /// <summary>
        /// Внедрение зависимости через интерфейс
        /// </summary>
        private readonly ICompletedTasksLogRepository _tasksRepository;
        private readonly Autorize _autorize;

        /// <summary>
        /// Конструктор
        /// </summary>
        public CompletedTasksLogsService(ICompletedTasksLogRepository repository, Autorize autorize)
        {
            _tasksRepository = repository;
            if (autorize != null)
                _autorize = autorize;
        }

        #region Interface

        public CompletedTask CreateNewTask(DateTime date, BaseEmployee employee, string taskname, double time)
        {
            //Первичная валидация данных
            if (employee == null ||
                string.IsNullOrWhiteSpace(taskname) ||
                time <= 0 || time > 24 ||
                date > DateTime.Now.AddDays(1))
            {
                return null;
            }

            //Внедрение ограничений по ролям пользователей
            bool isValid = default;

            switch (_autorize.UserRole)
            {
                case Role.Freelancer: //Может создавать логи для себя, при этом дата лога не может быть старше чем за 2 дня
                    isValid = _autorize.UserId.Equals(employee.Id) && 
                              (date.Date >= DateTime.Now.Date.AddDays(-2));
                    break;
                case Role.Developer: //Может создавать логи только за себя
                    isValid = _autorize.UserId.Equals(employee.Id);
                    break;
                case Role.Director:
                    isValid = true;
                    break;
                default:
                    break;
                    
            }
            if (!isValid)
                return null;

            return new CompletedTask
            {
                IdTask = Guid.NewGuid(),
                Date = date,
                IdEmployee = employee.Id,
                TaskName = taskname,
                Time = time
            };
        }

        /// <summary>
        /// Добавить новую задачу
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<bool> AddNewTaskLog(CompletedTask task)
        {
            //Проверяем входные параметры на null
            if (task == null)
                return false;

            //Пытаемся добавить задачу в хранилище, 
            //если результат не null возвращаем true, иначе false
            var result = await _tasksRepository.InsertCompletedTaskAsync(task);
            if (result)
                return true;
            else
                return false;

        }
        /// <summary>
        /// Получить выполненные задачи пользователя
        /// За определенный период
        /// </summary>
        /// <param name="person"></param>
        /// <param name="startday"></param>
        /// <param name="stopday"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CompletedTask>> GetEmployeeTaskLogs(Guid personID, DateTime startday, DateTime stopday)
        {
            if (ValidateData(startday, stopday))
                return await _tasksRepository.GetEmployeeTasksListAsync(personID, startday, stopday);
            else
                return null;
        }
        public async Task<IEnumerable<CompletedTask>> GetCompletedTaskLogs(DateTime startday, DateTime stopday)
        {
            if (ValidateData(startday, stopday))
                return await _tasksRepository.GetCompletedTasksListInPeriodAsync(startday, stopday);
            else
                return null;
        }
        
        #endregion
        
        /// <summary>
        /// Валидация данных
        /// </summary>
        /// <param name="startday"></param>
        /// <param name="stopday"></param>
        /// <returns></returns>
        private bool ValidateData(DateTime startday, DateTime stopday)
        {
            //Проверяем, чтобы начальная дата была не больше конечной даты
            if (startday > stopday)
                return false;
            //Проверяем, чтобы конечная дата была не больше, чем текущая плюс 1 год
            if (stopday > DateTime.Now.AddYears(1))
                return false;
            
            return true;
        }


    }
}
