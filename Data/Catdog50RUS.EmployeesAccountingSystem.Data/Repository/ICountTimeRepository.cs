using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository
{
    public interface ICountTimeRepository
    {
        Task AddWorkingTime(CounterTimes counter);
        Task<IEnumerable<CounterTimes>> GetCountTimesList();

        Task<IEnumerable<CounterTimes>> GetPersonsTaskListAsync(Person person, DateTime beginDate, DateTime lastDate);
    }
}
