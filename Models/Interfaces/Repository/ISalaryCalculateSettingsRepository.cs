using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Models
{
    //Не используется
    public interface ISalaryCalculateSettingsRepository
    {
        Task<SalaryCalculatingSettings> GetSettings();
        Task<bool> SaveSettings(SalaryCalculatingSettings settings);
    }
}
