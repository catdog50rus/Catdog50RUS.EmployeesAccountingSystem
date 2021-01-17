using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File.txt;
using Catdog50RUS.EmployeesAccountingSystem.Models;
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
        /// <summary>
        /// Конструктор
        /// </summary>
        public CompletedTasksLogsService(ICompletedTasksLogRepository repository)
        {
            _tasksRepository = repository;
        }

        #region Interface

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
            if (result != null)
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
