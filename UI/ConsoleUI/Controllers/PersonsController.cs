using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.ConsoleUI.Controllers
{
    public class PersonsController
    {
        private readonly string fileName = "persons.txt";

        public IEnumerable<Person> Persons { get; set; }
        public Person Person { get; set; }
        public IPersonRepository PersonRepository { get; set; }


        public PersonsController()
        {
            PersonRepository = new FilePersonRepository(fileName);
            
        }


        public async Task ShowPersonsListAsync()
        {
            Console.WriteLine("Список сотрудников");
            var collection = await GetAllPersonsAsync();
            foreach (var item in collection)
            {
                ShowPerson(item);
            }
        }

        

        public async Task InsertPersonAsync(Person person)
        {
            if (person != null)
            {
                var result = await PersonRepository.InsertPerson(person);
                if(result != null)
                {
                    Console.WriteLine("Добавлен новый сотрудник:");
                    ShowPerson(result);
                }
                Console.WriteLine("Ошибка добавления сотрудника: ");
                ShowPerson(person);
                
            }
            else Console.WriteLine("Сотрудник не определен");
        }

        private async Task<List<Person>> GetAllPersonsAsync()
        {
            var persons = await PersonRepository.GetPersonsListAsync();
            return persons.ToList();
        }
        
        private void ShowPerson(Person person)
        {
            Console.WriteLine(person.ToString());
            Console.WriteLine();
        }

    }
}
