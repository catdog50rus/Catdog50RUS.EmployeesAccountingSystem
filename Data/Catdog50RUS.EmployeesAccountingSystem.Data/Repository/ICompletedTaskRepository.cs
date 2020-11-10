using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository
{
    /// <summary>
    /// Интерфейс доступа к данным
    /// </summary>
    public interface ICompletedTaskRepository
    {
        /// <summary>
        /// Асинхронное добавление выполненной задачи
        /// </summary>
        /// <returns></returns>
        Task<CompletedTask> AddCompletedTask(CompletedTask task);
        /// <summary>
        /// Получить список всех выполненных задач
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CompletedTask>> GetCompletedTasksList();
        /// <summary>
        /// Получить список задач
        /// выполненных конкретным сотрудником
        /// за определенный период
        /// </summary>
        /// <param name="person"></param>
        /// <param name="beginDate"></param>
        /// <param name="lastDate"></param>
        /// <returns></returns>
        Task<IEnumerable<CompletedTask>> GetPersonsTaskListAsync(Guid personID, DateTime beginDate, DateTime lastDate);
        Task<IEnumerable<CompletedTask>> GetCompletedTasksListInPeriodAsync(DateTime beginDate, DateTime lastDate);
    }
}
