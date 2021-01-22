using System;
using System.Collections.Generic;

namespace Catdog50RUS.EmployeesAccountingSystem.Models.Employees
{
    public abstract class BaseEmployee
    {
        public Guid Id { get; set; }
        public string NamePerson { get; set; }
        public string SurnamePerson { get; set; }
        public Departments Department { get; set; }
        public Positions Positions { get; set; }
        public decimal BaseSalary { get; set; }

        public BaseEmployee(string name, string surname, Departments dep, decimal baseSalary)
        {
            Id = Guid.NewGuid();
            NamePerson = name;
            SurnamePerson = surname;
            Department = dep;
            BaseSalary = baseSalary;
        }

        public BaseEmployee(string name, string surname, Departments dep, Positions pos, decimal baseSalary)
        {
            Id = Guid.NewGuid();
            NamePerson = name;
            SurnamePerson = surname;
            Department = dep;
            Positions = pos;
            BaseSalary = baseSalary;
        }

        public BaseEmployee(Guid id, string name, string surname, Departments dep, decimal baseSalary) : 
                        this(name, surname, dep, baseSalary)
        {
            Id = id;
        }

        public BaseEmployee(Guid id, string name, string surname, Departments dep, Positions pos, decimal baseSalary) :
                        this(name, surname, dep, pos, baseSalary)
        {
            Id = id;
        }

        public override string ToString() => $"{NamePerson} {SurnamePerson}";

        public override bool Equals(object obj) => ToString().Equals(obj.ToString());

        public override int GetHashCode() => base.GetHashCode();

        public virtual string ToFile(char dateSeparator)
        {
            return $"{Id}{dateSeparator}" +
                   $"{NamePerson}{dateSeparator}" +
                   $"{SurnamePerson}{dateSeparator}" +
                   $"{Department}{dateSeparator}" +
                   $"{Positions}{dateSeparator}" +
                   $"{BaseSalary}{dateSeparator}";
        }

        public abstract decimal CalculateSamary(IEnumerable<CompletedTaskLog> tasksLogList);

    }
}
