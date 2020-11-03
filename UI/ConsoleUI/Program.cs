using System;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    class Program
    {
        static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }
        private static async Task MainAsync()
        {
            Console.WriteLine("Добро пожаловать!");

            var enter = new FirstMenu();
            await enter.Intro();

        }

        
    }
}
