using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    class MainMenu
    {
        private readonly PersonsService _personsService;
        private readonly CompletedTasksService _completedTasksService;
        private readonly Person _person;

        public MainMenu(Person person)
        {
            _personsService = new PersonsService();
            _completedTasksService = new CompletedTasksService();
            _person = person;
        }

        public async Task Intro()
        {
            bool exit = default;
            while (!exit)
            {
                ShowText(_person.Positions);
                var key = Console.ReadKey().KeyChar;
                Console.Clear();
                switch (key)
                { 
                    case '1':
                        await AddNewTask();
                        break;
                    case '2':
                        await CreatePersonReport();
                        break;
                    case '3':
                        await CreatePersonsList();
                        break;
                    case '8':
                        await InsertNewPerson();
                        break;
                    case '9':
                        await DeletePerson();
                        break;
                    case '0':
                        exit = Exit();
                        break;
                    default:
                        break;
                };
            }

        }

        

        private static void ShowText(Positions positions)
        {
            Console.WriteLine();
            Console.WriteLine("Выберите дальнейшие действия:");
            Console.WriteLine("1 - Добавить выполненную задачу");
            Console.WriteLine("2 - Посмотреть список моих выполненных задач");
            if (positions.Equals(Positions.Director))
            {
                Console.WriteLine("3 - Вывести на экран список сотрудников");
                Console.WriteLine("4 - Вывести на экран отчет по сотруднику за месяц (не реализовано)");
                Console.WriteLine("5 - Вывести на экран отчет по всем сотрудникам за месяц (не реализовано)");
                Console.WriteLine("6 - Вывести на экран отчет по работе отдела за месяц (не реализовано)");
                Console.WriteLine("8 - Добавить сотрудника");
                Console.WriteLine("9 - Удалить сотрудника");
            }
            Console.WriteLine("0 - Выйти из профиля");
        }

        //1
        private async Task AddNewTask()
        {
            var task = CreateCompletedTask.CreatNewTask(_person);
            if(task != null)
            {
                var result =  await _completedTasksService.AddNewTask(task);
                if (result)
                {
                    ShowOnConsole.ShowNewTask(task);
                }
            }
            else
            {
                ShowOnConsole.ShowError($"Ошибка добавления задачи");
            }
            ShowOnConsole.ShowContinue();
        }

        //2
        private async Task CreatePersonReport()
        {
            var period = InputParameters.GetPeriod();
            var tasksList = await _completedTasksService.GetPersonTask(_person, period.Item1, period.Item2);
            if(tasksList != null)
            {
                var personReport = new ReportService(_person, tasksList).GetPersonReport();
                if(personReport.Item1 >= 0 && personReport.Item2 >= 0)
                    ShowOnConsole.ShowPersonTasks(_person, tasksList, period, personReport);
            }
            else
            {
                ShowOnConsole.ShowError("Ошибка получения отчета!");
            }
            ShowOnConsole.ShowContinue();
            
        }

        //3
        private async Task CreatePersonsList()
        {
            var personsList = await _personsService.GetAllPersonsAsync();
            if (personsList != null)
                ShowOnConsole.ShowPersons(personsList);
            else
                ShowOnConsole.ShowError("Не удалось сформировать список сотрудников!");
            ShowOnConsole.ShowContinue();
        }

        //8
        private async Task InsertNewPerson()
        {
            var newPerson = CreatePerson.CreateNewPerson();
            if (newPerson != null)
            {
                var result = await _personsService.InsertPersonAsync(newPerson);
                if (result)
                {
                    ShowOnConsole.ShowNewPerson(newPerson);
                }
            }
            else
                ShowOnConsole.ShowError("Ошибка добавления сотрудника");
            ShowOnConsole.ShowContinue();

        }
        //9
        private async Task DeletePerson()
        {
            Console.Clear();
            var name = InputParameters.InputStringParameter("Введите имя удаляемого сотрудника");
            Person person = await _personsService.GetPersonByName(name);
            if(person != null)
            {
                var result = await _personsService.DeletePersonAsync(person.IdPerson);
                if (result)
                {
                    ShowOnConsole.ShowNewPerson(person);
                }
                else
                {
                    ShowOnConsole.ShowError($"Ошибка удаления сотрудника: {person}");
                }
            }
            else
                ShowOnConsole.ShowError($"Сотрудник с именем {name} не найден!");
            ShowOnConsole.ShowContinue(); ;
        }
        //0
        private bool Exit()
        {
            Console.WriteLine($"{_person} До свидания!");
            ShowOnConsole.ShowContinue();
            return true;
        }

    }
}
