using Catdog50RUS.EmployeesAccountingSystem.Models;
using Catdog50RUS.EmployeesAccountingSystem.Models.Employees;
using System.Threading.Tasks;

namespace Catdog50RUS.EmployeesAccountingSystem.Data.Services.AutorizeService
{
    public interface IAutorize
    { 
        Task<BaseEmployee> AutentificatedUser(string name);
        Autorize GetAuthorization(BaseEmployee employee);
       
    }

}
