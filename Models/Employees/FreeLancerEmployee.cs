using System;
using System.Collections.Generic;

namespace Catdog50RUS.EmployeesAccountingSystem.Models.Employees
{
    public class FreeLancerEmployee : BaseEmployee
    {
        public FreeLancerEmployee(string name, string surname, Departments dep, decimal baseSalary) 
                           : base(name, surname, dep, baseSalary)
        {
            Positions = Positions.Freelance;
        }

        public FreeLancerEmployee(Guid id, string name, string surname, Departments dep, decimal baseSalary) 
                           : base(id, name, surname, dep,  baseSalary)
        {
            Positions = Positions.Freelance;
        }

        public override decimal CalculateSamary(IEnumerable<CompletedTask> tasks)
        {
            throw new NotImplementedException();
        }
    }
}
