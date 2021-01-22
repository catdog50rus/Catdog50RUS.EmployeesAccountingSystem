﻿using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository
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
        Task<bool> InsertCompletedTaskAsync(CompletedTaskLog task);
        /// <summary>
        /// Получить список всех выполненных задач
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CompletedTaskLog>> GetCompletedTasksListAsync();
        /// <summary>
        /// Получить список задач
        /// выполненных конкретным сотрудником
        /// за определенный период
        /// </summary>
        /// <param name="person"></param>
        /// <param name="beginDate"></param>
        /// <param name="lastDate"></param>
        /// <returns></returns>
        Task<IEnumerable<CompletedTaskLog>> GetEmployeeTasksListAsync(Guid personID, DateTime beginDate, DateTime lastDate);
        Task<IEnumerable<CompletedTaskLog>> GetCompletedTasksListInPeriodAsync(DateTime beginDate, DateTime lastDate);
    }
}