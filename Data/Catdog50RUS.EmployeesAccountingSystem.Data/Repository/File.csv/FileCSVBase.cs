using Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File.csv
{
    /// <summary>
    /// Реализация доступа к данным из файлов.
    /// Базовый класс
    /// </summary>
    public class FileCSVBase
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
        public FileCSVBase(string fileName)
        {
            //Получаем директорию приложения
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory()).FullName;
            //Получаем полное имя файла с данными
            var fn = Path.Combine(directory, fileName);
            //Проверяем, есть файл на диске
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
            if (file.Contains(FileCSVSettings.EMPLOYEES_LIST_FILENAME))
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


        public static async Task<string[]> ReadAsync(string filename)
        {
            try
            {
                //Создаем экземпляр класса StreamReader, 
                //передаем в него полное имя файла с данными
                using StreamReader sr = new StreamReader(filename);
                string[] dataLines = null;

                //Считываем данные из файла
                var data = await sr.ReadToEndAsync();

                //Объявляем строковый массив и передаем в него строку с данными
                //Массив заполняется данными, каждый элемент массива разделяется знаком "новой строкой"
                //Исходя из структуры данных преобразуем string в элементы модели
                return dataLines = data.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
