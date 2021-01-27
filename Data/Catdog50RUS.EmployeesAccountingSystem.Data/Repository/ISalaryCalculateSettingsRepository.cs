using Catdog50RUS.EmployeesAccountingSystem.Models;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository
{
    public interface ISalaryCalculateSettingsRepository
    {
        Task<SalaryCalculatingSettings> GetSettings();
        Task<bool> SaveSettings(SalaryCalculatingSettings settings);
    }
}
