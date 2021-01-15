using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Services.EmployeeService
{
    public interface IEmployeeService
    {
        public bool IsFirstRun { get; }

        Task<bool> InsertEmployeeAsync(BaseEmployee employee);
        Task<IEnumerable<BaseEmployee>> GetAllEmployeeAsync();
        Task<BaseEmployee> GetEmployeeByName(string name);
        Task<bool> DeleteEmployeeAsync(Guid id);
    }
}
