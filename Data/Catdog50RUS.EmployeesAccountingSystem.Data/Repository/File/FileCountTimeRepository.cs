using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File
{
    public class FileCountTimeRepository : FileBase, ICountTimeRepository
    {
        /// <summary>
        /// Хранилище данных о затраченном времени на выполнение задач
        /// </summary>
        private static readonly string fileName = "countertimes.txt";
        /// <summary>
        /// Конструктор используем конструктор базового класса
        /// </summary>
        public FileCountTimeRepository() : base(fileName) { }


        #region Interface

        public async Task AddWorkingTime(Person person, DateTime date, double time, string taskName)
        {
            var counter = new CounterTimes(person, date, time, taskName);
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

        public async Task<IEnumerable<CounterTimes>> GetCountTimesList(DateTime beginDate, DateTime lastDate)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
