using Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.Controllers;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Models;
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

            var menu = new MainMenu();
            await menu.Intro();

        }

        
    }
}
