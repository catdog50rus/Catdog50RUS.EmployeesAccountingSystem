using Models.Settings;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository
{
    public interface ISettingsRepository
    {
        Task<ReportSettings> GetSettings();
        Task<bool> SaveSettings(ReportSettings settings);
    }
}
