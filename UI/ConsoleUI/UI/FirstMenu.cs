using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.Controllers;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    internal class FirstMenu
    {
        private readonly PersonsController _personsController;
        private Person _person;

        public FirstMenu()
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
                Console.WriteLine("Для начала работы необходимо авторизоваться!");
                Console.WriteLine();
                Console.WriteLine("Выберите дальнейшие действия:");
                Console.WriteLine("1 - Авторизоваться");
                Console.WriteLine("0 - Выйти из программы");

                var key = Console.ReadKey().KeyChar;
                Console.Clear();
                switch (key)
                {
                    case '1':
                        string name = GetNameParametr();
                        await Autorezation(name);
                        if(_person != null)
                        {
                            var mainmenu = new MainMenu(_person);
                            await mainmenu.Intro();
                        }                    
                        break;
                    case '0':
                        Console.WriteLine("Работа программы завершена");
                        exit = true;
                        break;
                    default:
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
    }
}
