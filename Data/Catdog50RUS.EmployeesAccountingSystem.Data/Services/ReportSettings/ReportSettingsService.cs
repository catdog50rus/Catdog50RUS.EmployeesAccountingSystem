using Catdog50RUS.EmployeesAccountingSystem.Data.Repository;
using Catdog50RUS.EmployeesAccountingSystem.Data.Repository.File;
using Catdog50RUS.EmployeesAccountingSystem.Models;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Services
{
    public class ReportSettingsService : ISettingsRepository
    {
        private ISettingsRepository SettingsRepository { get; } = new FileReportSettings();

        public async Task<ReportSettings> GetSettings()
        {
            return await SettingsRepository.GetSettings();

        }

        public Task<bool> SaveSettings(ReportSettings settings)
        {
            return SettingsRepository.SaveSettings(settings);
        }
    }
}
