using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File.txt
{
    /// <summary>
    /// Реализация доступа к данным выполненных задач из файла
    /// </summary>
    public class FileCompletedTaskRepository : FileBase//, ICompletedTasksLogRepository
    {
        /// <summary>
        /// Хранилище данных о затраченном времени на выполнение задач
        /// </summary>
        private static readonly string filename = FileSettings.TASKSFLIENAME;
        /// <summary>
        /// Внедряем репозиторий с данными сотрудников через интерфейс
        /// </summary>
        private static IPersonRepository PersonRepository { get; }// = new FilePersonRepository();

        /// <summary>
        /// Используем конструктор базового класса
        /// В конструктор базового класса передаем имя файла с данными
        /// </summary>
        public FileCompletedTaskRepository() : base(filename) { }


        #region Interface

        /// <summary>
        /// Асинхронное добавление выполненной задачи
        /// </summary>
        /// <returns></returns>
        public async Task<CompletedTaskLog> InsertCompletedTaskAsync(CompletedTaskLog task)
        {
            //Проверяем входные данные на null
            if (task != null)
            {
                try
                {
                    //Преобразуем задачу в строку используя модель
                    string line = task.ToFile();
                    //Создаем экземпляр класса StreamWriter, 
                    //передаем в него полное имя файла с данными и разрешаем добавление
                    using StreamWriter sw = new StreamWriter(FileName, true);
                    //Записываем в файл строку
                    await sw.WriteLineAsync(line);
                    return task;
                }
                catch (Exception)
                {
                    //TODO Дописать обработчик исключений
                    throw;
                }
                
            }
            else
                return null;
        }
        /// <summary>
        /// Получить список всех выполненных задач
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CompletedTaskLog>> GetCompletedTasksListAsync()
        {
            //Создаем новый список выполненных задач
            List<CompletedTaskLog> result = new List<CompletedTaskLog>();

            //Процесс получения данных оборачиваем в блок try,
            //чтобы отловить исключения как по доступу к файлу, так и по качеству данных,
            //что позволит не использовать методы TryParse
            try
            {
                //Создаем экземпляр класса StreamReader, 
                //передаем в него полное имя файла с данными и кодировку
                using StreamReader sr = new StreamReader(FileName, Encoding.Default);
                string line = null;

                //Считываем данные построчно, до тех пор пока очередная строка не окажется пустой
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    //Объявляем строковый массив и передаем в него строку с данными
                    //Массив заполняется данными, каждый элемент массива разделяется знаком ";"
                    //Исходя из структуры данных преобразуем string в элементы модели 
                    var model = line.Split(';');
                    //Получаем id сотрудника
                    var id = Guid.Parse(model[2]);
                    //Получаем сотрудника по id
                    Person person = await PersonRepository.GetPersonByIdAsync(id);
                    //Заполняем модель
                    //CompletedTask task = new CompletedTask();//(Guid.Parse(model[0]), person, DateTime.Parse(model[1]), double.Parse(model[3]), model[4]);

                    ////Проверяем полученную модель на null и добавляем в результирующий список
                    //if (task != null)
                    //    result.Add(task);
                    //model = default;
                }
            }
            //TODO Дописать обработчик исключений
            catch (Exception)
            {
                throw;
            }

            return result.OrderBy(d=>d.Date);
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
        public async Task<IEnumerable<CompletedTaskLog>> GetEmployeeTasksListAsync(Guid personID, 
                                                                              DateTime beginDate, 
                                                                              DateTime lastDate)
        {
            //Получаем список всех задач
            var tasksList = await GetCompletedTasksListInPeriodAsync(beginDate, lastDate);
            if (tasksList == null) return null;
            //Проверяем, есть ли в списке задачи, выполненные заданным сотрудником
            //Если задач нет выходим из метода, возвращаем null
            //Иначе передаем в результирующий список все задачи сотрудника
            ////if (tasksList.FirstOrDefault(p => p.Employee.IdPerson == personID) != null)
            //    return tasksList.Where(p => p.Employee.IdPerson == personID);
            else
                return null;
        }


        public async Task<IEnumerable<CompletedTaskLog>> GetCompletedTasksListInPeriodAsync(DateTime beginDate,
                                                                              DateTime lastDate)
        {
            //Получаем список всех задач
            var tasksList = await GetCompletedTasksListAsync();

            //Проверяем, есть ли в списке задачи, выполненные в указанную дату или позднее
            //Если задач нет выходим из метода, возвращаем null
            //Иначе передаем в результирующий список задач выполненных в заданный период
            if(tasksList.FirstOrDefault(d => d.Date >= beginDate) != null)
            {
                return tasksList.Where(d => d.Date >= beginDate && d.Date < lastDate);
            }
            return null;

            
        }

        #endregion

    }
}
