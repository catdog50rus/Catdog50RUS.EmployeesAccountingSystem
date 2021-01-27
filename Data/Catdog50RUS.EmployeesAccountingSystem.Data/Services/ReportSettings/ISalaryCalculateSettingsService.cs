using Catdog50RUS.EmployeesAccountingSystem.Models;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Services.ReportSettings
{
    public interface ISalaryCalculateSettingsService
    {
        Task<SalaryCalculatingSettings> GetSalaryCalculatingSettings();
        Task<bool> SaveSalaryCalculatingSettings(SalaryCalculatingSettings settings);
    }
}
