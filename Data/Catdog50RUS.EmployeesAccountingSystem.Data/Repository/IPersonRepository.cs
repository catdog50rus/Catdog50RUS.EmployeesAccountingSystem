using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository
{
    /// <summary>
    /// Интерфейс доступа к данным сотрудников
    /// </summary>
    public interface IPersonRepository
    {
        /// <summary>
        /// Получить асинхронно список всех сотрудников
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Person>> GetPersonsListAsync();
        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        Task<Person> InsertPerson(Person person);
        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Person> DeletePerson(Guid id);
        /// <summary>
        /// Получить сотрудника по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<Person> GetPersonByNameAsync(string name);
        /// <summary>
        /// Получить сотрудника по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Person> GetPersonByIdAsync(Guid id);
    }
}
