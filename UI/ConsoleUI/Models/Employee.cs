using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.Models
{
    /// <summary>
    /// DTO сотрудник
    /// </summary>
    public class Employee
    {
        public Guid Id { get; set; }
        public string NamePerson { get; set; }
        public string SurnamePerson { get; set; }
        public Departments Department { get; set; }
        public Positions Positions { get; set; }
        public decimal BaseSalary { get; set; }


        public override string ToString() => $"{NamePerson} {SurnamePerson}";

        public string ToInsert()
        {
            return AddCurrency($"Добавлен новый сотрудник: \n {SurnamePerson} {NamePerson} \n в отдел: {Department}, \n " +
                               $"на должность: {Positions}, \n с окладом {BaseSalary} рублей ");
        }
        public string ToDelete()
        {
            return $"Удален сотрудник: \n {SurnamePerson} {NamePerson} \n из отдела: {Department}, \n с должности: {Positions}";
        }
        public string ToDisplay()
        {
            return AddCurrency($"Сотрудник: {SurnamePerson} {NamePerson}, Отдел: {Department}, Должность: {Positions}, " +
                               $"Оклад: {BaseSalary} рублей ");
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
