using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.Controllers;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    internal class MainMenu
    {
        private readonly PersonsController _personsController;
        private Person _person;

        public MainMenu()
        {
            _personsController = new PersonsController();
            _person = default;
        }

        public async Task Intro()
        {
            bool exit = default;
            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("Выберите дальнейшие действия:");
                Console.WriteLine("1 - Вывести на экран список сотрудников");
                Console.WriteLine("2 - Авторизоваться");
                Console.WriteLine("3 - Добавить рабочее время");
                Console.WriteLine("9 - Добавить сотрудника");
                Console.WriteLine("0 - Выйти из программы");


                var key = Console.ReadKey().KeyChar;
                Console.Clear();
                switch (key)
                {
                    case '1':
                        await _personsController.ShowPersonsListAsync();
                        break;
                    case '2':
                        string name = GetNameParametr();
                        await Autorezation(name);
                        break;
                    case '3':

                        break;
                    case '9':
                        if (_person != null && _person.Positions == Positions.Director)
                        {
                            var newPerson = CreateNewPerson();
                            await _personsController.InsertPersonAsync(newPerson);
                        }
                        else
                        {
                            Console.WriteLine("У вас нет прав для добавления пользователя!");
                        }
                        break;
                    case '0':
                        Console.WriteLine("Работа программы завершена");
                        exit = true;
                        break;
                    default:
                        //await Intro();
                        break;

                };
            }

        }

        private async Task Autorezation(string name)
        {
            Console.Clear();
            var person = await _personsController.Authorization(name);
            if (person != null)
            {
                _person = person;
                Console.WriteLine($"Пользователь {_person.NamePerson} {_person.SurnamePerson} успешно авторизован!");
            }
            else
            {
                Console.WriteLine($"Пользователь с именем {name} не найден!");

            }
            Console.WriteLine();
        }

        private string GetNameParametr()
        {
            Console.Clear();
            string name = "";
            while (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Введите имя пользователя");
                name = Console.ReadLine();
            }
            return name;
        }

        private Person CreateNewPerson()
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
                name = InputStringParametr();
            }
            while (string.IsNullOrEmpty(surname))
            {
                Console.WriteLine("Введите фамилию");
                surname = InputStringParametr();
            }
            dep = InputDepartment();
            pos = InputPosition();
            while (baseSalary <= 0)
            {
                Console.WriteLine("Введите базовую ставку сотрудника");
                var salary = InputStringParametr();
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

        private string InputStringParametr()
        {
            var res = Console.ReadLine();
            if (string.IsNullOrEmpty(res) || string.IsNullOrWhiteSpace(res))
            {
                Console.WriteLine("Некорректный ввод!");
                Console.WriteLine("Попробуйте еще раз!");
                return "";
            }
            else return res;
        }

        private Enum InputDepartment()
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

        private Enum InputPosition()
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
