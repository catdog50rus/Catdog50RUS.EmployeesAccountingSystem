using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    /// <summary>
    /// Интерфейс доступа к данным
    /// </summary>
    public interface ICompletedTasksLogRepository
    {
        /// <summary>
        /// Асинхронное добавление выполненной задачи
        /// </summary>
        /// <returns></returns>
        Task<CompletedTaskLog> InsertCompletedTaskAsync(CompletedTaskLog task);
        /// <summary>
        /// Получить список задач
        /// выполненных конкретным сотрудником
        /// за определенный период
        /// </summary>
        /// <param name="person"></param>
        /// <param name="beginDate"></param>
        /// <param name="lastDate"></param>
        /// <returns></returns>
        Task<IEnumerable<CompletedTaskLog>> GetCompletedTasksListByEmployeeAsync(Guid personID, DateTime beginDate, DateTime lastDate);
        /// <summary>
        /// Получить список всех задач
        /// за определенный период
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="lastDate"></param>
        /// <returns></returns>
        Task<IEnumerable<CompletedTaskLog>> GetCompletedTasksListInPeriodAsync(DateTime beginDate, DateTime lastDate);
    }
}
