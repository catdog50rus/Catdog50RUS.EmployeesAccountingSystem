using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    internal class FirstMenu
    {
        private readonly PersonsService _personsService;
        private Person _person;

        public FirstMenu()
        {
            _personsService = new PersonsService();
            _person = default;
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
                        await Autorezation();
                        if (_person != null)
                        {
                            var mainmenu = new MainMenu(_person);
                            await mainmenu.Intro();
                        }
                        break;
                    case '0':
                        Console.WriteLine("Работа программы завершена");
                        ShowOnConsole.ShowContinue();
                        exit = true;
                        break;
                    default:
                        break;
                };
            }

        }

        private static void ShowText()
        {
            Console.WriteLine();
            Console.WriteLine("Для начала работы необходимо авторизоваться!");
            Console.WriteLine();
            Console.WriteLine("Выберите дальнейшие действия:");
            Console.WriteLine("1 - Авторизоваться");
            Console.WriteLine("0 - Выйти из программы");
        }

        private async Task Autorezation()
        {
            Console.Clear();
            string name = InputParameters.InputStringParameter("Введите имя пользователя");
            var person = await _personsService.Authorization(name);
            if (person != null)
            {
                _person = person;
                ShowOnConsole.ShowError($"Пользователь {_person} успешно авторизован!");  
            }
            else
            {
                ShowOnConsole.ShowError($"Пользователь с именем {name} не найден!");
            }
            ShowOnConsole.ShowContinue();
        }

    }
}
