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

        public Person() { }

        public Person(string name, string surname, Departments dep, Positions pos, decimal baseSalary)
        {
            IdPerson = Guid.NewGuid();
            NamePerson = name;
            SurnamePerson = surname;
            Department = dep;
            Positions = pos;
            BaseSalary = baseSalary;
        }

        public string ToDisplay()
        {
            return AddCurrency($"Сотрудник: {SurnamePerson} {NamePerson}, Отдел: {Department}, Должность: {Positions}, Оклад: {BaseSalary} рублей ");
        }

        public string ToFile()
        {
            return $"{IdPerson};{NamePerson};{SurnamePerson};{Department};{Positions};{BaseSalary}";
        }

        public string ToInsert()
        {
            return AddCurrency($"Добавлен новый сотрудник: \n {SurnamePerson} {NamePerson} \n в отдел: {Department}, \n на должность: {Positions}, \n с окладом {BaseSalary} рублей ");
        }

        public string ToDelete()
        {
            return $"Удален сотрудник: \n {SurnamePerson} {NamePerson} \n из отдела: {Department}, \n с должности: {Positions}";
        }

        public override string ToString()
        {
            return $"{NamePerson} {SurnamePerson}";
        }

        private string AddCurrency(string text)
        {
            if (Positions.Equals(Positions.Freelance))
                text += "в час";
            else
                text += "в месяц";
            return text;
        }
    }
}
