using Catdog50RUS.EmployeesAccountingSystem.Models;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File.csv
{
    public class FileCSVSalaryReportSettingsRepository : FileCSVBase, IReportSettingsRepository
    {
        /// <summary>
        /// Хранилище данных о сотрудниках
        /// </summary>
        private static readonly string _filename = FileCSVSettings.REPORTSETTINGS;

        public FileCSVSalaryReportSettingsRepository() : base(_filename)
        {
        }


        public async Task<ReportSettings> GetSettings()
        {
            //Считываем все строки из файла в текстовый массив
            string[] dataLines = await base.ReadAsync();
            var model = dataLines[0].Split(FileCSVSettings.DATA_SEPARATOR);

            int.TryParse(model[0], out int normTimeInMonth);
            decimal.TryParse(model[1], out decimal bonusDirector);
            decimal.TryParse(model[2], out decimal bonusCoefficient);

            return new ReportSettings(normTimeInMonth, bonusDirector, bonusCoefficient);
            
        }

        public async Task<bool> SaveSettings(ReportSettings settings)
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
