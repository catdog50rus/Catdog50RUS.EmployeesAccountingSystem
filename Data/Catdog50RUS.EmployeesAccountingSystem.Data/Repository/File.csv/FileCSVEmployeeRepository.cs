using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File.csv
{
    public class FileCSVEmployeeRepository : FileCSVBase, IEmployeeRepository
    {
        /// <summary>
        /// Хранилище данных о сотрудниках
        /// </summary>
        private static readonly string filename = FileCSVSettings.PERSONSFILENAME;
        /// <summary>
        /// Используем конструктор базового класса
        /// В конструктор базового класса передаем имя файла с данными
        /// </summary>
        public FileCSVEmployeeRepository() : base(filename) { }


        #region Interface

        /// <summary>
        /// Получить асинхронно список всех сотрудников
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EmployeesBase>> GetEmployeesListAsync()
        {
            //Создаем новый список сотрудников
            List<EmployeesBase> dto = new List<EmployeesBase>();

            //Процесс получения данных оборачиваем в блок try,
            //чтобы отловить исключения как по доступу к файлу
            try
            {
                //Создаем экземпляр класса StreamReader, 
                //передаем в него полное имя файла с данными
                using StreamReader sr = new StreamReader(FileName);
                string[] dataLines = null;

                //Считываем данные из файла
                var data = await sr.ReadToEndAsync();

                //Объявляем строковый массив и передаем в него строку с данными
                //Массив заполняется данными, каждый элемент массива разделяется знаком "новой строкой"
                //Исходя из структуры данных преобразуем string в элементы модели
                dataLines = data.Split(new char[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);

                foreach (var dataLine in dataLines)
                {
                    var model = dataLine.Split(',');
                    Guid.TryParse(model[0], out Guid id);
                    string name = model[1];
                    string surnamePerson = model[2];
                    var department = (Departments)Enum.Parse(typeof(Departments), model[3]);
                    var position = (Positions)Enum.Parse(typeof(Positions), model[4]);
                    decimal.TryParse(model[5], out decimal salary);

                    EmployeesBase employee = null;
                    switch (position)
                    {
                        case Positions.Developer:
                            employee = new StaffEmployee(id, name, surnamePerson, department, position, salary);                           
                            break;
                        case Positions.Director:
                            employee = new StaffEmployee(id, name, surnamePerson, department, position, salary);
                            break;
                        case Positions.Freelance:
                            employee = new StaffEmployee(id, name, surnamePerson, department, position, salary);
                            break;
                        default:
                            break;
                    }

                    if (employee != null)
                        dto.Add(employee);
                }               
            }
            //TODO Дописать обработчик исключений
            catch (Exception)
            {
                throw new Exception($"Ошибка блока FileCSVEmployeeRepository, метод GetEmployeesListAsync");
            }

            if (dto.Count == 0)
                return null;

            var result = dto;
            
            return result;

        }

        /// <summary>
        /// Удаление сотрудника из файла данных
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EmployeesBase> DeleteEmployeeAsync(Guid id)
        {
            //Получаем коллекцию всех сотрудников
            var employeesList = await GetEmployeesListAsync();
            //Находим удаляемого сотрудника по id  и проверяем, существует ли такой сотрудник
            var deleteEmployee = employeesList.FirstOrDefault(p => p.Id == id);
            if (deleteEmployee != null)
            {
                //Создаем результирующий список и удаляем из него сотрудника
                List<EmployeesBase> resultlist = employeesList.ToList();
                resultlist.Remove(deleteEmployee);

                //Удаляем файл с данными сотрудников
                try
                {
                    //Сохраним копию текущего файла с данными
                    //Получим имя сохраненного файла
                    string savefile = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).FullName,
                                                       $"{Path.GetFileNameWithoutExtension(FileName)}_save.csv");
                    //Копируем текущий файл
                    new FileInfo(FileName).CopyTo(savefile);
                    //И удаляем его
                    new FileInfo(FileName).Delete();

                    //Записываем результирующий список сотрудников в новый файл
                    foreach (var item in resultlist)
                    {
                        await InsertEmployeeAsync(item);
                    }

                    //Если ошибок не пришло удаляем временный файл
                    new FileInfo(savefile).Delete();
                    return deleteEmployee;

                }
                //TODO Дописать обработчик исключений
                catch (Exception)
                {
                    throw new Exception($"Ошибка блока FileCSVEmployeeRepository, метод DeleteEmployeeAsync");
                }



            }

            return null;
        }

        /// <summary>
        /// Получить сотрудника по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<EmployeesBase> GetEmployeeByNameAsync(string name)
        {
            var list = await GetEmployeesListAsync();
            if (list != null)
                return list.FirstOrDefault(p => p.NamePerson == name);
            else
                return null;
        }

        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<EmployeesBase> InsertEmployeeAsync(EmployeesBase employee)
        {
            //Проверяем входные данные на null
            if (employee != null)
            {
                var employeesList = await GetEmployeesListAsync();
                if (employeesList == null || employeesList.Contains(employee))
                    return null;

                try
                {
                    //Преобразуем сотрудника в строку используя модель
                    string line = employee.ToFile(',');

                    //Создаем экземпляр класса StreamWriter, 
                    //передаем в него полное имя файла с данными и разрешаем добавление
                    using StreamWriter sw = new StreamWriter(FileName, true);
                    //Записываем в файл строку
                    await sw.WriteLineAsync(line);

                    return employee;
                }
                catch (Exception)
                {
                    //TODO Дописать обработчик исключений
                    throw new Exception($"Ошибка блока FileCSVEmployeeRepository, метод InsertEmployeeAsync");
                }

            }
            else return null;


        }

        /// <summary>
        /// Получить сотрудника по id
        /// </summary>
        /// <returns></returns>
        public async Task<EmployeesBase> GetEmployeeByIdAsync(Guid id)
        {
            var list = await GetEmployeesListAsync();
            if (list != null)
                return list.FirstOrDefault(p => p.Id == id);
            else
                return null;
         }

        #endregion

    }
}
