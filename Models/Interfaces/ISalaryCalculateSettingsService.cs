using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    public interface ISalaryCalculateSettingsService
    {
        Task<SalaryCalculatingSettings> GetSalaryCalculatingSettings();
        Task<bool> SaveSalaryCalculatingSettings(SalaryCalculatingSettings settings);
    }
}
