﻿using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI;
using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Components;
using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Services;
using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Reports.SalaryReports;
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
        private ICompletedTask CompletedTasksService { get; } = new CompletedTasksService();

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
                    case '4':
                        
                        await GetReportByPerson();
                        break;
                    case '5':

                        await GetReportByAllPersons();
                        break;
                    case '8':
                        //Добавляем сотрудника
                        await InsertNewPerson();
                        break;
                    case '9':
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
                Console.WriteLine("4 - Вывести на экран отчет по сотруднику за месяц");
                Console.WriteLine("5 - Вывести на экран отчет по всем сотрудникам за месяц");
                Console.WriteLine("6 - Вывести на экран отчет по работе отдела за месяц (не реализовано)");
                Console.WriteLine("8 - Добавить сотрудника");
                Console.WriteLine("9 - Удалить сотрудника");
                Console.WriteLine("s - Ввести данные для расчета");
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
            var task = CreateTask.CreatNewTask(Person);
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
                ShowOnConsole.ShowMessage($"Ошибка добавления задачи");
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
            await Reports.GetPersonReport(Person, period);            
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
                ShowOnConsole.ShowMessage("Не удалось сформировать список сотрудников!");
            ShowOnConsole.ShowContinue();
        }

        //4
        private async Task GetReportByPerson()
        {
            //Получаем период
            var month = InputParameters.GetMonth();
            //Получаем сотрудника
            var person = await new Authorization().GetPerson();
            if(person != null)
            {
                await Reports.GetPersonReport(person, month);
            }
        }

        //5
        private async Task GetReportByAllPersons()
        {
            //Получаем период
            var month = InputParameters.GetMonth();
            await Reports.GetAllPersonsReport(month);
            
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
                ShowOnConsole.ShowMessage("Ошибка добавления сотрудника");
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

        #endregion

    }
}
