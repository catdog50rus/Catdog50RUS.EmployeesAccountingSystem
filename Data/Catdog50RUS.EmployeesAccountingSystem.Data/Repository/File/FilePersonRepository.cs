using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File
{
    /// <summary>
    /// Реализация доступа к данным сотрудников из файла
    /// </summary>
    public class FilePersonRepository : FileBase, IPersonRepository
    {
        /// <summary>
        /// Хранилище данных о сотрудниках
        /// </summary>
        private static readonly string filename = FileSettings.PERSONSFILENAME;
        /// <summary>
        /// Используем конструктор базового класса
        /// В конструктор базового класса передаем имя файла с данными
        /// </summary>
        public FilePersonRepository() : base(filename) { }

        #region Interface

        /// <summary>
        /// Получить асинхронно список всех сотрудников
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Person>> GetPersonsListAsync()
        {
            //Создаем новый список сотрудников
            List<Person> result = new List<Person>();

            //Процесс получения данных оборачиваем в блок try,
            //чтобы отловить исключения как по доступу к файлу, так и по качеству данных,
            //что позволит не использовать методы TryParse
            try
            {
                //Создаем экземпляр класса StreamReader, 
                //передаем в него полное имя файла с данными и кодировку
                using StreamReader sr = new StreamReader(FileName, Encoding.Default);
                string line = null;

                //Считываем данные построчно, до тех пор пока очередная строка не окажется пустой
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    //Объявляем строковый массив и передаем в него строку с данными
                    //Массив заполняется данными, каждый элемент массива разделяется знаком ";"
                    //Исходя из структуры данных преобразуем string в элементы модели
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
                    //Проверяем полученную модель на null и добавляем в результирующий список
                    if (person != null)
                        result.Add(person);
                    personModel = default;
                }
            }
            //TODO Дописать обработчик исключений
            catch (Exception)
            {

                throw;
            }

            return result;

        }
        /// <summary>
        /// Удаление сотрудника из файла данных
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Person> DeletePerson(Guid id)
        {
            //Получаем коллекцию всех сотрудников
            var personslist = await GetPersonsListAsync();
            //Находим удаляемого сотрудника по id  и проверяем, существует ли такой сотрудник
            var deletePerson = personslist.FirstOrDefault(p => p.IdPerson == id);
            if(deletePerson != null)
            {
                //Создаем результирующий список и удаляем из него сотрудника
                List<Person> resultlist = personslist.ToList();
                resultlist.Remove(deletePerson);

                //Удаляем файл с данными сотрудников
                try
                {
                    //Сохраним копию текущего файла с данными
                    //Получим имя сохраненного файла
                    string savefile = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).FullName, 
                                                       $"{Path.GetFileNameWithoutExtension(FileName)}_save.txt");
                    //Копируем текущий файл
                    new FileInfo(FileName).CopyTo(savefile);
                    //И удаляем его
                    new FileInfo(FileName).Delete();

                    //Записываем результирующий список сотрудников в новый файл
                    foreach (var item in resultlist)
                    {
                        await InsertPerson(item);
                    }

                    //Если ошибок не пришло удаляем временный файл
                    new FileInfo(savefile).Delete();
                    return deletePerson;

                }
                //TODO Дописать обработчик исключений
                catch (Exception)
                {

                    throw;
                }

                

            }

            return null;
        }
        /// <summary>
        /// Получить сотрудника по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Person> GetPersonByNameAsync(string name)
        {
            IEnumerable<Person> persons = await GetPersonsListAsync();
            return persons.FirstOrDefault(n => n.NamePerson == name);
        }
        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public async Task<Person> InsertPerson(Person person)
        {
            //Проверяем входные данные на null
            if (person != null)
            {
                try
                {
                    //Преобразуем сотрудника в строку используя модель
                    string line = person.ToFile();

                    //Создаем экземпляр класса StreamWriter, 
                    //передаем в него полное имя файла с данными и разрешаем добавление
                    using StreamWriter sw = new StreamWriter(FileName, true);
                    //Записываем в файл строку
                    await sw.WriteLineAsync(line);

                    return person;
                }
                catch (Exception)
                {
                    //TODO Дописать обработчик исключений
                    throw;
                }
                
            }
            else return null;


        }
        /// <summary>
        /// Получить сотрудника по id
        /// </summary>
        /// <returns></returns>
        public async Task<Person> GetPersonByIdAsync(Guid id)
        {
            var list = await GetPersonsListAsync();
            return list.FirstOrDefault(p => p.IdPerson == id);
        }

        #endregion

    }
}
