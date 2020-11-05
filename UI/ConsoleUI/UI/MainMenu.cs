using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    class MainMenu
    {
        //Поля
        //TODO реализовать внедрение через интерфейс
        /// <summary>
        /// Внедрение сервиса работы с данными сотрудника
        /// </summary>
        private PersonsService PersonsService { get; }
        /// <summary>
        /// Внедрение сервиса работы с задачами
        /// </summary>
        private CompletedTasksService CompletedTasksService { get; }
        /// <summary>
        /// Внедрение сервиса отчетов
        /// </summary>
        private ReportService ReportService { get; set; }
        /// <summary>
        /// Поле сотрудник
        /// </summary>
        private Person Person { get; }

        /// <summary>
        /// Конструктор
        /// Принимает сотрудника
        /// </summary>
        /// <param name="person"></param>
        public MainMenu(Person person)
        {
            //Инициализация полей
            PersonsService = new PersonsService();
            CompletedTasksService = new CompletedTasksService();
            Person = person;
        }

        /// <summary>
        /// Отображение главного меню
        /// </summary>
        /// <returns></returns>
        public async Task Intro()
        {
            //Флаг выхода из главного меню
            bool exit = default;

            //Запускаем цикл ожидающий выбора элементов меню
            while (!exit)
            {
                //Отображение элементов меню
                ShowText(Person.Positions);
                //Получаем символ нажатой клавиши
                var key = Console.ReadKey().KeyChar;
                Console.Clear();

                //Проверяем какая клавиша нажата
                switch (key)
                { 
                    case '1':
                        //Добавляем задачу в хранилище
                        await AddNewTask();
                        break;
                    case '2':
                        //Получаем отчет по сотруднику
                        await CreatePersonReport();
                        break;
                    case '3':
                        //Получаем список сотрудников
                        await CreatePersonsList();
                        break;
                    case '8':
                        //Добавляем сотрудника
                        await InsertNewPerson();
                        break;
                    case '9':
                        //Удаляем сотрудника
                        await DeletePerson();
                        break;
                    case '0':
                        //Выход из профиля и возврат к начальному меню
                        exit = Exit();
                        break;
                    default:
                        break;
                };
            }

        }

        #region Реализация

        //Отображение элементов меню
        private static void ShowText(Positions positions)
        {
            //Вывод общих элементов меню
            Console.WriteLine();
            Console.WriteLine("Выберите дальнейшие действия:");
            Console.WriteLine("1 - Добавить выполненную задачу");
            Console.WriteLine("2 - Посмотреть список моих выполненных задач");
            //Вывод элементов меню доступных только руководителю
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
        /// <summary>
        /// Добавить новую задачу
        /// </summary>
        /// <returns></returns>
        private async Task AddNewTask()
        {
            //Создаем новую задачу в отдельном компоненте UI
            //И проверяем результат на null
            var task = CreateCompletedTask.CreatNewTask(Person);
            if(task != null)
            {
                //Добавляем задачу в хранилище и проверяем результат операции
                var result =  await CompletedTasksService.AddNewTask(task);
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
        /// <summary>
        /// Получаем отчет по сотруднику
        /// </summary>
        /// <returns></returns>
        private async Task CreatePersonReport()
        {
            //Получаем период
            var period = InputParameters.GetPeriod();
            //Передаем в конструктор сервиса Сотрудника и период
            ReportService = new ReportService(Person, period);
            //Получаем отчет и проверяем его на null и валидность числовых параметров
            //Выводим результат
            var personReport = await ReportService.GetPersonReport();
            if(personReport.Item3 != null)
            {
                if(personReport.Item1 >= 0 && personReport.Item2 >= 0)
                    ShowOnConsole.ShowPersonTasks(Person, period, personReport);
            }
            else
            {
                ShowOnConsole.ShowError("Ошибка получения отчета!");
            }
            ShowOnConsole.ShowContinue();
            
        }

        //3
        /// <summary>
        /// Получить список сотрудников
        /// </summary>
        /// <returns></returns>
        private async Task CreatePersonsList()
        {
            //Получаем список сотрудников и проверяем результат на Null
            var personsList = await PersonsService.GetAllPersonsAsync();
            if (personsList != null)
                ShowOnConsole.ShowPersons(personsList);
            else
                ShowOnConsole.ShowError("Не удалось сформировать список сотрудников!");
            ShowOnConsole.ShowContinue();
        }

        //8
        /// <summary>
        /// Добавление нового сотрудника
        /// </summary>
        /// <returns></returns>
        private async Task InsertNewPerson()
        {
            //Создаем нового сотрудника в отдельном компоненте UI
            //И проверяем результат на null
            var newPerson = CreatePerson.CreateNewPerson();
            if (newPerson != null)
            {
                //Добавляем сотрудника в хранилище и проверяем результат операции
                var result = await PersonsService.InsertPersonAsync(newPerson);
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
        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <returns></returns>
        private async Task DeletePerson()
        {
            Console.Clear();
            //Получаем имя удаляемого сотрудника
            var name = InputParameters.InputStringParameter("Введите имя удаляемого сотрудника");
            //Получаем сотрудника по имени
            //И проверяем результат
            Person person = await PersonsService.GetPersonByName(name);
            if(person != null)
            {
                //Удаляем сотрудника из хранилища и проверяем результат операции
                var result = await PersonsService.DeletePersonAsync(person.IdPerson);
                if (result)
                {
                    ShowOnConsole.ShowDeletePerson(person);
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
        /// <summary>
        /// Выходим из главного меню
        /// </summary>
        /// <returns></returns>
        private bool Exit()
        {
            Console.WriteLine($"{Person} До свидания!");
            ShowOnConsole.ShowContinue();
            return true;
        }

        #endregion

    }
}
