using Catdog50RUS.EmployeesAccountingSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetPersonsListAsync();
        Task<Person> InsertPerson(Person person);
        Person DeletePerson(string name);
        Task<Person> GetPersonByNameAsync(string name);
    }
}
