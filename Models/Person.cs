using Models;
using System;

namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    public class Person
    {
        public Guid IdPerson { get; set; }
        public string NamePerson { get; set; }
        public string SurnamePerson { get; set; }
        public Departments Department { get; set; }
        public Positions Positions { get; set; }
        public decimal BaseSalary { get; set; }

        public Person()
        {

        }

        public Person(string name, string surname, Departments dep, Positions pos, decimal baseSalary)
        {
            IdPerson = Guid.NewGuid();
            NamePerson = name;
            SurnamePerson = surname;
            Department = dep;
            Positions = pos;
            BaseSalary = baseSalary;
        }

        public override string ToString()
        {
            return $"{IdPerson},{NamePerson},{SurnamePerson},{Department},{Positions},{BaseSalary}";
        }
    }
}
