using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Components;
using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.UI.Services;
using System;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    internal class FirstMenu
    {
        
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
                        await AuthorizeUser();
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

        private async Task AuthorizeUser() 
        {
            //Создаем экземпляр класса авторизации и получаем авторизованного пользователя 
            var auth = new Authorization();
            var person = await auth.AutorezationUser();
            if(person != null)
            {
                //Переходим в главное меню и передаем в него сотрудника
                var mainmenu = new MainMenu(person);
                await mainmenu.Intro();
            } 
        }

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

        

        #endregion

    }
}
