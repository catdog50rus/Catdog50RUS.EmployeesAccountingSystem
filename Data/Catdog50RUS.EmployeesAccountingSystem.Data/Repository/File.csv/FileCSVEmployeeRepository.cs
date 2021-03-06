﻿using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File.csv
{
    /// <summary>
    /// Реализация репозитория сотрудников
    /// </summary>
    public class FileCSVEmployeeRepository : FileCSVBase, IEmployeeRepository
    {
        /// <summary>
        /// Хранилище данных о сотрудниках
        /// </summary>
        private static readonly string _filename = FileCSVSettings.EMPLOYEES_LIST_FILENAME;
        /// <summary>
        /// Используем конструктор базового класса
        /// В конструктор базового класса передаем имя файла с данными
        /// </summary>
        public FileCSVEmployeeRepository() : base(_filename) { }

        #region Interface

        /// <summary>
        /// Получить флаг первого запуска программы
        /// </summary>
        /// <returns></returns>
        public bool IsFirstRun() => _isFirstRun;
        /// <summary>
        /// Получить асинхронно список всех сотрудников
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BaseEmployee>> GetEmployeesListAsync()
        {
            //Создаем новый список сотрудников
            List<BaseEmployee> result = new List<BaseEmployee>();

            //Считываем все строки из файла в текстовый массив
            string[] dataLines = await base.ReadAsync();

            foreach (var line in dataLines)
            {
                //Получаем модель сотрудника в виде текстового массива
                string[] model = line.Split(DataSearator);
                //Преобразуем данные массива
                Guid.TryParse(model[0], out Guid id);
                string name = model[1];
                string surnamePerson = model[2];
                var department = (Departments)Enum.Parse(typeof(Departments), model[3]);
                var position = (Positions)Enum.Parse(typeof(Positions), model[4]);
                decimal.TryParse(model[5], out decimal salary);
                
                //Создаем нового сотрудника и в зависимости от позиции создаем тип сотрудника
                BaseEmployee employee = null;
                switch (position)
                {
                    case Positions.None:
                        employee = new StaffEmployee(id, name, surnamePerson, department, position, salary);
                        break;
                    case Positions.Developer:
                        employee = new StaffEmployee(id, name, surnamePerson, department, salary);
                        break;
                    case Positions.Director:
                        employee = new DirectorEmployee(id, name, surnamePerson, department, salary);
                        break;
                    case Positions.Freelance:
                        employee = new FreeLancerEmployee(id, name, surnamePerson, department, salary);
                        break;
                    default:
                        break;
                }

                //Если сотрудник создан, добавляем его в результирующий список
                if (employee != null)
                    result.Add(employee);
            }
            //Если результирующий список пустой, возвращаем null
            if (result.Count == 0)
                return null;

            return result;
        }
        /// <summary>
        /// Удаление сотрудника из файла данных
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BaseEmployee> DeleteEmployeeByIdAsync(Guid id)
        {
            //Получаем коллекцию всех сотрудников
            var employeesList = await GetEmployeesListAsync();
            //Находим удаляемого сотрудника по id  и проверяем, существует ли такой сотрудник
            var deleteEmployee = employeesList.FirstOrDefault(p => p.Id == id);
            if (deleteEmployee == null)
                return null;
            var result = await DeleteEmployeeAsync(employeesList, deleteEmployee);
            return result;
        }
        /// <summary>
        /// Удаление сотрудника из файла данных по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<BaseEmployee> DeleteEmployeeByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;
            //Получаем коллекцию всех сотрудников
            var employeesList = await GetEmployeesListAsync();
            //Находим удаляемого сотрудника по id  и проверяем, существует ли такой сотрудник
            var deleteEmployee = employeesList.FirstOrDefault(p => p.NamePerson.Equals(name));
            if (deleteEmployee == null)
                return null;

            var result = await DeleteEmployeeAsync(employeesList, deleteEmployee);
            return result;
        }        
        /// <summary>
        /// Получить сотрудника по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<BaseEmployee> GetEmployeeByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;
            //Получаем список всех сотрудников
            var list = await GetEmployeesListAsync();
            //Если получаем null или пустой список, возвращаем null
            if (list == null || list.Count()==0)
                return null;
            //Возвращаем результат    
            return list.FirstOrDefault(p => p.NamePerson == name);
        }
        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<BaseEmployee> InsertEmployeeAsync(BaseEmployee employee)
        {
            //Проверяем входные данные на null
            if (employee == null)
                return null;

            //Преобразуем сотрудника в строку используя модель
            string line = employee.ToFile(DataSearator);

            //Записываем данные в файл, получаем результат
            var writingResult = await base.WriteAsync(line);
            if (writingResult)
                return employee;
            else
                return null;
        }
        /// <summary>
        /// Получить сотрудника по id
        /// </summary>
        /// <returns></returns>
        public async Task<BaseEmployee> GetEmployeeByIdAsync(Guid id)
        {
            //Получаем список сотрудников и проверяем его
            var list = await GetEmployeesListAsync();
            if (list == null || list.Count() == 0)
                return null;
                
            return list.FirstOrDefault(p => p.Id == id);
         }

        #endregion

        /// <summary>
        /// Реализация удаления сотрудника
        /// </summary>
        /// <param name="employeesList"></param>
        /// <param name="deleteEmployee"></param>
        /// <returns></returns>
        private async Task<BaseEmployee> DeleteEmployeeAsync(IEnumerable<BaseEmployee> employeesList, BaseEmployee deleteEmployee)
        {
            //Создаем результирующий список и удаляем из него сотрудника
            List<BaseEmployee> result = employeesList.ToList();
            result.Remove(deleteEmployee);

            //Удаляем файл с данными сотрудников
            try
            {
                //Сохраним копию текущего файла с данными
                //Получим имя сохраненного файла
                string savefile = Path.Combine(Directory.GetCurrentDirectory(),
                                               $"{Path.GetFileNameWithoutExtension(FileName)}_save.csv");
                //Копируем текущий файл
                new FileInfo(FileName).CopyTo(savefile);
                //И удаляем его
                new FileInfo(FileName).Delete();

                //Записываем результирующий список сотрудников в новый файл
                foreach (var e in result)
                {
                    await InsertEmployeeAsync(e);
                }

                //Если ошибок не пришло удаляем временный файл
                new FileInfo(savefile).Delete();
                return deleteEmployee;

            }
            //TODO Дописать обработчик исключений
            catch (Exception)
            {
                return null;
                throw new Exception($"Ошибка блока FileCSVEmployeeRepository, метод DeleteEmployeeAsync");
            }
        }
    }
}
