using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI
{
    class ShowPersonsList
    {
        public static void ShowPersons(IEnumerable<Person> collection)
        {
            Console.Clear();
            Console.WriteLine("Список сотрудников: ");
            foreach (var item in collection)
            {
                ShowPerson(item);
            }
        }

        private static void ShowPerson(Person person)
        {
            Console.WriteLine(person.ToDisplay());
            Console.WriteLine();
        }
    }
}
