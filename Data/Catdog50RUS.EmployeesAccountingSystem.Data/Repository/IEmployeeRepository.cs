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
        Task<IEnumerable<EmployeesBase>> GetEmployeesListAsync();
        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        Task<EmployeesBase> InsertEmployeeAsync(EmployeesBase employee);
        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EmployeesBase> DeleteEmployeeAsync(Guid id);
        /// <summary>
        /// Получить сотрудника по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<EmployeesBase> GetEmployeeByNameAsync(string name);
        /// <summary>
        /// Получить сотрудника по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<EmployeesBase> GetEmployeeByIdAsync(Guid id);
    }
}
