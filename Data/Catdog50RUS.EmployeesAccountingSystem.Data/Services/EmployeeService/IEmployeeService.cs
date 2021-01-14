using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Services.EmployeeService
{
    public interface IEmployeeService
    {
        public bool IsFirstRun { get; }

        Task<bool> InsertEmployeeAsync(EmployeesBase employee);
        Task<IEnumerable<EmployeesBase>> GetAllPersonsAsync();
        Task<EmployeesBase> GetPersonByName(string name);
        Task<bool> DeletePersonAsync(Guid id);
    }
}
