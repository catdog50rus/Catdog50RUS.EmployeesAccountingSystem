using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.Controllers;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    class MainMenu
    {
        private readonly PersonsController _personsController;
        private readonly CounterTimesController _counterTimesController;
        private readonly Person _person;

        public MainMenu(Person person)
        {
            _personsController = new PersonsController();
            _counterTimesController = new CounterTimesController();
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
                        ShowOnConsole.ShowPersons(personsList);
                        break;
                    case '2':
                        var counterTimes = CreateCounterTimes.CreatNewCounter(_person);
                        await _counterTimesController.AddNewTask(counterTimes);
                        break;
                    case '3':
                        var period = InputParameters.GetPeriod();
                        var tasksList = await _counterTimesController.GetPersonTask(_person, period.Item1, period.Item2);
                        ShowOnConsole.ShowPersonTasks(tasksList, period);
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
            Console.WriteLine("2 - Добавить выполненную задачу");
            Console.WriteLine("3 - Посмотреть список моих выполненных задач");
            Console.WriteLine("9 - Добавить сотрудника");
            Console.WriteLine("0 - Выйти из профиля");
        }

        private bool Exit()
        {
            Console.WriteLine($"{_person} До свидания!");
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
