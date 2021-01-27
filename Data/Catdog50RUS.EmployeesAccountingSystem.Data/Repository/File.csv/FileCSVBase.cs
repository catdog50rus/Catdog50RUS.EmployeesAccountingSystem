using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
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
        protected static char DataSearator { get; } = FileCSVSettings.DATA_SEPARATOR;
        protected static char StringSearator { get; } = FileCSVSettings.STRING_SEPARATOR;

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
            var directory = Directory.GetCurrentDirectory();
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
                var admin = new DirectorEmployee(Guid.Empty, "Admin", null, Departments.Managment, 0);

                //Преобразуем сотрудника в строку используя модель
                string line = admin.ToFile(DataSearator);
                _ = WriteAsync(line).Result;
            }

        }


        public async Task<string[]> ReadAsync()
        {
            try
            {
                //Создаем экземпляр класса StreamReader, 
                //передаем в него полное имя файла с данными
                using StreamReader sr = new StreamReader(FileName);
                string[] dataLines = null;

                //Считываем данные из файла
                var data = await sr.ReadToEndAsync();

                //Объявляем строковый массив и передаем в него строку с данными
                //Массив заполняется данными, каждый элемент массива разделяется знаком "новой строкой"
                //Исходя из структуры данных преобразуем string в элементы модели
                return dataLines = data.Split(new char[] { StringSearator }, StringSplitOptions.RemoveEmptyEntries);
            }
            catch (Exception)
            {
                return null;
                throw new Exception($"Ошибка блока FileCSVRepository, метод Чтения из файла");
            }

        }

        public async Task<bool> WriteAsync(string data, bool isAppending = true)
        {
            try
            {
                //Создаем экземпляр класса StreamWriter, 
                //передаем в него полное имя файла с данными и разрешаем добавление
                using StreamWriter sw = new StreamWriter(FileName, isAppending);
                //Записываем в файл строку
                await sw.WriteLineAsync(data);
                
            }
            catch (Exception)
            {
                return false;
                throw new Exception($"Ошибка блока FileCSVRepository, метод Записи в файл");
            }

            return true;
        }
    }
}
