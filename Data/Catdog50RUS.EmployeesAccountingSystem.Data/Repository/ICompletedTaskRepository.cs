using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository
{
    public interface ICompletedTaskRepository
    {
        Task<CompletedTask> AddCompletedTask(CompletedTask counter);
        Task<IEnumerable<CompletedTask>> GetCompletedTasksList();

        Task<IEnumerable<CompletedTask>> GetPersonsTaskListAsync(Person person, DateTime beginDate, DateTime lastDate);
    }
}
