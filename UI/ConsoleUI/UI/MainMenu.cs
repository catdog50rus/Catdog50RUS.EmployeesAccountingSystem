using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.Controllers;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    class MainMenu
    {
        private readonly PersonsController _personsController;
        private readonly Person _person;

        public MainMenu(Person person)
        {
            _personsController = new PersonsController();
            _person = person;
        }

        public async Task Intro()
        {
            bool exit = default;
            while (!exit)
            {
                ShowText();
                var key = Console.ReadKey().KeyChar;
                Console.Clear();
                switch (key)
                {
                    case '1':
                        var personsList = await _personsController.GetAllPersonsAsync();
                        ShowPersonsList.ShowPersons(personsList);
                        break;
                    case '2':
                        break;
                    case '9':
                        await InsertNewPerson();
                        break;
                    case '0':
                        exit = Exit();
                        break;
                    default:
                        break;
                };
            }

        }

        private static void ShowText()
        {
            Console.WriteLine();
            Console.WriteLine("Выберите дальнейшие действия:");
            Console.WriteLine("1 - Вывести на экран список сотрудников");
            Console.WriteLine("2 - Добавить рабочее время");
            Console.WriteLine("9 - Добавить сотрудника");
            Console.WriteLine("0 - Выйти из профиля");
        }

        private bool Exit()
        {
            Console.WriteLine($"{_person.NamePerson} До свидания!");
            Console.WriteLine();
            Console.WriteLine("Для продолжения нажмите любую клавишу!");
            Console.ReadKey();
            Console.Clear();
            return true;
        }

        private async Task InsertNewPerson()
        {
            if (_person != null && _person.Positions == Positions.Director)
            {
                var newPerson = CreatePerson.CreateNewPerson();
                if (newPerson != null)
                {
                    var result = await _personsController.InsertPersonAsync(newPerson);
                    if (result)
                    {
                        Console.Clear();
                        Console.WriteLine($"Добавлен новый сотрудник: {newPerson}");
                        Console.WriteLine("Для продолжения нажмите любую клавишу");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Ошибка добавления сотрудника: {newPerson}");
                        Console.WriteLine("Для продолжения нажмите любую клавишу");
                        Console.ReadKey();
                    }
                }
                else Console.WriteLine("Ошибка добавления сотрудника");
            }
            else
            {
                Console.WriteLine("У вас нет прав для добавления пользователя!");
            }
        }


    }
}
