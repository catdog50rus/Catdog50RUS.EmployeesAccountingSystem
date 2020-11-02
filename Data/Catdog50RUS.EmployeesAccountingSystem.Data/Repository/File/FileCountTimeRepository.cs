using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File
{
    public class FileCountTimeRepository : FileBase, ICountTimeRepository
    {
        /// <summary>
        /// Хранилище данных о затраченном времени на выполнение задач
        /// </summary>
        private static readonly string fileName = "countertimes.txt";

        private static readonly IPersonRepository personRepository = new FilePersonRepository();

        /// <summary>
        /// Конструктор используем конструктор базового класса
        /// </summary>
        public FileCountTimeRepository() : base(fileName) { }


        #region Interface

        public async Task AddWorkingTime(CounterTimes counter)
        {
            if (counter != null)
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    string line = counter.ToFile();
                    await sw.WriteLineAsync(line);
                }
            }
            else
                return;
        }

        public async Task<IEnumerable<CounterTimes>> GetCountTimesList()
        {
            List<CounterTimes> result = new List<CounterTimes>();
            try
            {
                using (StreamReader sr = new StreamReader(path, Encoding.Default))
                {
                    string line = null;
                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        var model = line.Split(';');
                        var id = Guid.Parse(model[2]);
                        Person person = await personRepository.GetPersonByIdAsync(id);
                        CounterTimes task = new CounterTimes()
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
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public async Task<IEnumerable<CounterTimes>> GetPersonsTaskListAsync(Person person, DateTime beginDate, DateTime lastDate)
        {
            var tasksList = await GetCountTimesList();
            var result = tasksList.Where(p => p.Person.IdPerson == person.IdPerson);
            result = result.Where(d => d.Date >= beginDate && d.Date < lastDate);
            return result;
        }

        

        #endregion

    }
}
