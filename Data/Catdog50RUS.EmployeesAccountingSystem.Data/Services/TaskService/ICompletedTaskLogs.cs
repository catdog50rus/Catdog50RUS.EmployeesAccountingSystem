using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Services
{
    public interface ICompletedTaskLogs
    {
        Task<bool> AddNewTaskLog(CompletedTask task);
        Task<IEnumerable<CompletedTask>> GetEmployeeTaskLogs(Guid employeeID, DateTime startday, DateTime stopday);
        Task<IEnumerable<CompletedTask>> GetCompletedTaskLogs(DateTime startday, DateTime stopday);

    }
}
