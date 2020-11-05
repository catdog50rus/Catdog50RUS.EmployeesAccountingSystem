using Catdog50RUS.EmployeesAccountingSystem.Data.Services;
using System;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    internal class FirstMenu
    {
        //Поля
        //TODO реализовать внедрение через интерфейс
        /// <summary>
        /// Внедрение бизнес логики
        /// </summary>
        private PersonsService PersonsService { get; } = new PersonsService();
        
        /// <summary>
        /// Отображение начального меню
        /// </summary>
        /// <returns></returns>
        public async Task Intro()
        {
            //Флаг выхода из программы
            bool exit = default;
            //Запускаем цикл ожидающий выбора элементов меню
            while (!exit)
            {
                //Отображение элементов меню
                ShowText();
                //Получаем символ нажатой клавиши
                var key = Console.ReadKey().KeyChar;
                //Очищаем консоль
                Console.Clear();
                //Проверяем какая клавиша нажата
                switch (key)
                {
                    case '1':
                        //Выполняем авторизацию
                        await Autorezation();
                        break;
                    case '0':
                        //Выходим из приложения
                        Console.WriteLine("Работа программы завершена");
                        ShowOnConsole.ShowContinue();
                        exit = true;
                        break;
                    default: //Нажата любая другая клавиша
                        break;
                };
            }

        }

        #region Реализация

        /// <summary>
        /// Отображение элементов меню
        /// </summary>
        private static void ShowText()
        {
            Console.WriteLine();
            Console.WriteLine("Для начала работы необходимо авторизоваться!");
            Console.WriteLine();
            Console.WriteLine("Выберите дальнейшие действия:");
            Console.WriteLine("1 - Авторизоваться");
            Console.WriteLine("0 - Выйти из программы");
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <returns></returns>
        private async Task Autorezation()
        {
            Console.Clear();
            //Получаем имя сотрудника
            string name = InputParameters.InputStringParameter("Введите имя пользователя");
            //Получаем из хранилища сотрудника по имени и проверяем, если ли сотрудник с таким именем
            var person = await PersonsService.Authorization(name);
            if (person != null)
            {
                ShowOnConsole.ShowError($"Пользователь {person} успешно авторизован!");
                //Переходим в главное меню и передаем в него сотрудника
                var mainmenu = new MainMenu(person);
                await mainmenu.Intro();
            }
            else
            {
                ShowOnConsole.ShowError($"Пользователь с именем {name} не найден!");
                ShowOnConsole.ShowContinue();
            }
            
        }

        #endregion

    }
}
