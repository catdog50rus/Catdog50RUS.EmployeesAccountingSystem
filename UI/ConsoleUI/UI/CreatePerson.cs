using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    class CreatePerson
    { 
        public static Person CreateNewPerson()
        {
            Console.Clear();
            string name = "", surname = "";
            Enum dep, pos;
            decimal baseSalary = 0M;

            Console.WriteLine("Добавление нового пользователя");
            Console.WriteLine("");
            while (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Введите имя");
                name = InputParameters.InputStringParameter();
            }
            while (string.IsNullOrEmpty(surname))
            {
                Console.WriteLine("Введите фамилию");
                surname = InputParameters.InputStringParameter();
            }
            dep = InputDepartment();
            pos = InputPosition();
            while (baseSalary <= 0)
            {
                Console.WriteLine("Введите базовую ставку сотрудника");
                var salary = InputParameters.InputStringParameter();
                decimal.TryParse(salary, out baseSalary);

            }
            Person person = new Person()
            {
                IdPerson = Guid.NewGuid(),
                NamePerson = name,
                SurnamePerson = surname,
                Department = (Departments)dep,
                Positions = (Positions)pos,
                BaseSalary = baseSalary
            };
            return person;
        }


        private static Enum InputDepartment()
        {
            Departments dep = default;
            Console.WriteLine("Выберите отдел сотрудника:");
            Console.WriteLine("1 - Управление компанией");
            Console.WriteLine("2 - IT отдел");
            Console.WriteLine();
            var key = Console.ReadKey().KeyChar;
            switch (key)
            {
                case '1':
                    dep = Departments.Managment;
                    break;
                case '2':
                    dep = Departments.IT;
                    break;
                default:
                    InputDepartment();
                    break;
            }
            return dep;

        }

        private static Enum InputPosition()
        {
            Positions pos = default;
            Console.WriteLine("Выберите должность сотрудника:");
            Console.WriteLine("1 - Директор");
            Console.WriteLine("2 - Разработчик в штате");
            Console.WriteLine("3 - Разработчик вне штата");
            Console.WriteLine();
            var key = Console.ReadKey().KeyChar;
            switch (key)
            {
                case '1':
                    pos = Positions.Director;
                    break;
                case '2':
                    pos = Positions.Developer;
                    break;
                case '3':
                    pos = Positions.Freelance;
                    break;
                default:
                    InputPosition();
                    break;
            }
            return pos;

        }
    }
}
