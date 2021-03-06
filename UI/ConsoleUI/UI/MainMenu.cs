﻿using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.Models;
using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Components;
using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Services;
using Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File.csv;
using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
using Catdog50RUS.EmployeesAccountingSystem.Data.Services.EmployeeService;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using Catdog50RUS.EmployeesAccountingSystem.Reports.Services.SalaryReportService;
using System;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    class MainMenu
    {
        #region Fields & Constructors

        /// <summary>
        /// Авторизация
        /// </summary>
        private readonly AutorizeToken _autorize;
        /// <summary>
        /// Внедрение сервиса работы с данными сотрудника
        /// </summary>
        private readonly IEmployeeService _employeeService;
        /// <summary>
        /// Внедрение сервиса работы с задачами
        /// </summary>
        private readonly ICompletedTaskLogsService _completedTasksService;
        /// <summary>
        /// Внедрение сервиса отчетов
        /// </summary>
        private readonly ISalaryReportService _salaryReportService;
        /// <summary>
        /// DTO
        /// </summary>
        private readonly Employee _employee;

        /// <summary>
        /// Конструктор
        /// Принимает сотрудника
        /// </summary>
        /// <param name="person"></param>
        public MainMenu((AutorizeToken, BaseEmployee) inputParameters)
        {
            _autorize = inputParameters.Item1;

            _completedTasksService = new CompletedTasksLogsService(new FileCSVCompletedTasksLogRepository(), _autorize);
            _employeeService = new EmployeeService(new FileCSVEmployeeRepository(), _autorize);

            _salaryReportService = new SalaryReportService(_completedTasksService, _employeeService);

            _employee = inputParameters.Item2.ToEmployeeModel();
        }

        #endregion

        /// <summary>
        /// Отображение главного меню
        /// </summary>
        /// <returns></returns>
        public async Task Intro()
        {
            //Флаг выхода из главного меню
            bool exit = default;

            //Проверяем не является ли сотрудник админом (учетная запись для первого запуска программы)
            if (_employee.NamePerson.Equals("Admin"))
            {
                //Вывод на консооль главного меню для первого запуска программы
                await ShowAdmin();
                return;
            }

            //Запускаем цикл ожидающий выбора элементов меню
            while (!exit)
            {
                //Отображение элементов меню
                ShowText(_autorize.UserRole);
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
                        //await SetSettings();
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

        /// <summary>
        /// Вывести главное меню для первого запуска программы
        /// </summary>
        /// <returns></returns>
        private async Task ShowAdmin()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Вы вошли под пользователем Admin!");
            Console.WriteLine("Для продолжения работы программы необходимо зарегистрировать в системе пользователя - Руководителя");
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
                        await _employeeService.DeleteEmployeeAsync(_employee.Id);
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
        private void ShowText(Role role)
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
            if (role.Equals(Role.Director))
            { 
                Console.WriteLine("4 - Вывести на экран отчет по всем сотрудникам за месяц");
                Console.WriteLine("5 - Вывести на экран отчет по работе отдела за месяц");
                Console.WriteLine(new string('-', 70));
                Console.WriteLine("6 - Вывести на экран список сотрудников");
                Console.WriteLine("7 - Добавить сотрудника");
                Console.WriteLine("8 - Удалить сотрудника");
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
            //Получаем id
            var id = _employee.Id;
            //Проверяем, если пользователь - Директор, то он может загрузить данные для любого сотрудника
            if (_autorize.UserRole.Equals(Role.Director))
            {
                ShowSelectUserMenu();
                var newperson = await SelectPerson();
                if (newperson != null)
                    id = newperson.Id;
                Console.Clear();
            }

            //Создаем новую задачу в отдельном компоненте UI, передаем авторизацию и id сотрудника
            var task = new CreateTaskLog(_autorize).CreatNewTask(id);
            if (task == null)
                ShowOnConsole.ShowMessage($"Ошибка создания задачи");

            //Получаем TaskLog из DTO и добавляем задачу в хранилище и проверяем результат
            var taskLog = task.ToCompletedTaskLog();
            var result = await _completedTasksService.AddNewTaskLog(taskLog);
            if (result)
                ShowOnConsole.ShowInsertNewTaskMessage(task);
            else
                ShowOnConsole.ShowMessage($"Ошибка добавления задачи");

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
            var employee = _employee.ToBaseEmployee();
            //Проверяем, если пользователь - Директор, то он может загрузить данные для любого сотрудника
            if (_autorize.UserRole.Equals(Role.Director))
            {
                ShowSelectUserMenu();
                var newperson = await SelectPerson();
                if (newperson != null)
                    employee = newperson;
                Console.Clear();
            }

            await new Reports(_salaryReportService).GetEmployeeReport(employee, period);
        }

        //3
        private async Task GetReportByPerson()
        {
            //Получаем период
            var period = InputParameters.GetMonth();
            var employee = _employee.ToBaseEmployee();
            //Проверяем, если пользователь - Директор, то он может загрузить данные для любого сотрудника
            if (_autorize.UserRole.Equals(Role.Director))
            {
                ShowSelectUserMenu();
                var newperson = await SelectPerson();
                if (newperson != null)
                    employee = newperson;
                Console.Clear();
            }

            await new Reports(_salaryReportService).GetEmployeeReport(employee, period);

        }

        //4
        private async Task GetReportByAllPersons()
        {
            //Получаем период
            var month = InputParameters.GetMonth();
            await new Reports(_salaryReportService).GetAllPersonsReport(month);

        }

        //5
        private async Task GetReportByDepatments()
        {
            //Получаем период
            var month = InputParameters.GetMonth();
            await new Reports(_salaryReportService).GetAllDepartmentsReport(month);

        }

        //6
        /// <summary>
        /// Получить список сотрудников
        /// </summary>
        /// <returns></returns>
        private async Task CreatePersonsList()
        {
            //Получаем список сотрудников и проверяем результат на Null
            var personsList = await _employeeService.GetAllEmployeeAsync();
            if (personsList != null)
                ShowOnConsole.ShowEmployeesList(personsList);
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
            var newPerson = CreateNewEmployee.CreateNewPerson();
            if (newPerson != null)
            {
                //Добавляем сотрудника в хранилище и проверяем результат операции
                var employee = newPerson.ToBaseEmployee();

                var result = await _employeeService.InsertEmployeeAsync(employee);
                if (result)
                {
                    ShowOnConsole.ShowInsertNewEmployeeMessage(newPerson);
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
            BaseEmployee employee = await _employeeService.GetEmployeeByNameAsync(name);
            if (employee != null)
            {
                var employeeModel = employee.ToEmployeeModel();
                //Удаляем сотрудника из хранилища и проверяем результат операции
                var result = await _employeeService.DeleteEmployeeByNameAsync(employeeModel.NamePerson);
                if (result)
                    ShowOnConsole.ShowDeleteEmployeeMessage(employeeModel);
                else
                    ShowOnConsole.ShowMessage($"Ошибка удаления сотрудника: {employeeModel}");
            }
            else
                ShowOnConsole.ShowMessage($"Сотрудник с именем {name} не найден!");
            ShowOnConsole.ShowContinue(); ;
        }

        //0
        /// <summary>
        /// Выходим из главного меню
        /// </summary>
        /// <returns></returns>
        private bool Exit()
        {
            Console.WriteLine($"{_employee} До свидания!");
            ShowOnConsole.ShowContinue();
            return true;
        }


        #endregion

        #region Select another employee

        /// <summary>
        /// Выбрать сотрудника для получения информации
        /// </summary>
        /// <returns></returns>
        private async Task<BaseEmployee> SelectPerson()
        {
            Console.WriteLine();
            BaseEmployee employee = default;
            var key = Console.ReadKey().KeyChar;
            switch (key)
            {
                case '1':
                    employee = await new Authorization().GetEmployee();
                    break;
                default:
                    break;
            }
            return employee;
        }
        /// <summary>
        /// Показать меню выбора интересующего сотрудника
        /// </summary>
        private void ShowSelectUserMenu()
        {
            Console.WriteLine("Выберете сотрудника для ввода выполненной задачи:");
            Console.WriteLine();
            Console.WriteLine("1 - Выбрать другого сотрудника");
            Console.WriteLine("Любая клавиши - продолжить");
        }

        #endregion

    }
}
