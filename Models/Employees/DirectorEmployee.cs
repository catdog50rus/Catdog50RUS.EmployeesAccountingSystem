using System;
using System.Collections.Generic;

namespace Catdog50RUS.EmployeesAccountingSystem.Models.Employees
{
    public class DirectorEmployee : BaseEmployee
    {
        public DirectorEmployee(string name, string surname, Departments dep, Positions pos, decimal baseSalary) 
                         : base(name, surname, dep, pos, baseSalary)
        {
        }

        public DirectorEmployee(Guid id, string name, string surname, Departments dep, Positions pos, decimal baseSalary) 
                         : base(id, name, surname, dep, pos, baseSalary)
        {
        }

        public override decimal CalculateSamary(IEnumerable<CompletedTask> tasks)
        {
            throw new NotImplementedException();
        }
    }
}
