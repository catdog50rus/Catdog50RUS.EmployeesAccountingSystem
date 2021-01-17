using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository
{
    public interface IEmployeeRepository
    {
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
        Task<BaseEmployee> DeleteEmployeeAsync(Guid id);
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
