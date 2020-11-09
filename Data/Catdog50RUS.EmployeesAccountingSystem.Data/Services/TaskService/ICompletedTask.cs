using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Services
{
    public interface ICompletedTask
    {
        Task<bool> AddNewTask(CompletedTask task);
        Task<IEnumerable<CompletedTask>> GetPersonTask(Person person, DateTime firstDate, DateTime lastDate);

    }
}
