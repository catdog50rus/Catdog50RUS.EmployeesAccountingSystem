using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI;
using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Components;
using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Services;
using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    class MainMenu
    {
        //Поля
        /// <summary>
        /// Внедрение сервиса работы с данными сотрудника
        /// </summary>
        private IPersons PersonsService { get; }
        /// <summary>
        /// Внедрение сервиса работы с задачами
        /// </summary>
        private ICompletedTaskLogs CompletedTasksService { get; } = new CompletedTasksService();

        private ISettingsRepository Settings { get; } = new ReportSettingsService();

        /// <summary>
        /// Внедрение сервиса отчетов
        /// </summary>
        //private SalaryReport Report { get; set; }
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
            //CompletedTasksService = new CompletedTasksService();
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

            if (Person.NamePerson.Equals("Admin"))
            {
                await ShowAdmin();
                return;
            }
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
                        //Получаем отчет по сотруднику за период
                        await GetPersonReport();
                        break; 
                    case '3':
                        //Получаем отчет по сотруднику за месяц
                        await GetReportByPerson();
                        break;
                    case '4':
                        //Получаем отчет по всем сотрудникам за месяц
                        await GetReportByAllPersons();
                        break;
                    case '5':
                        //Получаем отчет по отделам за месяц
                        await GetReportByDepatments();
                        break;
                    case '6':
                        //Получаем список сотрудников
                        await CreatePersonsList();
                        break;
                    case '7':
                        //Добавляем сотрудника
                        await InsertNewPerson();
                        break;
                    case '8':
                        //Удаляем сотрудника
                        await DeletePerson();
                        break;
                    case 's':
                        //Создать настройки
                        await SetSettings();
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

        private async Task ShowAdmin()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Вы вошли под пользователем Admin!");
            Console.WriteLine("Для продолжения работы программы необходимо зарегистрировать в системе пользоателя - Руководителя");
            Console.WriteLine();
            Console.WriteLine("Выберите дальнейшие действия:");
            Console.WriteLine("1 - Зарегистрировать Руководителя");
            Console.WriteLine("0 - Выйти из профиля");

            bool exit = default;
            while (!exit)
            {
                var key = Console.ReadKey().KeyChar;
                switch (key)
                {
                    case '1':
                        await InsertNewPerson();
                        await PersonsService.DeletePersonAsync(Person.IdPerson);
                        exit = true;
                        break;
                    case '0':
                        exit = true;
                        break;
                    default:
                        break;
                }
            }
        }

        //Отображение элементов меню
        private static void ShowText(Positions positions)
        {
            //Вывод общих элементов меню
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Выберите дальнейшие действия:");
            Console.WriteLine("1 - Добавить выполненную задачу");
            Console.WriteLine(new string('-', 70));
            Console.WriteLine("2 - Вывести на экран отчет по сотруднику за период");
            Console.WriteLine("3 - Вывести на экран отчет по сотруднику за месяц");
            //Вывод элементов меню доступных только руководителю
            if (positions.Equals(Positions.Director))
            { 
                Console.WriteLine("4 - Вывести на экран отчет по всем сотрудникам за месяц");
                Console.WriteLine("5 - Вывести на экран отчет по работе отдела за месяц");
                Console.WriteLine(new string('-', 70));
                Console.WriteLine("6 - Вывести на экран список сотрудников");
                Console.WriteLine("7 - Добавить сотрудника");
                Console.WriteLine("8 - Удалить сотрудника");
                Console.WriteLine(new string('-', 70));
                Console.WriteLine("s - Ввести данные для расчета");
            }
            Console.WriteLine(new string('-', 70));
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
            var task = await CreateTask.CreatNewTask(Person);
            if(task != null)
            {
                //Добавляем задачу в хранилище и проверяем результат операции
                var result =  await CompletedTasksService.AddNewTaskLog(task);
                if (result)
                {
                    ShowOnConsole.ShowNewTask(task);
                }
            }
            else
            {
                ShowOnConsole.ShowMessage($"Ошибка добавления задачи");
            }
            ShowOnConsole.ShowContinue();
        }

        //2
        /// <summary>
        /// Получаем отчет по сотруднику
        /// </summary>
        /// <returns></returns>
        private async Task GetPersonReport()
        {
            //Получаем период
            var period = InputParameters.GetPeriod();
            await GetReportOnPerson(period);
        }
       

        //3
        private async Task GetReportByPerson()
        {
            //Получаем период
            var period = InputParameters.GetMonth();
            await GetReportOnPerson(period);

        }



        //4
        private async Task GetReportByAllPersons()
        {
            //Получаем период
            var month = InputParameters.GetMonth();
            await Reports.GetAllPersonsReport(month);
            
        }

        //5
        private async Task GetReportByDepatments()
        {
            //Получаем период
            var month = InputParameters.GetMonth();
            await Reports.GetDepartmentsReport(month);

        }


        //6
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
                ShowOnConsole.ShowMessage("Не удалось сформировать список сотрудников!");
            ShowOnConsole.ShowContinue();
        }

        //7
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
                ShowOnConsole.ShowMessage("Ошибка добавления сотрудника");
            ShowOnConsole.ShowContinue();

        }

        //8
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
                    ShowOnConsole.ShowMessage($"Ошибка удаления сотрудника: {person}");
                }
            }
            else
                ShowOnConsole.ShowMessage($"Сотрудник с именем {name} не найден!");
            ShowOnConsole.ShowContinue(); ;
        }

        //s
        private async Task SetSettings()
        {
            var settings = SetNewSettings.CreateNewSettings();
            if(settings != null)
            {
                var res = await Settings.SaveSettings(settings);
                if (res) 
                    ShowOnConsole.ShowMessage("Настройки обновлены!");
                else
                    ShowOnConsole.ShowMessage("Ошибка обновления настроек!");
            }
            else
                ShowOnConsole.ShowMessage("Ошибка обновления настроек!");
            ShowOnConsole.ShowContinue();
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

        /// <summary>
        /// Поучение отчета по сотруднику с проверкой сотрудника
        /// </summary>
        /// <param name="period"></param>
        /// <returns></returns>
        private async Task GetReportOnPerson((DateTime, DateTime) period)
        {
            //Если текущий сотрудник директор, то получаем интересующего сотрудника
            if (Person.Positions.Equals(Positions.Director))
            {
                var person = await new Authorization().GetPerson();
                if (person != null)
                {
                    await Reports.GetPersonReport(person, period);
                }
            }
            else //иначе получаем отчет по текущему сотруднику
                await Reports.GetPersonReport(Person, period);
        }

        #endregion

    }
}
