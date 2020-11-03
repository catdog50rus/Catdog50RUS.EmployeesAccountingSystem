using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    class CreatePerson
    { 
        public static Person CreateNewPerson()
        {
            Console.Clear();

            Console.WriteLine("Добавление нового пользователя");
            Console.WriteLine();
            string name = InputParameters.InputStringParameter("Введите имя сотрудника");
            string surname = InputParameters.InputStringParameter("Введите фамилию сотрудника");
            Departments dep = InputDepartment();
            Positions pos = InputPosition();
            decimal baseSalary = InputParameters.InputDecimlParameter("Введите базовую ставку сотрудника");

            return new Person(name, surname, dep, pos, baseSalary);

        }


        private static Departments InputDepartment()
        {
            Departments dep = default;
            Console.WriteLine();
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

        private static Positions InputPosition()
        {
            Positions pos = default;
            Console.WriteLine();
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
