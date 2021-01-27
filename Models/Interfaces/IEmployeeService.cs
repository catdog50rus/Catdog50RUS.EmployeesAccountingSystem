using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Models 
{ 
    public interface IEmployeeService
    {
        public bool IsFirstRun { get; }

        Task<bool> InsertEmployeeAsync(BaseEmployee employee);
        Task<IEnumerable<BaseEmployee>> GetAllEmployeeAsync();
        
        Task<BaseEmployee> GetEmployeeByNameAsync(string name);
        Task<BaseEmployee> GetEmployeeByIdAsync(Guid id);
        
        Task<bool> DeleteEmployeeAsync(Guid id);
        Task<bool> DeleteEmployeeByNameAsync(string name);
    }
}
