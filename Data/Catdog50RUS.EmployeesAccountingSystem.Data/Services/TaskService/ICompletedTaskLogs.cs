using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Services
{
    public interface ICompletedTaskLogs
    {
        CompletedTask CreateNewTask(DateTime date, BaseEmployee employee, string taskname, double time);
        Task<bool> AddNewTaskLog(CompletedTask task);
        Task<IEnumerable<CompletedTask>> GetEmployeeTaskLogs(Guid employeeID, DateTime startday, DateTime stopday);
        Task<IEnumerable<CompletedTask>> GetCompletedTaskLogs(DateTime startday, DateTime stopday);

    }
}
