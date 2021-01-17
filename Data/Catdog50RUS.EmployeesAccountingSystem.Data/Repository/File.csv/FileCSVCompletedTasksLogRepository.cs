using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File.csv
{
    public class FileCSVCompletedTasksLogRepository : FileCSVBase, ICompletedTasksLogRepository
    {
        private static readonly string _filename = FileCSVSettings.TASKSLOGS_FILENAME;
        /// <summary>
        /// Внедряем репозиторий с данными сотрудников через интерфейс
        /// </summary>
        private IEmployeeRepository _employeeRepository { get; } = new FileCSVEmployeeRepository();

        public FileCSVCompletedTasksLogRepository() : base(_filename) { }

        #region Interface

        /// <summary>
        /// Асинхронное добавление выполненной задачи
        /// </summary>
        /// <returns></returns>
        public async Task<CompletedTask> InsertCompletedTaskAsync(CompletedTask taskLog)
        {
            //Проверяем входные данные на null
            if (taskLog == null)
                return null;
            try
            {
                //Преобразуем задачу в строку используя модель
                string line = taskLog.ToFile();
                //Создаем экземпляр класса StreamWriter, 
                //передаем в него полное имя файла с данными и разрешаем добавление
                using StreamWriter sw = new StreamWriter(FileName, true);
                //Записываем в файл строку
                await sw.WriteLineAsync(line);
                return taskLog;
            }
            catch (Exception)
            {
                //TODO Дописать обработчик исключений
                throw;
            }
        }
        /// <summary>
        /// Получить список всех выполненных задач
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CompletedTask>> GetCompletedTasksListAsync()
        {
            var dataLines = await ReadAsync(FileName);

            //Создаем новый список выполненных задач
            List<CompletedTask> result = new List<CompletedTask>();

            foreach (var line in dataLines)
            {
                var model = line.Split(',');
                //Получаем id сотрудника
                var id = Guid.Parse(model[2]);
                //Получаем сотрудника по id
                BaseEmployee employee = await _employeeRepository.GetEmployeeByIdAsync(id);
                //Заполняем модель
                CompletedTask task = new CompletedTask()
                {
                    IdTask = Guid.Parse(model[0]),
                    Date = DateTime.Parse(model[1]),
                    Employee = employee,
                    Time = double.Parse(model[3]),
                    TaskName = model[4],
                };
                //Проверяем полученную модель на null и добавляем в результирующий список
                if (task != null)
                    result.Add(task);
                model = default;
            }
            return result.OrderBy(d => d.Date);
        }
        /// <summary>
        /// Получить список задач
        /// выполненных конкретным сотрудником
        /// за определенный период
        /// </summary>
        /// <param name="person"></param>
        /// <param name="beginDate"></param>
        /// <param name="lastDate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CompletedTask>> GetEmployeeTasksListAsync(Guid employeeID,
                                                                              DateTime beginDate,
                                                                              DateTime lastDate)
        {
            //Получаем список всех задач
            var tasksList = await GetCompletedTasksListInPeriodAsync(beginDate, lastDate);
            if (tasksList == null) return null;
            //Проверяем, есть ли в списке задачи, выполненные заданным сотрудником
            //Если задач нет выходим из метода, возвращаем null
            //Иначе передаем в результирующий список все задачи сотрудника
            if (tasksList.FirstOrDefault(p => p.Employee.Id == employeeID) != null)
                return tasksList.Where(p => p.Employee.Id == employeeID);
            else
                return null;
        }
        public async Task<IEnumerable<CompletedTask>> GetCompletedTasksListInPeriodAsync(DateTime beginDate,
                                                                              DateTime lastDate)
        {
            //Получаем список всех задач
            var tasksList = await GetCompletedTasksListAsync();

            //Проверяем, есть ли в списке задачи, выполненные в указанную дату или позднее
            //Если задач нет выходим из метода, возвращаем null
            //Иначе передаем в результирующий список задач выполненных в заданный период
            if (tasksList.FirstOrDefault(d => d.Date >= beginDate) != null)
            {
                return tasksList.Where(d => d.Date >= beginDate && d.Date < lastDate);
            }
            return null;


        }

        #endregion
    }
}
