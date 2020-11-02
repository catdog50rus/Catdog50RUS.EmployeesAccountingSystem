using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.Controllers
{
    public class PersonsController
    {
        private readonly IPersonRepository _personRepository;

        public PersonsController()
        {
            _personRepository = new FilePersonRepository();
        }

        public async Task<Person> Authorization(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var person = await _personRepository.GetPersonByNameAsync(name);
                if (person != null)
                    return person;
            }
            return null;
        }

        public async Task<bool> InsertPersonAsync(Person person)
        {
            if (person != null)
            {
                var result = await _personRepository.InsertPerson(person);
                if (result != null)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public async Task<List<Person>> GetAllPersonsAsync()
        {
            var persons = await _personRepository.GetPersonsListAsync();
            return persons.ToList();
        }
    }
}
