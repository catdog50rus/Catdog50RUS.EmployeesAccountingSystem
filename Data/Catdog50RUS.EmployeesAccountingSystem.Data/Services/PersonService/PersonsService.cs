using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Services
{
    /// <summary>
    /// Реализация бизнес логики
    /// Работа хранилищем сотрудников
    /// </summary>
    public class PersonsService : IPersons
    {
        /// <summary>
        /// Внедрение зависимости через интерфейс
        /// </summary>
        private IPersonRepository PersonRepository { get; }
        /// <summary>
        /// Конструктор
        /// </summary>
        public PersonsService()
        {
            PersonRepository = new FilePersonRepository();
        }

        #region Interface

        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public async Task<bool> InsertPersonAsync(Person person)
        {
            //Проверяем входные параметры на null
            if (person != null)
            {
                //Пытаемся добавить сотрудника в хранилище, 
                //если результат не null возвращаем true, иначе false
                var result = await PersonRepository.InsertPerson(person);
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
            return await PersonRepository.GetPersonsListAsync();
        }

        /// <summary>
        /// Получить сотрудника по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Person> GetPersonByName(string name)
        {
            return await PersonRepository.GetPersonByNameAsync(name);
        }

        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeletePersonAsync(Guid id)
        {
            //Пробуем удалить сотрудника из хранилища
            var result = await PersonRepository.DeletePerson(id);
            //Если результат не null возвращаем true, иначе false
            if (result != null)
                return true;
            else
                return false;
        }

        #endregion


    }
}
