using System;
using System.Collections.Generic;

namespace Catdog50RUS.EmployeesAccountingSystem.Models.Employees
{
    public class StaffEmployee : BaseEmployee
    {
        public StaffEmployee(string name, string surname, Departments department, decimal baseSalary)
                            : base(name, surname, department, baseSalary)
        {
            Positions = Positions.Developer;
        }

        public StaffEmployee(Guid id, string name, string surname, Departments department,  decimal baseSalary) 
                            : base(id, name, surname, department, baseSalary)
        {
            Positions = Positions.Developer;
        }

        public StaffEmployee(string name, string surname, Departments department, Positions pos, decimal baseSalary)
                            : base(name, surname, department, pos, baseSalary)
        {
            
        }

        public StaffEmployee(Guid id, string name, string surname, Departments department, Positions pos, decimal baseSalary)
                            : base(id, name, surname, department, pos, baseSalary)
        {
            
        }





        public override decimal CalculateSamary(IEnumerable<CompletedTask> tasks)
        {
            throw new NotImplementedException();
        }
    }
}
