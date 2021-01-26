using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File.csv;
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

            var enter = new FirstMenu();
            await enter.Intro();

        }

        
    }
}
