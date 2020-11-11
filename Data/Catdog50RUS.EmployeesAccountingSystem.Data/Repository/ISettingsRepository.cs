using Catdog50RUS.EmployeesAccountingSystem.Models;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Repository
{
    public interface ISettingsRepository
    {
        Task<ReportSettings> GetSettings();
        Task<bool> SaveSettings(ReportSettings settings);
    }
}
