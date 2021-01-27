using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Services.ReportSettings
{
    public class SalaryCalculatingSettingsService : ISalaryCalculateSettingsService
    {
        private readonly ISalaryCalculateSettingsRepository _salaryCalculateSettingsRepository;

        public SalaryCalculatingSettingsService(ISalaryCalculateSettingsRepository salaryCalculateSettingsRepository)
        {
            _salaryCalculateSettingsRepository = salaryCalculateSettingsRepository;
        }

        #region Interface

        /// <summary>
        /// Получить настройки
        /// </summary>
        /// <returns></returns>
        public async Task<SalaryCalculatingSettings> GetSalaryCalculatingSettings()
        {
            //Получаем настройки
            var result = await _salaryCalculateSettingsRepository.GetSettings();
            return result;
        }

        /// <summary>
        /// Записать настройки
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public async Task<bool> SaveSalaryCalculatingSettings(SalaryCalculatingSettings settings)
        {
            //Проверяем входные параметры
            if (settings == null)
                return false;
            
            //Записываем настройки, получаем результат
            var result = await _salaryCalculateSettingsRepository.SaveSettings(settings);

            return result;

        }


        #endregion


    }
}
