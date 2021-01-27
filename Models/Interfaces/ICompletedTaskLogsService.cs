using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    public interface ICompletedTaskLogsService
    {
        CompletedTaskLog CreateNewTask(DateTime date, BaseEmployee employee, string taskname, double time);
        Task<bool> AddNewTaskLog(CompletedTaskLog task);
        Task<IEnumerable<CompletedTaskLog>> GetEmployeeTaskLogs(Guid employeeID, DateTime startday, DateTime stopday);
        Task<IEnumerable<CompletedTaskLog>> GetCompletedTaskLogs(DateTime startday, DateTime stopday);

    }
}
