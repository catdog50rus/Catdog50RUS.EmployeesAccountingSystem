using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Console.Clear();
            Console.WriteLine("Список сотрудников: ");
            var collection = await GetAllPersonsAsync();
            foreach (var item in collection)
            {
                ShowPerson(item);
            }
        }

        public async Task<Person> Authorization(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var person = await PersonRepository.GetPersonByNameAsync(name);
                if (person != null)
                    return person;
            }
            return null;
        }

        public async Task InsertPersonAsync(Person person)
        {
            Console.Clear();
            if (person != null)
            {
                var result = await PersonRepository.InsertPerson(person);
                if(result != null)
                {
                    Console.Clear();
                    Console.WriteLine($"Добавлен новый сотрудник: {result}");
                    Console.WriteLine("Для продолжения нажмите любую клавишу");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"Ошибка добавления сотрудника: {person}");
                    Console.WriteLine("Для продолжения нажмите любую клавишу");
                    Console.ReadLine();
                }
            }
            else 
                Console.WriteLine("Сотрудник не определен");
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
