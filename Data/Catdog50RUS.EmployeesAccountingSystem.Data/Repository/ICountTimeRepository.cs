using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository
{
    public interface ICountTimeRepository
    {
        Task AddWorkingTime(Person person, DateTime date, double time, string taskName);
        Task<IEnumerable<CounterTimes>> GetCountTimesList(DateTime beginDate, DateTime lastDate);
    }
}
