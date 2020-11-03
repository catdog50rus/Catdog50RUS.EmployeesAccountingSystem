using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetPersonsListAsync();
        Task<Person> InsertPerson(Person person);
        Task<Person> DeletePerson(Guid id);
        Task<Person> GetPersonByNameAsync(string name);
        Task<Person> GetPersonByIdAsync(Guid name);
    }
}
