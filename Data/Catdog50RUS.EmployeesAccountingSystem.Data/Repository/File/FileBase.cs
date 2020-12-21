using Catdog50RUS.EmployeesAccountingSystem.Models;
using System.IO;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File
{
    /// <summary>
    /// Реализация доступа к данным из файлов.
    /// Базовый класс
    /// </summary>
    public class FileBase
    {
        /// <summary>
        /// Путь к файлу с данными
        /// </summary>
        protected string FileName { get; } = "";

        public bool IsFirstRun { get; }

        /// <summary>
        /// Конструктор базового класса
        /// </summary>
        /// <param name="fileName">Имя файла с данными</param>
        public FileBase(string fileName)
        {
            //Получаем директорию приложения
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory()).FullName;
            //Получаем полное имя файла с данными
            var fn = Path.Combine(directory, fileName);
            
            if(new FileInfo(fn).Exists)
            {
                FileName = fn;
                IsFirstRun = false;
            }
            else
            {
                //throw new FileNotFoundException($"Файл {fileName} не найден!");
                FileNotFound(fn);
                FileName = fn;
                IsFirstRun = true;
            }

        }

        private void FileNotFound(string file)
        {
            new FileInfo(file).Create().Close();
            if (file.Contains(FileSettings.PERSONSFILENAME))
            {
                var admin = new Person()
                {
                    NamePerson = "Admin",
                    Department = Departments.Managment,
                    Positions = Positions.Director
                };
                //Преобразуем сотрудника в строку используя модель
                string line = admin.ToFile();

                //Создаем экземпляр класса StreamWriter, 
                //передаем в него полное имя файла с данными и разрешаем добавление
                using StreamWriter sw = new StreamWriter(file, true);
                sw.WriteLine(line);
            }

        }
    }
}
