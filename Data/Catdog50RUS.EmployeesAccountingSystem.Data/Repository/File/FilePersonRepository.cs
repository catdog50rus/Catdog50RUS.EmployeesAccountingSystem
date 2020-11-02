using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File
{
    public class FilePersonRepository : FileBase, IPersonRepository
    {
        /// <summary>
        /// Хранилище данных о сотрудниках
        /// </summary>
        private static readonly string fileName = "persons.txt";
        /// <summary>
        /// Конструктор используем конструктор базового класса
        /// </summary>
        public FilePersonRepository() : base(fileName) { }

        #region Interface

        public async Task<IEnumerable<Person>> GetPersonsListAsync()
        {
            List<Person> result = new List<Person>();
            try
            {
                using (StreamReader sr = new StreamReader(path, Encoding.Default))
                {
                    string line = null;
                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        var personModel = line.Split(';');
                        Person person = new Person()
                        {
                            IdPerson = Guid.Parse(personModel[0]),
                            NamePerson = personModel[1],
                            SurnamePerson = personModel[2],
                            Department = (Departments)Enum.Parse(typeof(Departments), personModel[3]),
                            Positions = (Positions)Enum.Parse(typeof(Positions), personModel[4]),
                            BaseSalary = decimal.Parse(personModel[5])
                        };
                        if (person != null)
                            result.Add(person);
                        personModel = default;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;

        }

        //Не реализован
        public Person DeletePerson(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Person> GetPersonByNameAsync(string name)
        {
            IEnumerable<Person> persons = await GetPersonsListAsync();
            return persons.FirstOrDefault(n => n.NamePerson == name);
        }

        public async Task<Person> InsertPerson(Person person)
        {
            if (person != null)
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    string line = person.ToFile();
                    await sw.WriteLineAsync(line);
                }
                return person;
            }
            else return null;


        }

        public async Task<Person> GetPersonByIdAsync(Guid id)
        {
            var list = await GetPersonsListAsync();
            Person person = list.FirstOrDefault(p => p.IdPerson == id);
            if(person != null)
                return person;
            else
                return null;
        }

        #endregion

    }
}
