using System;
using System.Collections.Generic;

namespace Catdog50RUS.EmployeesAccountingSystem.Models.Employees
{
    public class StaffEmployee : EmployeesBase
    {
        public StaffEmployee(string name, string surname, Departments department, Positions position, decimal baseSalary)
                            : base(name, surname, department, position, baseSalary)
        {

        }

        public StaffEmployee(Guid id, string name, string surname, Departments department, Positions position, decimal baseSalary) 
                            : base(id, name, surname, department, position, baseSalary)
        {

        }





        public override decimal CalculateSamary(IEnumerable<CompletedTask> tasks)
        {
            throw new NotImplementedException();
        }
    }
}
