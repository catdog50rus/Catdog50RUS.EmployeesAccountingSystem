using System;
using System.Collections.Generic;

namespace Catdog50RUS.EmployeesAccountingSystem.Models.Employees
{
    public class DirectorEmployee : BaseEmployee
    {
        public DirectorEmployee(string name, string surname, Departments dep, decimal baseSalary) 
                         : base(name, surname, dep, baseSalary)
        {
            Positions = Positions.Director;
        }

        public DirectorEmployee(Guid id, string name, string surname, Departments dep, decimal baseSalary) 
                         : base(id, name, surname, dep, baseSalary)
        {
            Positions = Positions.Director;
        }

        public override decimal CalculateSamary(IEnumerable<CompletedTask> tasks)
        {
            throw new NotImplementedException();
        }
    }
}
