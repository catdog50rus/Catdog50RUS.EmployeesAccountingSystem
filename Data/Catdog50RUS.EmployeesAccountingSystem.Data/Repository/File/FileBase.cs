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
        /// <summary>
        /// Конструктор базового класса
        /// </summary>
        /// <param name="fileName">Имя файла с данными</param>
        public FileBase(string fileName)
        {
            //Получаем директорию приложения
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory()).FullName;
            //Получаем полное имя файла с данными
            FileName = Path.Combine(directory, fileName);

        }
    }
}
