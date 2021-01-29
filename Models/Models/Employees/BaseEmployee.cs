using System;
using System.Collections.Generic;

namespace Catdog50RUS.EmployeesAccountingSystem.Models.Employees
{
    /// <summary>
    /// Базовый класс модели сотрудника
    /// </summary>
    public abstract class BaseEmployee : SalaryCalculateSettings
    {
        #region Fields & Constructors

        public Guid Id { get; set; }
        public string NamePerson { get; set; }
        public string SurnamePerson { get; set; }
        public Departments Department { get; set; }
        public Positions Position { get; set; }
        public decimal BaseSalary { get; set; }

        public BaseEmployee(string name, string surname, Departments dep, decimal baseSalary)
        {
            Id = Guid.NewGuid();
            NamePerson = name;
            SurnamePerson = surname;
            Department = dep;
            BaseSalary = baseSalary;
        }
        public BaseEmployee(string name, string surname, Departments dep, Positions pos, decimal baseSalary) : this(name, surname, dep, baseSalary)
        {
            Position = pos;
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

        #endregion

        public override string ToString() => $"{NamePerson} {SurnamePerson}";
        public override bool Equals(object obj) => ToString().Equals(obj.ToString());
        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        /// Преобразование модели к записи в файл
        /// </summary>
        /// <param name="dateSeparator"></param>
        /// <returns></returns>
        public virtual string ToFile(char dateSeparator)
        {
            return $"{Id}{dateSeparator}" +
                   $"{NamePerson}{dateSeparator}" +
                   $"{SurnamePerson}{dateSeparator}" +
                   $"{Department}{dateSeparator}" +
                   $"{Position}{dateSeparator}" +
                   $"{BaseSalary}{dateSeparator}";
        }
        /// <summary>
        /// Рассчитать зарплату
        /// </summary>
        /// <param name="tasksLogList"></param>
        /// <returns></returns>
        public abstract decimal CalculateSamary(IEnumerable<CompletedTaskLog> tasksLogList);
    }
}
