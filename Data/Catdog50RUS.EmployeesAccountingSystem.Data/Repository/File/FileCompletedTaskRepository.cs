using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File
{
    public class FileCompletedTaskRepository : FileBase, ICompletedTaskRepository
    {
        /// <summary>
        /// Хранилище данных о затраченном времени на выполнение задач
        /// </summary>
        private static readonly string fileName = "completedtasks.txt";

        private static readonly IPersonRepository personRepository = new FilePersonRepository();

        /// <summary>
        /// Конструктор используем конструктор базового класса
        /// </summary>
        public FileCompletedTaskRepository() : base(fileName) { }


        #region Interface

        public async Task<CompletedTask> AddCompletedTask(CompletedTask task)
        {
            if (task != null)
            {
                try
                {
                    using StreamWriter sw = new StreamWriter(path, true);
                    string line = task.ToFile();
                    await sw.WriteLineAsync(line);
                    return task;
                }
                catch (Exception)
                {
                    throw;
                }
                
            }
            else
                return null;
        }

        public async Task<IEnumerable<CompletedTask>> GetCompletedTasksList()
        {
            List<CompletedTask> result = new List<CompletedTask>();
            try
            {
                using StreamReader sr = new StreamReader(path, Encoding.Default);
                string line = null;
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    var model = line.Split(';');
                    var id = Guid.Parse(model[2]);
                    Person person = await personRepository.GetPersonByIdAsync(id);
                    CompletedTask task = new CompletedTask()
                    {
                        IdCounter = Guid.Parse(model[0]),
                        Date = DateTime.Parse(model[1]),
                        Person = person,
                        Time = double.Parse(model[3]),
                        TaskName = model[4],
                    };

                    if (task != null)
                        result.Add(task);
                    model = default;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public async Task<IEnumerable<CompletedTask>> GetPersonsTaskListAsync(Person person, DateTime beginDate, DateTime lastDate)
        {
            IEnumerable<CompletedTask> result = null; 
            var tasksList = await GetCompletedTasksList();
            var enablePerson = tasksList.FirstOrDefault(p => p.Person.IdPerson == person.IdPerson);
            if (enablePerson == null) return null;
            result = tasksList.Where(p => p.Person.IdPerson == person.IdPerson);
            var enablePeriod = result.FirstOrDefault(d => d.Date >= beginDate);
            if (enablePeriod == null) return null;
            return result.Where(d => d.Date >= beginDate && d.Date < lastDate);
        }

        #endregion

    }
}
