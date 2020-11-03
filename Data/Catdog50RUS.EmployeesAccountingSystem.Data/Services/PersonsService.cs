using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Services
{
    public class PersonsService
    {
        private readonly IPersonRepository _personRepository;

        public PersonsService()
        {
            _personRepository = new FilePersonRepository();
        }

        #region Interface

        /// <summary>
        /// Авторизация по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Получить всех сотрудников
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            var persons = await _personRepository.GetPersonsListAsync();
            return persons.ToList();
        }

        public async Task<Person> GetPersonByName(string name)
        {
            return await _personRepository.GetPersonByNameAsync(name);
        }

        public async Task<bool> DeletePersonAsync(Guid id)
        {
            var result = await _personRepository.DeletePerson(id);
            if (result != null)
                return true;
            else
                return false;
        }

        #endregion


    }
}
