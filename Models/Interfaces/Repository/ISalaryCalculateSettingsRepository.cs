using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    public interface ISalaryCalculateSettingsRepository
    {
        Task<SalaryCalculatingSettings> GetSettings();
        Task<bool> SaveSettings(SalaryCalculatingSettings settings);
    }
}
