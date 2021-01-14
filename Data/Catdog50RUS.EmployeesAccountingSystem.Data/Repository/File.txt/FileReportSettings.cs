using Catdog50RUS.EmployeesAccountingSystem.Models;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File.txt
{
    public class FileReportSettings : FileBase, ISettingsRepository
    {
        /// <summary>
        /// Хранилище данных с настройками
        /// </summary>
        private static readonly string filename = FileSettings.REPORTSETTINGS;

        public FileReportSettings() : base(filename) { }

        public async Task<ReportSettings> GetSettings()
        {
            ReportSettings settings = null;

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
                    var model = line.Split(';');

                    //Заполняем модель
                    int.TryParse(model[0], out int normTimeInMonth);
                    decimal.TryParse(model[1], out decimal bonusDirector);
                    decimal.TryParse(model[2], out decimal bonusCoefficient);

                    settings = new ReportSettings(normTimeInMonth, bonusDirector, bonusCoefficient);

                    return settings;
                }
            }
            //TODO Дописать обработчик исключений
            catch (Exception)
            {
                throw;
            }

            return settings;
        }

        public async Task<bool> SaveSettings(ReportSettings settings)
        {
            //Проверяем входные данные на null
            if (settings != null)
            {
                try
                {
                    //Если настройки были удаляем их
                    var file = new FileInfo(FileName);
                    if(file.Exists)
                        file.Delete();
                    //Преобразуем задачу в строку используя модель
                    string line = settings.ToFile();
                    //Создаем экземпляр класса StreamWriter, 
                    //передаем в него полное имя файла с данными и разрешаем добавление
                    using StreamWriter sw = new StreamWriter(FileName, true);
                    //Записываем в файл строку
                    await sw.WriteLineAsync(line);
                    return true;
                }
                catch (Exception)
                {
                    //TODO Дописать обработчик исключений
                    throw;
                }

            }
            else
                return false;
        }
    }
}
