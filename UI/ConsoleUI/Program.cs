using System;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    class Program
    {
        static void Main()
        {
            //Активируем асинхронный режим
            MainAsync().GetAwaiter().GetResult();
        }
        private static async Task MainAsync()
        {
            Console.WriteLine("Добро пожаловать!");

            await new FirstMenu().Intro();
        }  
    }
}
