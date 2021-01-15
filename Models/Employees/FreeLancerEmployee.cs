using System;
using System.Collections.Generic;

namespace Catdog50RUS.EmployeesAccountingSystem.Models.Employees
{
    public class FreeLancerEmployee : BaseEmployee
    {
        public FreeLancerEmployee(string name, string surname, Departments dep, Positions pos, decimal baseSalary) 
                           : base(name, surname, dep, pos, baseSalary)
        {
        }

        public FreeLancerEmployee(Guid id, string name, string surname, Departments dep, Positions pos, decimal baseSalary) 
                           : base(id, name, surname, dep, pos, baseSalary)
        {
        }

        public override decimal CalculateSamary(IEnumerable<CompletedTask> tasks)
        {
            throw new NotImplementedException();
        }
    }
}
