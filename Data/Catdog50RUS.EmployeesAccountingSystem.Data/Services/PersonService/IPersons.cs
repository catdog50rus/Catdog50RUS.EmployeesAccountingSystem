using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Services
{
    public interface IPersons
    {
        public bool IsFirstRun { get; }

        Task<bool> InsertPersonAsync(Person person);
        Task<IEnumerable<Person>> GetAllPersonsAsync();
        Task<Person> GetPersonByName(string name);
        Task<bool> DeletePersonAsync(Guid id);
    }
}
