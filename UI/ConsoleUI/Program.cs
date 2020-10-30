using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.Controllers;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Models;
using System;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    class Program
    {
        public static PersonsController PersonsController { get; set; }
        static void Main()
        {
            PersonsController = new PersonsController();
            MainAsync().GetAwaiter().GetResult();
        }
        private static async Task MainAsync()
        {
            Console.WriteLine("Добро пожаловать!");
            Console.WriteLine();
            await Intro();

        }

        private async static Task Intro()
        {
            bool exit = default;
            while (!exit)
            {
                
                Console.WriteLine("Выберите дальнейшие действия:");
                Console.WriteLine("1 - Вывести на экран список сотрудников");
                Console.WriteLine("2 - Добавить сотрудника");
                Console.WriteLine("0 - Выйти из программы");


                var key = Console.ReadKey().KeyChar;
                Console.Clear();
                switch (key)
                {
                    case '1':
                        await PersonsController.ShowPersonsListAsync();
                        break;
                    case '2':
                        var newPerson = CreateNewPerson();
                        await PersonsController.InsertPersonAsync(newPerson);
                        break;
                    case '0':
                        Console.WriteLine("Работа программы завершена");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Необходимо ввести цифры 1 или 0");
                        await Intro();
                        break;

                };
            }
            
        }

        private static Person CreateNewPerson()
        {
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

        private static string InputStringParametr()
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
