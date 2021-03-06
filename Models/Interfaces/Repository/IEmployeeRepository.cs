﻿using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Получить флаг первого запуска приложения
        /// </summary>
        /// <returns></returns>
        public bool IsFirstRun();
        /// <summary>
        /// Получить асинхронно список всех сотрудников
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BaseEmployee>> GetEmployeesListAsync();
        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        Task<BaseEmployee> InsertEmployeeAsync(BaseEmployee employee);
        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BaseEmployee> DeleteEmployeeByIdAsync(Guid id);
        /// <summary>
        /// Удалить сотрудника по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<BaseEmployee> DeleteEmployeeByNameAsync(string name);
        /// <summary>
        /// Получить сотрудника по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<BaseEmployee> GetEmployeeByNameAsync(string name);
        /// <summary>
        /// Получить сотрудника по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BaseEmployee> GetEmployeeByIdAsync(Guid id);
    }
}
