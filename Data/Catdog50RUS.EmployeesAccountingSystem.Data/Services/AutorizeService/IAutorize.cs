using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Services.AutorizeService
{
    public interface IAutorize
    {
        Autorize GetAuthorization(BaseEmployee employee);
        Task<BaseEmployee> Autentificate(string name);
    }

}
