﻿using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
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

        public bool IsFirstRun { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public EmployeeService(IEmployeeRepository repository)
        {
            _employeeRepository = repository;
            //IsFirstRun = _employeeRepository.IsFirstRun;
        }

        #region Interface

        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<bool> InsertEmployeeAsync(BaseEmployee employee)
        {
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
            return await _employeeRepository.GetEmployeesListAsync();
        }

        /// <summary>
        /// Получить сотрудника по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<BaseEmployee> GetEmployeeByName(string name)
        {
            return await _employeeRepository.GetEmployeeByNameAsync(name);
        }

        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            //Пробуем удалить сотрудника из хранилища
            var result = await _employeeRepository.DeleteEmployeeAsync(id);
            //Если результат не null возвращаем true, иначе false
            if (result != null)
                return true;
            else
                return false;
        }

        #endregion

    }
}
