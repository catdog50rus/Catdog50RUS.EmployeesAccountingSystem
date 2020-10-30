using Catdog50RUS.EmployeesAccountingSystem.Models;
using Models;
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
        private readonly string _path;

        public FilePersonRepository(string fileName) : base(fileName)
        {
            _path = path;

        }

        public async Task<IEnumerable<Person>> GetPersonsListAsync()
        {
            List<Person> result = new List<Person>();
            try
            {
                using (StreamReader sr = new StreamReader(_path, Encoding.Default))
                {
                    string line = null;
                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        var personModel = line.Split(',');
                        Person person = new Person()
                        {
                            IdPerson = Guid.Parse(personModel[0]),
                            NamePerson = personModel[1],
                            SurnamePerson = personModel[2],
                            Department = (Departments)Enum.Parse(typeof(Departments), personModel[3]),
                            Positions = (Positions)Enum.Parse(typeof(Positions), personModel[4]),
                            BaseSalary = decimal.Parse(personModel[5])
                        };
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
                using (StreamWriter sw = new StreamWriter(_path, true))
                {
                    string line = person.ToString();
                    await sw.WriteLineAsync(line);
                }
                return person;
            }
            else return null;

            
        }
    }
}
