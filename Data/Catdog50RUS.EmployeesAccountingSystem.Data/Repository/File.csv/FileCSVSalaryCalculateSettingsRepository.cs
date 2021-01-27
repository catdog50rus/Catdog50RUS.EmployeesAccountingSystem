using Catdog50RUS.EmployeesAccountingSystem.Models;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File.csv
{
    public class FileCSVSalaryCalculateSettingsRepository : FileCSVBase, ISalaryCalculateSettingsRepository
    {
        /// <summary>
        /// Хранилище данных о сотрудниках
        /// </summary>
        private static readonly string _filename = FileCSVSettings.REPORTSETTINGS;

        public FileCSVSalaryCalculateSettingsRepository() : base(_filename) { }



        public async Task<SalaryCalculatingSettings> GetSettings()
        {
            //Считываем все строки из файла в текстовый массив
            string[] dataLines = await base.ReadAsync();
            var model = dataLines[0].Split(FileCSVSettings.DATA_SEPARATOR);

            double.TryParse(model[0], out double numberWorkingHoursPerMonth);
            double.TryParse(model[1], out double numberWorkingDaysPerMonth);
            double.TryParse(model[2], out double numberWorkingHoursPerDay);
            decimal.TryParse(model[3], out decimal bonusDirector);
            double.TryParse(model[4], out double bonusCoefficient);

            return new SalaryCalculatingSettings(numberWorkingHoursPerMonth, numberWorkingDaysPerMonth, numberWorkingHoursPerDay, bonusDirector, bonusCoefficient);
            
        }

        public async Task<bool> SaveSettings(SalaryCalculatingSettings settings)
        {
            if (settings == null)
                return false;
            
            //Преобразуем сотрудника в строку используя модель
            string line = settings.ToFile(DataSearator);

            //Записываем в файл, добавляем флаг перезаписи файла, получаем результат записи
            var writingResult = await base.WriteAsync(line, false);

            return writingResult;
        }
    }
}
