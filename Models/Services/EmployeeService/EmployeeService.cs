using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Services.EmployeeService
{

    public class EmployeeService : IEmployeeService
    {
        /// <summary>
        /// Внедрение зависимости через интерфейс
        /// </summary>
        private readonly IEmployeeRepository _employeeRepository;
        private readonly Autorize _autorize;

        /// <summary>
        /// Конструктор
        /// </summary>
        public EmployeeService(IEmployeeRepository repository, Autorize autorize)
        {
            _employeeRepository = repository;

            if (autorize.UserRole == Role.Director)
                _autorize = autorize;
        }

        #region Interface

        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<bool> InsertEmployeeAsync(BaseEmployee employee)
        {
            //Проверяем права доступа
            if (_autorize == null)
                return false;
            //Проверяем входные параметры на null
            if (employee == null)
                return false;

            //Проверяем сотрудника на уникальность
            var employeesList = await GetAllEmployeeAsync();
            if (employeesList.ToList().Contains(employee))
                return false;

            //Пытаемся добавить сотрудника в хранилище, 
            //если результат не null возвращаем true, иначе false
            var result = await _employeeRepository.InsertEmployeeAsync(employee);
            if (result != null)
                return true;
            else
                return false;


        }

        /// <summary>
        /// Получить всех сотрудников
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BaseEmployee>> GetAllEmployeeAsync()
        {
            //Проверяем права доступа
            if (_autorize == null)
                return null;
            return await _employeeRepository.GetEmployeesListAsync();
        }

        /// <summary>
        /// Получить сотрудника по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<BaseEmployee> GetEmployeeByNameAsync(string name)
        {
            //Проверяем права доступа
            if (_autorize == null)
                return null;
            //Проверяем входной параметр на пустоту и null
            if (string.IsNullOrWhiteSpace(name))
                return null;

            return await _employeeRepository.GetEmployeeByNameAsync(name);
        }

        public async Task<BaseEmployee> GetEmployeeByIdAsync(Guid id)
        {
            //Проверяем права доступа
            if (_autorize == null)
                return null;

            return await _employeeRepository.GetEmployeeByIdAsync(id);
        }

        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            //Проверяем права доступа
            if (_autorize == null)
                return false;
            //Пробуем удалить сотрудника из хранилища
            var result = await _employeeRepository.DeleteEmployeeByIdAsync(id);
            //Если результат не null возвращаем true, иначе false
            if (result != null)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Удалить сотрудника по имени
        /// </summary>
        public async Task<bool> DeleteEmployeeByNameAsync(string name)
        {
            //Проверяем права доступа
            if (_autorize == null)
                return false;
            //Проверяем имя на пустое значение или null
            if (string.IsNullOrWhiteSpace(name))
                return false;
            
            //Пробуем удалить сотрудника из хранилища
            var result = await _employeeRepository.DeleteEmployeeByNameAsync(name);
            //Если результат не null возвращаем true, иначе false
            if (result != null)
                return true;
            else
                return false;
        }

        #endregion

    }
}
